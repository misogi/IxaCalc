namespace IxaCalc.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using IxaCalc.Enums;

    /// <summary>
    /// 兵種をメニューボタンの状態に変換
    /// </summary>
    public class SoldierTypeToCheckedConverter : IValueConverter
    {
        /// <summary>
        /// 兵種をメニューボタンの状態に変換
        /// </summary>
        /// <param name="value">兵種 SoldierType型</param>
        /// <param name="targetType">タイプ</param>
        /// <param name="parameter">ボタンが担当する兵種 文字列でenumに変換できること</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>ボタンの状態</returns>
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

        /// <summary>
        /// メニューボタンの状態を兵種に変更
        /// </summary>
        /// <param name="value">ボタンの状態</param>
        /// <param name="targetType">タイプ</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>ボタンの状態</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var soldierType = (SoldierTypes)Enum.Parse(typeof(SoldierTypes), (string)parameter, false);
            return soldierType;
        }
    }
}
