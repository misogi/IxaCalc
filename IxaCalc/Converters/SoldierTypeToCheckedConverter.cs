namespace IxaCalc.Converters
{
    using System;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Globalization;
    using IxaCalc.Enums;

    public class SoldierTypeToCheckedConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rarity = (SoldierTypes)value;
            var cmpRarity = (SoldierTypes)Enum.Parse(typeof(SoldierTypes), (string)parameter, false);
            if (rarity.Equals(cmpRarity))
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var soldierType = (SoldierTypes)Enum.Parse(typeof(SoldierTypes), (string)parameter, false);
            return soldierType;
        }
    }
}
