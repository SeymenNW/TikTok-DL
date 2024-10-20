using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTokDL.Views.Converters
{
    internal class TrueFalseTextConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is bool s)
            {
                if (s)
                {
                    return "Valid Url!✅";
                }
                else if (!s)
                {
                    return "Invalid Url!❌";
                }
            }

            return "Input..";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
