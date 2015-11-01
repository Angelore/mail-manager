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

namespace MailManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MenuCommands MenuCommands { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MenuCommands = new MenuCommands(this);
        }

        private void ExecuteMenuCommand(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string commandName = menuItem.CommandParameter.ToString();
            MenuCommands.GetCommand(commandName)?.Execute();
        }
    }
}
