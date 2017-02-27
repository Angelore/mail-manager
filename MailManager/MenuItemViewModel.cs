using MailManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailManager
{
    public class MenuItemViewModel: BindableBase
    {
        private string _header;
        public string Header
        {
            get { return _header; }
            set { SetProperty(ref _header, value); }
        }

        private string _iconName;
        public string IconName
        {
            get { return _iconName; }
            set { SetProperty(ref _iconName, value); }
        }

        private int _iconSize;
        public int IconSize
        {
            get { return _iconSize; }
            set { SetProperty(ref _iconSize, value); }
        }

        private ICommand _command;
        public ICommand Command
        {
            get { return _command; }
            set { SetProperty(ref _command, value); }
        }

        private object _commandParameter;
        public object CommandParameter
        {
            get { return _commandParameter; }
            set { SetProperty(ref _commandParameter, value); }
        }


        private List<MenuItemViewModel> _children;
        public List<MenuItemViewModel> Children
        {
            get { return _children; }
            set { SetProperty(ref _children, value); }
        }

    }
}
