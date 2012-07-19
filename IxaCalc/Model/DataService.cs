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
            var item = new ObservableCollection<Busho>();
            item.Add(new Busho { Name = "武田", Rarity = RarityRank.Common });
            item.Add(new Busho { Name = "伊達", Rarity = RarityRank.Rare });
            callback(item, null);
        }
    }
}