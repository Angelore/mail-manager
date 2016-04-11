using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailManager.Utility;
using System.Globalization;

namespace MailManager.TemplateManager
{
    public class TemplateManagerViewModel: BindableBase
    {

        public TemplateManagerViewModel()
        {
            MonthNames = GetMonthNames();
        }

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
    }
}
