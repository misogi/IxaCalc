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
                    col = Color.FromArgb(255, 70, 70, 140);
                    break;
                case RarityRank.Uncommon:
                    col = Color.FromArgb(255, 150, 150, 80);
                    break;
                case RarityRank.Rare:
                    col = Color.FromArgb(255, 140, 80, 80);
                    break;
                case RarityRank.SuperRare:
                    col = Color.FromArgb(255, 30, 30, 30);
                    break;
                case RarityRank.UltraRare:
                    col = Color.FromArgb(255, 120, 120, 120);
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
