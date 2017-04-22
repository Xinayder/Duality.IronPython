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

		protected virtual float Delta
		{
			get { return Time.MsPFMult * Time.TimeMult; }
		}

		[EditorHintFlags(MemberFlags.Invisible)]
		public float BoundRadius
		{
			get
			{
				if (_engine != null)
					if (_engine.HasMethod("bound_radius"))
						return Convert.ToSingle(_engine.GetVariable("bound_radius"));
				return 0.0f;
			}
		}

		public void OnInit(InitContext context)
		{
			if (Script.IsExplicitNull || !Script.IsAvailable) return;

			if (context == InitContext.Loaded)
			{
				_engine = new PythonExecutionEngine(Script.Res.Content);
				_engine.SetVariable("game_object", GameObj);
			}

			if (_engine != null)
				if (_engine.HasMethod("start"))
					_engine.CallMethod("start", context);
		}

		public void OnUpdate()
		{
			if (_engine != null)
				if (_engine.HasMethod("update"))
					_engine.CallMethod("update", Delta);
		}

		public void OnShutdown(ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate)
				GameObj.DisposeLater();
		}

		public bool IsVisible(IDrawDevice device)
		{
			if (_engine != null)
			{
				if (_engine.HasMethod("is_visible"))
				{
					bool isVisible;
					if (bool.TryParse(Convert.ToString(_engine.CallFunction("is_visible", device)), out isVisible))
						return isVisible;
				}
			}
			return false;
		}

		public void Draw(IDrawDevice device)
		{
			if (_engine != null)
				if (_engine.HasMethod("draw"))
					_engine.CallMethod("draw", device);
		}
	}
}
