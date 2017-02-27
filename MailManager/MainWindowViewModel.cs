using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailManager.Utility;
using System.Windows;

namespace MailManager
{
    public class MainWindowViewModel: BindableBase
    {
        public MainWindowViewModel()
        {            
            ExitMenuCommand = new RelayCommand(OnExitMenuCommand);
            AboutMenuCommand = new RelayCommand(OnAboutMenuCommand);

            MenuItems = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel()
                {
                    Header = Utility.Localization.Get("MainWindowMenuFile"),
                    Children = new List<MenuItemViewModel>()
                    {
                        new MenuItemViewModel()
                        {
                            Header = Utility.Localization.Get("MainWindowMenuExit"),
                            Command = ExitMenuCommand,
                            IconName = "SignOut",
                            IconSize = 14
                        }
                    }
                },
                new MenuItemViewModel()
                {
                    Header = Utility.Localization.Get("MainWindowMenuHelp"),
                    Children = new List<MenuItemViewModel>()
                    {
                        new MenuItemViewModel()
                        {
                            Header = Utility.Localization.Get("MainWindowMenuAbout"),
                            Command = AboutMenuCommand,
                            IconName = "Question",
                            IconSize = 12
                        }
                    }
                }
            };

            CurrentContentViewModel = new TemplateManager.TemplateManagerViewModel();
        }

        private BindableBase _currentContentViewModel;
        public BindableBase CurrentContentViewModel
        {
            get { return _currentContentViewModel; }
            set
            {
                if (value is IMenuProvider)
                    ReplacePreviousMenuItem(value as IMenuProvider);
                SetProperty(ref _currentContentViewModel, value);
            }
        }

        private List<MenuItemViewModel> _menuItems;
        public List<MenuItemViewModel> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }


        private void ReplacePreviousMenuItem(IMenuProvider provider)
        {
            // try to remove previous menu item
            if (_currentContentViewModel is IMenuProvider)
            {
                var prevItem = MenuItems.FirstOrDefault(x => x.Header == (_currentContentViewModel as IMenuProvider).MenuItemHeader);
                if (prevItem != null)
                    MenuItems.Remove(prevItem);
            }
            MenuItems.Add(provider.GetMenuItem());
        }

        public RelayCommand ExitMenuCommand { get; private set; }
        public RelayCommand AboutMenuCommand { get; private set; }

        public void OnExitMenuCommand()
        {
            CloseWindowAction?.Invoke();
        }

        public void OnAboutMenuCommand()
        {
            var childWindow = new Window();
            childWindow.Content = new AboutView();
            childWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            childWindow.ResizeMode = ResizeMode.NoResize;
            childWindow.MaxHeight = 300;
            childWindow.MaxWidth = 500;
            SetSelfAsParentWindowAction?.Invoke(childWindow);
            childWindow.ShowDialog();
        }

        #region Interaction with parent view
        public Action CloseWindowAction { get; set; }
        public Action<Window> SetSelfAsParentWindowAction { get; set; }
        #endregion
    }
}
