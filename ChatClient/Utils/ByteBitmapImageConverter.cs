using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChatClient.Utils
{
    public class ByteBitmapImageConverter
    {
        public object Covert (object value , Type targetType , object parametr, CultureInfo culture)
        {
            if (value == null)
                return null;
            var img = value as byte[];
            ImageSourceConverter converter = new ImageSourceConverter();
            var bmtSource = (BitmapImage)converter.ConvertFrom(img);
            return bmtSource;
        }
        public object ConvertBack(object value, Type targetType, object parametr, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
