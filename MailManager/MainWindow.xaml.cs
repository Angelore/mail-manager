using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MailManager.Utility;

namespace MailManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var view = new MainWindowViewModel();
            view.CloseWindowAction = this.Close;
            view.SetSelfAsParentWindowAction = this.SetSelfAsParentWindow;
            DataContext = view;
        }

        public void SetSelfAsParentWindow(Window window)
        {
            window.Owner = this;
        }
    }
}
