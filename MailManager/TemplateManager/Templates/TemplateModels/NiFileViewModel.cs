using MailManager.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MailManager.TemplateManager.Templates.TemplateModels
{
    class NiFileViewModel: BindableBase
    {
        private string _source;

        public NiFileViewModel()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var reader = new StreamReader(assembly.GetManifestResourceStream("MailManager.TemplateManager.Templates.FileTemplates.ni.txt"), Encoding.GetEncoding(866));
            var _source = reader.ReadToEnd();
        }
    }
}
