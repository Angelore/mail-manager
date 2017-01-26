using MailManager.TemplateManager.Templates.ViewTemplates;
using MailManager.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailManager.TemplateManager.Templates.TemplateModels
{
    class NiFileViewModel: BindableBase
    {
        private string _source;

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
        }

        private string _recipients;
        public string Recipients
        {
            get { return _recipients; }
            set { SetProperty(ref _recipients, value); }
        }

        private string _divider = "----";
        public string Divider
        {
            get { return _divider; }
        }

        private ObservableCollection<NiFileEntry> _entries;
        public ObservableCollection<NiFileEntry> Entries
        {
            get { return _entries; }
            set { SetProperty(ref _entries, value); }
        }

        public NiFileViewModel()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var reader = new StreamReader(assembly.GetManifestResourceStream("MailManager.TemplateManager.Templates.FileTemplates.ni.txt"), Encoding.GetEncoding(866));
            var _source = reader.ReadToEnd();

            FileName = "ni" + DateTime.Today.ToString("yyMMdd") + ".svb";
            Recipients = "svb";
            Entries = new ObservableCollection<NiFileEntry>();
            Entries.Add(new NiFileEntry());

            NewEntryCommand = new RelayCommand(AddNewEntry);
        }

        public RelayCommand NewEntryCommand { get; private set; }
        public void AddNewEntry()
        {
            Entries.Add(new NiFileEntry());
        }
    }

    class NiFileEntry: BindableBase
    {
        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set { SetProperty(ref _projectName, value); }
        }

        private string _customer;
        public string Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        private DateTime _startTime;
        public DateTime _StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
        }

        private DateTime _supposedEndTime;
        public DateTime SupposedEndTime
        {
            get { return _supposedEndTime; }
            set { SetProperty(ref _supposedEndTime, value); }
        }

        private string _misc;
        public string Misc
        {
            get { return _misc; }
            set { SetProperty(ref _misc, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
    }
}
