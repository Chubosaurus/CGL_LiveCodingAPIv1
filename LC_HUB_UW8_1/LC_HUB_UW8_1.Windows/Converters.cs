using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;          
namespace LC_HUB_UW8_1
{
    public class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string string_value = value as string;
            BitmapImage thumb = new BitmapImage(new Uri(string_value));
            return thumb;                
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
