using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailManager.Utility
{
    public static class Localization
    {
        public enum Cultures { en }

        private static Dictionary<Cultures, string> ResourceFiles = new Dictionary<Cultures, string>
        {
            { Cultures.en, "MailManager.Resources.Strings" }
        };
            
		private static ResourceManager ResourceManager = new ResourceManager(ResourceFiles[Culture], Assembly.GetExecutingAssembly());

        private static Cultures _uiCulture = Cultures.en;
        
        public static Cultures Culture
        {
            get { return _uiCulture; }
            set
            {
                _uiCulture = value;
                InitializeResourceManager(value);
            }
        }

        public static void InitializeResourceManager(Cultures culture)
        {
            ResourceManager = new ResourceManager(ResourceFiles[Culture], Assembly.GetExecutingAssembly());
        }

        public static string Get(string name)
        {
            return ResourceManager.GetString(name);
        }
    }

    [ValueConversion(typeof(object), typeof(string))]
    public class LocalizationConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Localization.Get((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
