using System;

namespace IxaCalc.Model
{
    using System.Collections.ObjectModel;

    using IxaCalc.Enums;

    /// <summary>
    /// データサービス データを初期化するロジックを持つ
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        /// データ取得
        /// </summary>
        /// <param name="callback">コールバック</param>
        public void GetData(Action<ObservableCollection<Busho>, Exception> callback)
        {
            // Use this to connect to the actual data service
            var item = new ObservableCollection<Busho>()
                {
                    new Busho("織田信長", "天", 3500, 3.5, "s", "s", "s", "s"),
                    new Busho("本多忠勝", "極", 2790, 3.0, "s", "s", "s", "s"),
                    new Busho("上泉信綱", "特", 820, 2.0, "s", "s", "s", "s"),
                    new Busho("武田晴信", "上", 2230, 3.0, "s", "s", "s", "s"),
                    new Busho("鬼馬義直", "序", 1670, 2.0, "s", "s", "s", "s")
                };
            callback(item, null);
        }
    }
}