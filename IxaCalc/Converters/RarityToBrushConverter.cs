namespace IxaCalc.Converters
{
    using System;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Globalization;
    using IxaCalc.Enums;

    /// <summary>
    /// レアリティを背景色に変換するコンバータ
    /// </summary>
    public class RarityToBrushConverter : IValueConverter
    {
        /// <summary>
        /// レアリティを背景色に変換
        /// </summary>
        /// <param name="value">レアリティ RarityRank型</param>
        /// <param name="targetType">タイプ</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>背景色用ブラシ</returns>
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
                    col = Color.FromArgb(255, 40, 40, 100);
                    break;
                case RarityRank.Uncommon:
                    col = Color.FromArgb(255, 120, 120, 60);
                    break;
                case RarityRank.Rare:
                    col = Color.FromArgb(255, 100, 50, 50);
                    break;
                case RarityRank.SuperRare:
                    col = Color.FromArgb(255, 40, 40, 40);
                    break;
                case RarityRank.UltraRare:
                    col = Color.FromArgb(255, 110, 110, 112);
                    break;
            }

            var brush = new SolidColorBrush(col);
            return brush;
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
