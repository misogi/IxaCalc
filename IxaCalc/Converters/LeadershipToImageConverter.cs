namespace IxaCalc.Converters
{
    using System;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Globalization;
    using IxaCalc.Model;
    using IxaCalc.Enums;

    /// <summary>
    /// 統率力を画像に変換する
    /// </summary>
    public class LeadershipToImageConverter : IValueConverter
    {
        /// <summary>
        /// 値変換
        /// </summary>
        /// <param name="value">統率力 LeadershipRank</param>
        /// <param name="targetType">Type</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>画像</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rank = (LeadershipRank)value;
            return RankDictionary.leadershipImage[rank];
        }

        /// <summary>
        /// 未実装
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="targetType">タイプ</param>
        /// <param name="parameter">パラメタ</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>戻り値</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
