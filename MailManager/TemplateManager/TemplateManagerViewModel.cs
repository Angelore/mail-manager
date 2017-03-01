using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailManager.Utility;
using System.Globalization;
using MailManager.TemplateManager.Templates.TemplateModels;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;

namespace MailManager.TemplateManager
{
    public class TemplateManagerViewModel: BindableBase, IDisposable, IMenuProvider
    {

        public TemplateManagerViewModel()
        {
            // MonthNames = GetMonthNames().Where(x => !string.IsNullOrEmpty(x)).ToArray();
            FileTypesList = new Dictionary<string, BindableBase>()
            {
                { "Ni", new NiFileViewModel() }
            };
            foreach (var vm in FileTypesList.Select(x => x.Value as IRenderable))
            {
                vm.RenderRequest += FileViewModel_RenderRequest;
            }
            SelectedFileType = FileTypesList.First();
            FileViewModel_RenderRequest(SelectedFileType.Value, null);

            CopyToClipboardCommand = new RelayCommand<TextBlock>(CopyViewTextBlockToClipboard);
            SaveToFileCommand = new RelayCommand(SaveToFile);
        }
        
        private void FileViewModel_RenderRequest(object sender, EventArgs e)
        {
            RenderedView = (sender as IRenderable)?.RenderThis();            
        }

        private Dictionary<string, BindableBase> _fileTypesList;
        public Dictionary<string, BindableBase> FileTypesList
        {
            get { return _fileTypesList; }
            set { SetProperty(ref _fileTypesList, value); }
        }

        private KeyValuePair<string, BindableBase> _selectedFileType;
        public KeyValuePair<string, BindableBase> SelectedFileType
        {
            get { return _selectedFileType; }
            set { SetProperty(ref _selectedFileType, value); }
        }

        private string _renderedView;
        public string RenderedView
        {
            get { return _renderedView; }
            set { SetProperty(ref _renderedView, value); }
        }

        public RelayCommand<TextBlock> CopyToClipboardCommand { get; set; }
        public void CopyViewTextBlockToClipboard(TextBlock textBlock)
        {
            Clipboard.SetText(textBlock.Text);
        }

        public RelayCommand SaveToFileCommand { get; set; }
        private void SaveToFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, RenderedView, Encoding.GetEncoding(866));
            }
        }

        #region IMenuProvider
        public string MenuItemHeader { get; set; } = "_Template Manager";
        public MenuItemViewModel GetMenuItem()
        {
            return new MenuItemViewModel()
            {
                Header = MenuItemHeader, //Utility.Localization.Get("MainWindowMenuHelp"),
                Children = new List<MenuItemViewModel>()
                {
                    new MenuItemViewModel()
                    {
                        Header = Utility.Localization.Get("MainWindowMenuAbout"),
                        Command = SaveToFileCommand,
                        IconName = "FileText",
                        IconSize = 12
                    }
                }
            };
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            foreach (var vm in FileTypesList.Where(x => x.Value is IRenderable).Select(x => x.Value as IRenderable))
            {
                vm.RenderRequest -= FileViewModel_RenderRequest;
            }
        }
        #endregion

        #region MonthNames
        private string[] _monthNames;
        public string[] MonthNames
        {
            get
            {
                return _monthNames;
            }
            set
            {
                SetProperty(ref _monthNames, value);
            }
        }

        private int _selectedMonth;
        public int SelectedMonth
        {
            get
            {
                return _selectedMonth;
            }
            set
            {
                SetProperty(ref _selectedMonth, value);
            }
        }

        /// <summary>
        /// Returns an array of month names for specified culture.
        /// If nothing is passed, defaults to current culture. Will contain 13 entries, the last one being an empty string for the cultures that use only 12 months.
        /// </summary>
        /// <param name="cultureInfo">A CultureInfo object to be used for getting names</param>
        /// <returns></returns>
        public string[] GetMonthNames(CultureInfo cultureInfo = null)
        {
            var culture = cultureInfo ?? CultureInfo.CurrentCulture;
            return (culture).DateTimeFormat.MonthNames;
        }
        #endregion
    }
}
