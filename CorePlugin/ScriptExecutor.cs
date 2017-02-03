using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using PythonScripting.Resources;

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using IronPython.Compiler;

namespace PythonScripting
{
	public class ScriptExecutor : Component, ICmpInitializable
	{
        public ContentRef<PythonScript> Script { get; set; }

		public void OnInit(InitContext context)
        {
            if (context == InitContext.Activate)
            {
            }
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
