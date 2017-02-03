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
        public string Content { get { return _content; } }

        public PythonScript()
        {
            var sb = new StringBuilder();
            sb.AppendLine("# You can access the parent GameObject by calling `gameObject`.");
            sb.AppendLine("# To use Duality objects, you must first import them.");
            sb.AppendLine("# Example:");
            sb.AppendLine(@"#\tfrom Duality import Vector2");
            sb.AppendLine();
            sb.AppendLine("class PyModule: ");
            sb.AppendLine("    def __init__(self):");
            sb.AppendLine("        pass");
            sb.AppendLine();
            sb.AppendLine("# The `start` function is called whenever a component is initializing.");
            sb.AppendLine("    def start(self):");
            sb.AppendLine("        pass");
            sb.AppendLine();
            sb.AppendLine("# The `update` function is called whenever an update happens, and includes a delta.");
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
