namespace IxaCalc.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using IxaCalc.Enums;

    /// <summary>
    /// レアリティをボタンのChecked状態に変換
    /// </summary>
    public class RarityToCheckedConverter : IValueConverter
    {
        /// <summary>
        /// レアリティをショートカットボタンのChecked状態に変換
        /// </summary>
        /// <param name="value">レアリティ RarityRank型</param>
        /// <param name="targetType">タイプ</param>
        /// <param name="parameter">そのボタンが担当するレアリティタイプ文字列（enumに変換できる値にすること）</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>ボタンの状態</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rarity = (RarityRank)value;
            var cmpRarity = (RarityRank)Enum.Parse(typeof(RarityRank), (string)parameter, false);
            if (rarity.Equals(cmpRarity))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ボタンの状態をレアリティに読み替える
        /// </summary>
        /// <param name="value">ボタンの状態</param>
        /// <param name="targetType">タイプ</param>
        /// <param name="parameter">ボタンが担当するレアリティタイプ 文字列でenumに変換できること</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>レアリティ</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rarity = (RarityRank)Enum.Parse(typeof(RarityRank), (string)parameter, false);
            return rarity;
        }
    }
}
