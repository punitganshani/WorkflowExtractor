using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WFE.Lib
{
    public class AssemblyResolver : IDisposable
    {
        private readonly string _assemblyPath;
        private string _baseDirectory;

        public string BaseDirectory
        {
            get { return _baseDirectory; }
        }

        private Assembly _assembly;

        public Assembly InputAssembly
        {
            get { return _assembly; }
        }

        private readonly Dictionary<string, string> _resourceCache;

        public event EventHandler OnAssemblyLoaded;

        public AssemblyResolver(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new ArgumentNullException("assemblyPath");

            _assemblyPath = assemblyPath;
            _resourceCache = new Dictionary<string, string>();
        }

        internal void Load()
        {
            try
            {
                _baseDirectory = FileUtils.GetDirectory(_assemblyPath);
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                var worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(WorkerDoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerRunWorkerCompleted);
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Could not load the assembly " + _assemblyPath, ex);
            }
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnAssemblyLoaded != null)
                OnAssemblyLoaded(this, EventArgs.Empty);
        }

        void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            LoadAssemblyCache();
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assembly = Path.Combine(_baseDirectory, args.Name.Remove(args.Name.IndexOf(',')) + ".dll");
            return Assembly.LoadFile(assembly);
        }

        internal List<string> GetTypes()
        {
            try
            {
                return _assembly.GetTypes().Select(x => x.FullName).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Could not retrieve the types from the assembly " + _assemblyPath, ex);
            }
        }

        internal List<string> GetResources()
        {
            try
            {
                return _assembly.GetManifestResourceNames().Where(x => x.Contains(".rules")).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Could not retrieve the types from the assembly " + _assemblyPath, ex);
            }
        }

        private void LoadAssemblyCache()
        {
            _assembly = Assembly.LoadFrom(_assemblyPath);

            string[] resources = _assembly.GetManifestResourceNames();

            Parallel.For(0, resources.Length, i => GetResourceData(resources[i]));

        }

        internal string GetResourceData(string resource)
        {
            if (_resourceCache.ContainsKey(resource) == false)
            {
                Stream stream = _assembly.GetManifestResourceStream(resource);
                if (stream != null)
                {
                    var data = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(data, 0, (int)stream.Length);

                    _resourceCache[resource] = Encoding.ASCII.GetString(data);
                }
            }

            return _resourceCache[resource];
        }

        #region IDisposable Members

        public void Dispose()
        {
            _assembly = null;
        }

        #endregion


    }
}
