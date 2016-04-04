using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailManager.Utility;

namespace MailManager
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            CurrentContentViewModel = new TemplateManager.TemplateManagerViewModel();
        }

        public BindableBase CurrentContentViewModel { get; set; }
    }
}
