using System;

using IronPython.Hosting;
using IronPython.Runtime;
using IronPython.Compiler;

using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

using Duality;

namespace RockyTV.Duality.Plugins.IronPython
{
    public class PythonExecutionEngine
    {
        [DontSerialize]
        private ScriptEngine _engine;
        [DontSerialize]
        private ScriptScope _scope;
        [DontSerialize]
        private ScriptSource _source;
        [DontSerialize]
        private CompiledCode _code;
        [DontSerialize]
        private object _class;

        public PythonExecutionEngine(string source, string className = "PyModule")
        {
            if (string.IsNullOrEmpty(source)) throw new ArgumentException("script");
            string[] requiredAssemblies = new string[]
            {
                System.IO.Path.Combine(Environment.CurrentDirectory, "OpenTK.dll"),
                System.IO.Path.Combine(Environment.CurrentDirectory, "Duality.dll"),
                System.IO.Path.Combine(Environment.CurrentDirectory, "DualityPrimitives.dll")
            };

            // Add reference to Duality binaries
            var builder = new System.Text.StringBuilder();
            builder.AppendLine("import sys");
            builder.AppendLine("import clr");
            builder.AppendLine();
            foreach (string assembly in requiredAssemblies)
                builder.AppendLine(string.Format("clr.AddReferenceToFileAndPath(r\"{0}\")", assembly));
            builder.AppendLine();
            builder.AppendLine("from Duality import Vector2");
            builder.AppendLine();

            string script = source.Insert(0, builder.ToString());

            _engine = Python.CreateEngine();
            _engine = Python.CreateEngine();
            _engine.Runtime.IO.RedirectToConsole(); // for now we have to redirect to console

            // Setup stdlib path
            var searchPaths = _engine.GetSearchPaths();
            searchPaths.Add(System.IO.Path.Combine(Environment.CurrentDirectory, "PythonLibs"));
            _engine.SetSearchPaths(searchPaths);

            _scope = _engine.CreateScope();
            _source = _engine.CreateScriptSourceFromString(script);
            _code = _source.Compile();
            _code.Execute(_scope);

            _class = _engine.Operations.Invoke(_scope.GetVariable(className));
        }

        public void SetVariable(string name, dynamic value)
        {
            _scope.SetVariable(name, value);
        }

        public dynamic GetVariable(string name)
        {
            return _scope.GetVariable(name);
        }

        public void CallMethod(string method, params dynamic[] parameters)
        {
            _engine.Operations.InvokeMember(_class, method, parameters);
        }

        public dynamic CallFunction(string method, params dynamic[] parameters)
        {
            return _engine.Operations.InvokeMember(_class, method, parameters);
        }

        public bool HasMethod(string method)
        {
            object _;
            if (_engine.Operations.TryGetMember(_class, method, out _))
                return true;
            return false;
        }
    }
}
