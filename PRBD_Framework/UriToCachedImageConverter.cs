using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace PRBD_Framework {
    // see: http://www.thejoyofcode.com/WPF_Image_element_locks_my_local_file.aspx
    public class UriToCachedImageConverter : MarkupExtension, IValueConverter {
        private static List<string> _cache = new List<string>();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || !File.Exists(value.ToString()))
                return null;
            var path = value.ToString();
            if (!string.IsNullOrEmpty(path)) {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(path);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                if (!_cache.Contains(value.ToString())) {
                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    _cache.Add(path);
                }
                bi.EndInit();
                bi.Freeze();
                return bi;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException("Two way conversion is not supported.");
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        internal static void ClearCache() {
            _cache.Clear();
        }

        internal static void ClearCache(string newPath) {
            _cache.Remove(newPath);
        }
    }
}
