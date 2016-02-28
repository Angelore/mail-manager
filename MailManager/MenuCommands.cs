using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailManager
{
    internal class MenuCommands
    {
        private Window _window;
        private List<BaseMenuCommand> _commands;

        internal MenuCommands(Window window)
        {
            _window = window;
            _commands = new List<BaseMenuCommand>()
            {
                new ExitCommand(_window),
                new AboutCommand(_window)
            };
        }

        internal BaseMenuCommand GetCommand(string commandName)
        {
            return _commands.FirstOrDefault(x => x.CommandName == commandName);
        }
    }

    internal class BaseMenuCommand
    {
        protected Window _window;

        public BaseMenuCommand(Window window)
        {
            _window = window;
        }

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }

        public virtual string CommandName { get; } = "BaseCommand";
    }

    internal class ExitCommand: BaseMenuCommand
    {
        public ExitCommand(Window window):base(window)
        { }

        public override void Execute()
        {
            _window.Close();
        }

        public override string CommandName { get; } = "ExitCommand";
    }

    internal class AboutCommand: BaseMenuCommand
    {
        public AboutCommand(Window window):base(window)
        { }

        public override void Execute()
        {
            var childWindow = new Window();
            childWindow.Content = new AboutView();
            childWindow.Owner = _window;
            childWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            childWindow.ResizeMode = ResizeMode.NoResize;
            childWindow.MaxHeight = 300;
            childWindow.MaxWidth = 500;
            childWindow.ShowDialog();
        }

        public override string CommandName { get; } = "AboutCommand";
    }
}
