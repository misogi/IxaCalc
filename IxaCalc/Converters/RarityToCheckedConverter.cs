namespace IxaCalc.Converters
{
    using System;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Globalization;
    using IxaCalc.Enums;

    public class RarityToCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rarity = (RarityRank)value;
            var cmpRarity = (RarityRank)Enum.Parse(typeof(RarityRank),(string)parameter, false);
            if (rarity.Equals(cmpRarity))
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();

        }
    }
}
