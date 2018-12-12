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
using Duality.Editor;
using Duality.Drawing;

namespace RockyTV.Duality.Plugins.IronPython
{
	[EditorHintCategory(Properties.ResNames.CategoryScripts)]
	[EditorHintImage(Properties.ResNames.IconScriptGo)]
	public class PythonScriptExecutor : Component, ICmpInitializable, ICmpUpdatable, ICmpRenderer
	{
		public ContentRef<PythonScript> Script { get; set; }

		[DontSerialize]
		private PythonExecutionEngine _engine;
		protected PythonExecutionEngine Engine
		{
			get { return _engine; }
		}

		protected virtual float Delta => Time.DeltaTime;
		private float boundRadius = 1.0f;
		[EditorHintFlags(MemberFlags.Invisible)]
		public float BoundRadius
		{
			get
			{
				return boundRadius;
			}
		}

		void ICmpInitializable.OnActivate()
		{
			if (Script.IsExplicitNull || !Script.IsAvailable) return;

			_engine = new PythonExecutionEngine(Script.Res.Content);
			_engine.SetVariable("game_object", GameObj);

			if (_engine != null)
			{
				// I can't find a way to retrieve single variables from the code
				// that's why we need a function (method that returns an object)
				if (_engine.HasMethod("bound_radius"))
					boundRadius = Convert.ToSingle(_engine.CallFunction("bound_radius"));

				if (_engine.HasMethod("start"))
					_engine.CallMethod("start");
			}
		}

		void ICmpUpdatable.OnUpdate()
		{
			if (_engine != null)
				if (_engine.HasMethod("update"))
					_engine.CallMethod("update", Delta);
		}

		void ICmpInitializable.OnDeactivate()
		{
			GameObj.DisposeLater();
		}

		void ICmpRenderer.Draw(IDrawDevice device)
		{
			// Create the canvas in our code and just pass it as a parameter
			// to the script's draw function
			var _canvas = new Canvas();

			_canvas.Begin(device);

			if (_engine != null)
				if (_engine.HasMethod("draw"))
					_engine.CallMethod("draw", _canvas);

			_canvas.End();
		}

		void ICmpRenderer.GetCullingInfo(out CullingInfo info)
		{

			info.Position = Vector3.Zero;
			info.Radius = boundRadius;
			info.Visibility = VisibilityFlag.AllGroups | VisibilityFlag.ScreenOverlay;
		}
	}
}
