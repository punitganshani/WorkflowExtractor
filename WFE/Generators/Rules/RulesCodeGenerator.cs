using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using WFE.Base;
using WFE.Lib;
using WFE.Models;
using WFE.Models.Output;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WFE.Generators.Rules
{
    internal class RulesCodeGenerator : RulesOutputGenerator
    {
        private readonly CodeSettings _codeSettings;

        public RulesCodeGenerator(List<RuleSetDomModel> model, Settings settings, bool runAsync)
            : base(model, settings, runAsync)
        {
            if (settings.GetLanguage() > OutputFormat.VBNet)
                throw new NotSupportedException("Output Language not supported:" + settings.GetLanguage());

            if (settings.GetType() != typeof(CodeSettings)) // create default codesettings
                _codeSettings = new CodeSettings("ClassName", settings.GetOutputDirectory(), settings.GetLanguage());
            else
                _codeSettings = (CodeSettings)settings;

            Name = "Rules to Code Generator";
        }

        #region CodeGenerator

        private CodeNamespace GenerateSkeleton()
        {
            CodeNamespace codeNamespace = CreateNameSpace(_codeSettings.Namespace);
            CodeTypeDeclaration generatedClass = CreateClass(Settings.GetInputFile());

            codeNamespace.Types.Add(generatedClass);

            for (int i = 0; i < Model.Count; i++)
            {
                if (Model[i] == null)
                    continue;

                if (_codeSettings.CreateSubClass)
                {
                    #region Sub Class
                    CodeTypeDeclaration generatedSubClass = CreateClass(Model[i].Name);
                    generatedClass.Members.Add(generatedSubClass);

                    for (int x = 0; x < Model[i].MethodsRuleDom.Count; x++)
                    {
                        RulesDomModel codeModel = Model[i].MethodsRuleDom[x];
                        generatedSubClass.Members.Add(CreateMethod(Model[i].Name, codeModel));
                    }
                    #endregion
                }
                else
                {
                    for (int x = 0; x < Model[i].MethodsRuleDom.Count; x++)
                    {
                        RulesDomModel codeModel = Model[i].MethodsRuleDom[x];
                        generatedClass.Members.Add(CreateMethod(Model[i].Name, codeModel));
                    }
                }
            }

            return codeNamespace;
        }

        private CodeNamespace GenerateSkeletonAsync()
        {
            CodeNamespace codeNamespace = CreateNameSpace(_codeSettings.Namespace);
            CodeTypeDeclaration generatedClass = CreateClass(Settings.GetInputFile());

            codeNamespace.Types.Add(generatedClass);

            Parallel.For(0, Model.Count, i =>
            {
                if (Model[i] != null)
                {
                    if (_codeSettings.CreateSubClass)
                    {
                        #region Sub Class
                        CodeTypeDeclaration generatedSubClass = CreateClass(Model[i].Name);
                        generatedClass.Members.Add(generatedSubClass);
                                               
                        for (int x = 0; x < Model[i].MethodsRuleDom.Count; x++)
                        {
                            RulesDomModel codeModel = Model[i].MethodsRuleDom[x];
                            generatedSubClass.Members.Add(CreateMethod(Model[i].Name, codeModel));
                        }
                        #endregion
                    }
                    else
                    {
                        for (int x = 0; x < Model[i].MethodsRuleDom.Count; x++)
                        {
                            RulesDomModel codeModel = Model[i].MethodsRuleDom[x];
                            generatedClass.Members.Add(CreateMethod(Model[i].Name, codeModel));
                        }
                    }
                }
            });

            return codeNamespace;
        }



        private CodeTypeMember CreateMethod(string ruleSetName, RulesDomModel codeModel)
        {
            // Declare a new CodeEntryPointMethod
            var method = new CodeMemberMethod
                             {
                                 Attributes = MemberAttributes.Public,
                                 Name = ParseName(ruleSetName, codeModel),
                                 ReturnType = new CodeTypeReference(_codeSettings.MethodSettings.Returns),
                             };

            string[] paramList = _codeSettings.MethodSettings.ParametersFormat.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (paramList.Length % 2 == 0) // even numbers
            {
                for (int i = 0; i < paramList.Length; i += 2)
                    method.Parameters.Add(new CodeParameterDeclarationExpression(paramList[i], paramList[i + 1]));
            }

            // Specify that this method will be both static and public

            if (_codeSettings.IncludeComments && codeModel.Comments.Count > 0)
                method.Comments.AddRange(codeModel.Comments.ToArray());

            var variableResult =
                new CodeVariableDeclarationStatement(new CodeTypeReference(_codeSettings.MethodSettings.Returns),
                                                     "result",
                                                     new CodeDefaultValueExpression(
                                                         new CodeTypeReference(_codeSettings.MethodSettings.Returns)));
            method.Statements.Add(variableResult);

            var conditionStatement = new CodeConditionStatement(codeModel.Condition, codeModel.Then.ToArray(),
                                                                codeModel.Else.ToArray());
            method.Statements.Add(conditionStatement);
            method.Statements.Add(
                new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "result")));

            return method;
        }

        private string ParseName(string ruleSet, RulesDomModel codeModel)
        {
            return _codeSettings.MethodSettings.NameFormat.Replace("[workflow]",
                _codeSettings.GetInputFile())
                .Replace("[ruleSet]", codeModel.ParentRuleSet.Name)
                .Replace("[ruleName]", codeModel.Name);
        }

        private CodeTypeDeclaration CreateClass(string name)
        {
            var returnValue = new CodeTypeDeclaration(name) { IsClass = true, IsPartial = true };

            for (int i = 0; i < _codeSettings.InheritedClasses.Count; i++)
                returnValue.BaseTypes.Add(new CodeTypeReference(_codeSettings.InheritedClasses[i]));

            AddConstructorWithModel(returnValue);

            return returnValue;
        }

        private void AddConstructorWithModel(CodeTypeDeclaration generatedClass)
        {
            var defaultConstructor = new CodeConstructor { Attributes = MemberAttributes.Public };
            generatedClass.Members.Add(defaultConstructor);

            if (_codeSettings.ModelType != null)
            {
                var member = new CodeMemberField(_codeSettings.ModelType, "_model");
                generatedClass.Members.Add(member);

                var constructor = new CodeConstructor();
                constructor.Parameters.Add(new CodeParameterDeclarationExpression(_codeSettings.ModelType, "model"));
                constructor.Attributes = MemberAttributes.Public;
                constructor.Statements.Add(new CodeAssignStatement(new CodeSnippetExpression("_model"),
                                                                   new CodeSnippetExpression("model")));
                generatedClass.Members.Add(constructor);
            }
        }

        private CodeNamespace CreateNameSpace(string nameSpaceName)
        {
            var returnValue = new CodeNamespace(nameSpaceName);

            for (int i = 0; i < _codeSettings.References.Count; i++)
                returnValue.Imports.Add(new CodeNamespaceImport(_codeSettings.References[i]));

            return returnValue;
        }

        private string GenerateCode(CodeNamespace sourceCode)
        {
            CodeDomProvider provider = CreateProvider();
            var codeGeneratorOptions = new CodeGeneratorOptions
                                           {
                                               BlankLinesBetweenMembers = false,
                                               BracingStyle = "C",
                                               IndentString = "   "
                                           };

            ICodeGenerator codeGenerator = provider.CreateGenerator();
            var code = new StringBuilder();
            var stringWriter = new StringWriter(code);
            codeGenerator.GenerateCodeFromNamespace(sourceCode, stringWriter, codeGeneratorOptions);
            return code.ToString();
        }

        private CodeDomProvider CreateProvider()
        {
            switch (Settings.GetLanguage())
            {
                case OutputFormat.VBNet:
                    return new VBCodeProvider();
                case OutputFormat.CSharp:
                    return new CSharpCodeProvider();

                default:
                    throw new NotSupportedException(string.Format("Format not supported:{0}", Settings.GetLanguage()));
            }
        }

        #endregion

        public override bool Generate()
        {
            try
            {
                string fileContents = string.Empty;

                if (!RunAsync)
                    fileContents = GenerateCode(GenerateSkeleton());
                else 
                    fileContents = GenerateCode(GenerateSkeletonAsync());

                fileContents = ProcessReplacements(fileContents);

                File.WriteAllText(Settings.GetOutputFile("code"), fileContents);


                return true;
            }
            catch (Exception ex)
            {
                SendMessage(ex.Message, EventType.Error);
                Log.Error("Exception occured while generating Code File ", ex);
                return false;
            }
        }

        private string ProcessReplacements(string input)
        {
            string output = input;

            for (int i = 0; i < _codeSettings.Replacements.Count; i++)
            {
                switch(_codeSettings.Replacements[i].IsRegex)
                {
                    case true:
                        output = Regex.Replace(output, _codeSettings.Replacements[i].Find, _codeSettings.Replacements[i].ReplaceBy);
                        break;

                    case false:
                        output = output.Replace(_codeSettings.Replacements[i].Find,
                                                _codeSettings.Replacements[i].ReplaceBy);
                        break;
                }
            }

            return output;
        }
    }
}