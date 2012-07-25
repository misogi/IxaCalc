namespace IxaCalc.Converters
{
    using System;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Globalization;
    using IxaCalc.Model;
    using IxaCalc.Enums;

    public class LeadershipToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rank = (LeadershipRank)value;
            return RankDictionary.leadershipImage[rank];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();

        }

    }
}
