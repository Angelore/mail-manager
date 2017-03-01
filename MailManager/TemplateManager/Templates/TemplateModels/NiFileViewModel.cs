using MailManager.TemplateManager.Templates.ViewTemplates;
using MailManager.Utility;
using Nustache.Core;
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
    class NiFileViewModel: BindableBase, IRenderable, IDisposable
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
            _source = reader.ReadToEnd();

            FileName = "ni" + DateTime.Today.ToString("yyMMdd") + ".svb";
            Recipients = "svb";
            Entries = new ObservableCollection<NiFileEntry>();
            AddNewEntry();

            NewEntryCommand = new RelayCommand(AddNewEntry);
            RemoveEntryCommand = new RelayCommand<NiFileEntry>(RemoveEntry);
        }

        public RelayCommand NewEntryCommand { get; private set; }
        public void AddNewEntry()
        {
            var newEntry = new NiFileEntry();
            newEntry.ProjectName = "xxxx";
            newEntry.Misc = "N EV TRA";
            newEntry.Customer = "D: !_gs;";

            Entries.Add(newEntry);
            newEntry.PropertyChanged += NiFileEntry_PropertyChanged;
            NiFileEntry_PropertyChanged(null, null);
        }

        private void NiFileEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RenderRequest?.Invoke(this, null);
        }

        public RelayCommand<NiFileEntry> RemoveEntryCommand { get; private set; }
        public void RemoveEntry(NiFileEntry entry)
        {
            entry.PropertyChanged -= NiFileEntry_PropertyChanged;
            Entries.Remove(entry);
            NiFileEntry_PropertyChanged(null, null);
        }

        #region IRenderable
        public event EventHandler RenderRequest;

        public string RenderThis()
        {
            // By default, Nustache converts the string to html encoding, which makes russian text unreadable.
            // Since I don't need html encoding anyway, I change default encoder to simply return input string
            // https://github.com/jdiamond/Nustache/blob/master/Nustache.Core/Encoders.cs
            return Render.StringToString(_source, this, new RenderContextBehaviour() { HtmlEncoder = text => text });

            // Left as an example of another approach
            // return System.Net.WebUtility.HtmlDecode(Render.StringToString(_source, this));
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            foreach(var e in Entries)
            {
                e.PropertyChanged -= NiFileEntry_PropertyChanged;
            }
        }
        #endregion
    }

    class NiFileEntry: BindableBase
    {
        public const string ShortTimeFormat = "HH:mm";

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
        public DateTime StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }
        public string StartTimeF { get { return StartTime.ToString(ShortTimeFormat); } }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
        }
        public string EndTimeF { get { return EndTime.ToString(ShortTimeFormat); } }

        private DateTime _supposedEndTime;
        public DateTime SupposedEndTime
        {
            get { return _supposedEndTime; }
            set { SetProperty(ref _supposedEndTime, value); }
        }
        public string SupposedEndTimeF { get { return SupposedEndTime.ToString(ShortTimeFormat); } }

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
