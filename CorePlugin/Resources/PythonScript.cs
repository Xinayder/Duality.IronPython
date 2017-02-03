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

        public void UpdateContent(string content)
        {
            _content = content;
        }
    }
}
