using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Duality;
using Duality.Editor;

namespace PythonScripting.Resources
{
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
