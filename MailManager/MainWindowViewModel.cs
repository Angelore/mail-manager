using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailManager.Utility;
using System.Windows;

namespace MailManager
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            CurrentContentViewModel = new TemplateManager.TemplateManagerViewModel();

            ExitMenuCommand = new RelayCommand<MainWindow>(OnExitMenuCommand);
            AboutMenuCommand = new RelayCommand<MainWindow>(OnAboutMenuCommand);
        }

        public BindableBase CurrentContentViewModel { get; set; }
        
        public RelayCommand<MainWindow> ExitMenuCommand { get; private set; }
        public RelayCommand<MainWindow> AboutMenuCommand { get; private set; }

        public void OnExitMenuCommand(MainWindow window)
        {
            window.Close();
        }

        public void OnAboutMenuCommand(MainWindow window)
        {
            var childWindow = new Window();
            childWindow.Content = new AboutView();
            childWindow.Owner = window;
            childWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            childWindow.ResizeMode = ResizeMode.NoResize;
            childWindow.MaxHeight = 300;
            childWindow.MaxWidth = 500;
            childWindow.ShowDialog();
        }
    }
}
