using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Duality;
using Duality.Editor;

namespace RockyTV.Duality.Plugins.IronPython.Resources
{
	[EditorHintCategory(Properties.ResNames.CategoryScripts)]
	[EditorHintImage(Properties.ResNames.IconScript)]
	public class PythonScript : Resource
	{
		protected string _content;
		[EditorHintFlags(MemberFlags.Invisible)]
		public string Content { get { return _content; } }

		public PythonScript()
		{
			var sb = new StringBuilder();
			sb.AppendLine("# You can access the parent GameObject by calling `game_object`.");
			sb.AppendLine("#");
			sb.AppendLine("# To use Duality classes, you must first import them:");
			sb.AppendLine("#    from Duality import Vector2");
			sb.AppendLine("#");
			sb.AppendLine("# By using `import Duality`, you must use qualified names:");
			sb.AppendLine("#    import Duality");
			sb.AppendLine("#    Duality.Log.Game.Write('Hello, world!')");
			sb.AppendLine("#");
			sb.AppendLine("# All code must be inside a class called `PyModule`.");
			sb.AppendLine("# Code outside the class scope will be executed as well,");
			sb.AppendLine("# except that you won't be able to access functions nor variables.");
			sb.AppendLine();
			sb.AppendLine("import Duality");
			sb.AppendLine("class PyModule: ");
			sb.AppendLine("    def __init__(self):");
			sb.AppendLine("        pass");
			sb.AppendLine();
			sb.AppendLine("    # Called when a Component is initializing.");
			sb.AppendLine("    # `context`: what kind of initialization is being performed on the Component.");
			sb.AppendLine("    def start(self, context):");
			sb.AppendLine("        pass");
			sb.AppendLine();
			sb.AppendLine("    # Called once per frame in order to update the Component.");
			sb.AppendLine("    # `delta`: time the last frame took, in milliseconds, with time scale applied.");
			sb.AppendLine("    def update(self, delta):");
			sb.AppendLine("        pass");
			sb.AppendLine();

			_content = sb.ToString();
		}

		public void UpdateContent(string content)
		{
			_content = content;
		}
	}
}
