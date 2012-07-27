using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFE.Lib;
using WFE.Models;
using System.Threading.Tasks;
using System.Reflection;
using System.Workflow.ComponentModel;
using WFE.Exceptions;
using System.IO;
using WFE.Base;

namespace WFE.Extractors
{
    internal sealed class WorkflowExtractor : Events
    {
        private readonly AssemblyResolver _assemblyResolver;
        private List<ActivityChainModel> _activitiesInAssembly;
        private readonly Type _typeActivity;

        internal List<ActivityChainModel> Activities
        {
            get { return _activitiesInAssembly; }
        }

        internal WorkflowExtractor(AssemblyResolver assemblyResolver)
        {
            if (assemblyResolver == null)
                throw new ArgumentNullException("assemblyResolver");

            _assemblyResolver = assemblyResolver;
            _typeActivity = typeof(Activity);
        }

        private void GetActivitiesInAssembly()
        {
            _activitiesInAssembly = new List<ActivityChainModel>();

            try
            {

                Type[] types = _assemblyResolver.InputAssembly.GetTypes();
                int len = types.Length;
                string typeNames = types.Select(x => x.Name).ToArray().ToCsv();
                SendMessage("Activity search started found " + types.Length + " " + typeNames, EventType.Output);

                for (int x = 0; x < len; x++)
                //Parallel.For(0, len, x =>
                {
                    SendMessage("Processing activity: " + types[x].FullName, EventType.Output);

                    try
                    {
                        var model = new ActivityChainModel {WorkflowType = types[x]};
                        model.Workflow = Activator.CreateInstance(model.WorkflowType);

                        IEnumerable<FieldInfo> innerFields = model.WorkflowType.GetFields(BindingFlags.NonPublic |
                                                                                          BindingFlags.Instance |
                                                                                          BindingFlags.Public);

                        innerFields = innerFields.Where(current => current.ReflectedType.IsDerivativeOf(_typeActivity));

                        Parallel.ForEach(innerFields, currentField =>
                        {
                            Activity activityCreated = null;
                            try
                            {
                                activityCreated = (Activity)currentField.GetValue(model.Workflow);
                                if (activityCreated != null)
                                {
                                    SendMessage(activityCreated.ToString() + activityCreated, EventType.Output);
                                    model.AddActivity(activityCreated);
                                }
                                else
                                {
                                    model.Errors.Add(new CouldNotCreateInstanceException(CouldNotCreateInstanceException.CreateMessage(currentField)));
                                    SendMessage("Could not create instance of field " + currentField.Name, EventType.Error);
                                }
                            }
                            catch (Exception ex)
                            {
                                model.Errors.Add(ex);
                                if (activityCreated != null)
                                    SendMessage(activityCreated.Name + " " + ex.Message, EventType.Error);
                                else
                                    SendMessage(ex.Message, EventType.Error);
                            }
                        });

                        _activitiesInAssembly.Add(model);
                    }
                    catch (Exception exActivity)
                    {
                        SendMessage(types[x].FullName + " " + exActivity.Message, EventType.Error);
                    }
                }

                SendMessage("Activity search completed", EventType.Output);
            }
            catch (Exception ex)
            {
                SendMessage(ex.Message, EventType.Error);
            }
        }

        private void AssociateActivities()
        {
            SendMessage("Activity association started", EventType.Output);

            // Loop for each class
            Parallel.For(0, _activitiesInAssembly.Count, classCounter =>
                {
                    try
                    {
                        var allActivities = _activitiesInAssembly[classCounter].UnresolvedActivities;
                        var rootActivities = _activitiesInAssembly[classCounter].ActivityTree;

                        var orderedActivities = allActivities.OrderBy(x => x.Path);
                        var removalList = new List<ActivityWrapper>();

                        foreach (ActivityWrapper wrapper in orderedActivities)
                        {
                            ActivityWrapper rootActivity;

                            try
                            {
                                // find root activity
                                rootActivity = rootActivities.First(x => x.Path == wrapper.RootPath);
                            }
                            catch (InvalidOperationException exception)
                            {
                                try
                                {
                                    // root not found exception in Root Activities?
                                    // Check in the all activity list if it has any
                                    rootActivity = allActivities.First(x => x.Path == wrapper.RootPath);
                                }
                                catch (InvalidOperationException)
                                {
                                    // ideally should not come here....
                                    // if it still comes, swallow the exception and then skip this record
                                    SendMessage("Could not find an activity " + wrapper.Name + " ignoring", EventType.Output);
                                    continue;
                                }
                            }

                            if (rootActivity != null)
                            {
                                rootActivity.Children.Add(wrapper);
                                removalList.Add(wrapper);
                            }
                        }

                        if (removalList.Count > 0) // some changes happened, so you need to remove this from the activity list..
                        {
                            for (int x = 0; x < removalList.Count; x++)
                            {
                                _activitiesInAssembly[classCounter].UnresolvedActivities.Remove(removalList[x]);
                            }

                            removalList.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        _activitiesInAssembly[classCounter].Errors.Add(ex);

                        SendMessage(ex.Message, EventType.Error);
                    }
                });

            SendMessage("Activity association completed", EventType.Output);
        }

        internal List<ActivityChainModel> GetActivityTree()
        {
            GetActivitiesInAssembly();
            CreateExpectedTemplate();
            AssociateActivities();

            return _activitiesInAssembly;
        }

        private void CreateExpectedTemplate()
        {
            try
            {
                var templateExpected = new TemplateModel();

                for (int activityCount = 0; activityCount < _activitiesInAssembly.Count; activityCount++)
                {
                    for (int typeCount = 0; typeCount < _activitiesInAssembly[activityCount].DeclarationTypes.Count; typeCount++)
                    {
                        if (templateExpected.Data.Count(y => y.ActivityType == 
                            _activitiesInAssembly[activityCount].DeclarationTypes[typeCount]) == 0)
                        {
                            templateExpected.Data.Add(new TemplateRowModel
                            {
                                ActivityType = _activitiesInAssembly[activityCount].DeclarationTypes[typeCount],
                                Description = "Sample desc"
                            });
                        }
                    }
                }

                string template = Serializer.Serialize<TemplateModel>(templateExpected, Encoding.UTF8);
                string path = Path.Combine(_assemblyResolver.BaseDirectory, "expected-template.xml");
                File.WriteAllText(path, template);

                SendMessage("Expected template built: " + path, EventType.Output);
            }
            catch
            {
            }
        }
    }
}