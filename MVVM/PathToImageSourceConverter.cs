using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfDictionary.MVVM
{
    public class PathToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith("pack://"))
                {
                    return new BitmapImage(new Uri(path));
                }
                else if (File.Exists(path))
                {
                    return new BitmapImage(new Uri(path, UriKind.Absolute));
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
