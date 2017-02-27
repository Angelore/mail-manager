using System.Windows.Controls;

namespace MailManager
{
    interface IMenuProvider
    {
        MenuItemViewModel GetMenuItem();
        string MenuItemHeader { get; set; }
    }
}
