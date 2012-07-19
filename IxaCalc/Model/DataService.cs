using System;

namespace IxaCalc.Model
{
    /// <summary>
    /// データサービス データを初期化するロジックを持つ
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        /// データ取得
        /// </summary>
        /// <param name="callback">コールバック</param>
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service
            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }
    }
}