using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using RockyTV.Duality.Plugins.IronPython.Resources;

using IronPython.Hosting;
using IronPython.Runtime;
using IronPython.Compiler;

using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace RockyTV.Duality.Plugins.IronPython
{
    public class ScriptExecutor : Component, ICmpInitializable, ICmpUpdatable
    {
        public ContentRef<PythonScript> Script { get; set; }

        [DontSerialize]
        private PythonExecutionEngine _engine;
        protected PythonExecutionEngine Engine
        {
            get { return _engine; }
        }

        protected virtual float Delta
        {
            get { return Time.MsPFMult * Time.TimeMult; }
        }

        public void OnInit(InitContext context)
        {
            if (!Script.IsAvailable) return;

            _engine = new PythonExecutionEngine(Script.Res.Content);
            _engine.SetVariable("gameObject", GameObj);

            if (_engine.HasMethod("start"))
                _engine.CallMethod("start");
        }

        public void OnUpdate()
        {
            if (_engine.HasMethod("update"))
                _engine.CallMethod("update", Delta);
        }

        public void OnShutdown(ShutdownContext context)
        {
            if (context == ShutdownContext.Deactivate)
            {
                GameObj.DisposeLater();
            }
        }
    }
}
