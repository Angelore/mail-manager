using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailManager
{
    internal class MenuCommands
    {
        private MainWindow _window;
        private List<BaseMenuCommand> _commands;

        internal MenuCommands(MainWindow window)
        {
            _window = window;
            _commands = new List<BaseMenuCommand>()
            {
                new ExitCommand(_window)
            };
        }

        internal BaseMenuCommand GetCommand(string commandName)
        {
            return _commands.FirstOrDefault(x => x.CommandName() == commandName);
        }
    }

    internal class BaseMenuCommand
    {
        protected MainWindow _window;

        public BaseMenuCommand(MainWindow window)
        {
            _window = window;
        }

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }

        public virtual string CommandName()
        {
            return "BaseCommand";
        }
    }

    internal class ExitCommand: BaseMenuCommand
    {
        public ExitCommand(MainWindow window):base(window)
        { }

        override public void Execute()
        {
            _window.Close();
        }

        public override string CommandName()
        {
            return "ExitCommand";
        }
    }
}
