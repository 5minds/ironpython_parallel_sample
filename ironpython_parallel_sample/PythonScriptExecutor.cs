namespace Elster.SmartMeter.Services.DeviceCommunication.PythonAPI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using IronPython.Hosting;

    /// <summary>
    /// Execute the DeviceCommunication python script with the required parameters.
    /// </summary>
    internal class PythonScriptExecutor : MarshalByRefObject
    {
        //private const string ResultVariableName = "RESULT";

        public IReadOnlyDictionary<string, object> Execute()
        {
            var options = new Dictionary<string, object>();
#if true
            // Enable debugging from c# into python code
            options["Debug"] = false;
#endif
            var pythonEngine = Python.CreateEngine(options);
            var pythonScope = pythonEngine.CreateScope();

            // set search paths
            var searchPaths = pythonEngine.GetSearchPaths();
            searchPaths.Add(@"c:\dev\elster\ironpython_parallel_sample\ironpython_parallel_sample\Lib");  // IronPython Path
            pythonEngine.SetSearchPaths(searchPaths);

            var executePath = @"c:\dev\elster\ironpython_parallel_sample\ironpython_parallel_sample\exec_dc_api.py";

            // Calls DeviceCommunication PythonAPI execute file
            var resultScope = pythonEngine.ExecuteFile(executePath, pythonScope);

            return null;

            // Get result dictionary
            //var result = resultScope.GetVariable<Dictionary<string, object>>(ResultVariableName);

            // Return readonly result dictionary
            //return new ReadOnlyDictionary<string, object>(result);
        }
    }
}
