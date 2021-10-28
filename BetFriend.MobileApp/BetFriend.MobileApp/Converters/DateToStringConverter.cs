using System;
using System.Globalization;
using Xamarin.Forms;

namespace BetFriend.MobileApp.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime dt))
            {
                if (bool.Parse(parameter.ToString()))
                    return string.Format("{0} {1}", dt.ToLocalTime().ToLongDateString(), dt.ToLocalTime().ToShortTimeString());
                else
                    return string.Format("{0} {1}", dt.ToLocalTime().ToShortDateString(), dt.ToLocalTime().ToShortTimeString());
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
