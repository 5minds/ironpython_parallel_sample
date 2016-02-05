namespace ironpython_parallel_sample
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Elster.SmartMeter.Services.DeviceCommunication.PythonAPI;

    using IronPython.Hosting;

    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Parallel.Invoke(ExecutScript, ExecutScript, ExecutScript, ExecutScript, ExecutScript);
            }

            Console.ReadKey();
        }

        private static void ExecutScript()
        {
            var options = new Dictionary<string, object>();
            options["Debug"] = false;

            var pythonEngine = Python.CreateEngine(options);

            var searchPaths = pythonEngine.GetSearchPaths();
            searchPaths.Add(@"c:\dev\elster\ironpython_parallel_sample\ironpython_parallel_sample\Lib");  // IronPython Path
            pythonEngine.SetSearchPaths(searchPaths);

            var domain = CreateAppDomain();
            var scriptExecutor = domain.CreateInstanceAndUnwrap(
                    typeof(PythonScriptExecutor).Assembly.FullName,
                    typeof(PythonScriptExecutor).FullName) as PythonScriptExecutor;

            scriptExecutor.Execute();
        }

        private static AppDomain CreateAppDomain()
        {
            var domainSetup = new AppDomainSetup();
            domainSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            var domain = AppDomain.CreateDomain(string.Empty, null, domainSetup);
            return domain;
        }
    }
}
