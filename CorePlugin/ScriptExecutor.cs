using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using PythonScripting.Resources;

using IronPython.Hosting;
using IronPython.Runtime;
using IronPython.Compiler;

using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace PythonScripting
{
	public class ScriptExecutor : Component, ICmpInitializable, ICmpUpdatable
	{
        public ContentRef<PythonScript> Script { get; set; }

		public void OnInit(InitContext context)
        {
            if (context == InitContext.Activate)
            {

            }
        }

        public void OnUpdate()
        {

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
