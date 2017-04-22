using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality.Editor;

namespace RockyTV.Duality.Editor.Plugins.IronPython
{
	/// <summary>
	/// Defines a Duality editor plugin.
	/// </summary>
	public class PythonScriptingEditorPlugin : EditorPlugin
	{
		public override string Id
		{
			get { return "RockyTV.IronPython"; }
		}
	}
}
