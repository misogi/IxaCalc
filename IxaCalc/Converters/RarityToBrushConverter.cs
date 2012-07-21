using System.Windows.Data;

namespace IxaCalc.Converters
{
    using System;
    using System.Windows.Media;
    using System.Globalization;
    using IxaCalc.Enums;

    public class RarityToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new SolidColorBrush(Color.FromArgb(255, 220, 220, 220));
            }
            var rarity = (RarityRank)value;
            Color col = Colors.Black;
            switch(rarity)
            {
                case RarityRank.Common:
                    col = Color.FromArgb(255, 130, 130, 240);
                    break;
                case RarityRank.Uncommon:
                    col = Color.FromArgb(255, 230, 230, 110);
                    break;
                case RarityRank.Rare:
                    col = Color.FromArgb(255, 230, 110, 110);
                    break;
                case RarityRank.SuperRare:
                    col = Color.FromArgb(255, 90, 90, 90);
                    break;
                case RarityRank.UltraRare:
                    col = Color.FromArgb(255, 180, 180, 180);
                    break;
            }

            var brush = new SolidColorBrush(col);
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();

        }
    }
}
