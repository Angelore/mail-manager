using MailManager.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MailManager.Services
{
    public class SettingsService: BindableBase, IDisposable
    {
        private const string PathToSettings = "settings.xml";

        #region Singleton
        private static SettingsService instance = null;
        public static SettingsService Instance
        {
            get
            {
                if (instance == null)
                    instance = new SettingsService();
                return instance;
            }
        }
        private SettingsService()
        {
            LoadSettingsFromFile();
        }
        #endregion

        public object SettingsViewModel { get; set; }

        private void LoadSettingsFromFile()
        {
            if (File.Exists(PathToSettings))
            {
                // read settings from file

                // dont forget corrupted settings file
            }
            else
            {
                // init settingsviewmodel
            }
        }

        private void SaveSettingsToFile()
        {
            // save settings
        }

        #region IDisposable
        public void Dispose()
        {
            SaveSettingsToFile();
        }
        #endregion

        ~SettingsService()
        {
            SaveSettingsToFile();
        }
    }
}
