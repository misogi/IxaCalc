using System;
using IxaCalc.Model;

namespace IxaCalc.Design
{
    /// <summary>
    /// データ取得メソッド
    /// </summary>
    public class DesignDataService : IDataService
    {
        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="callback">コールバック関数</param>
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data
            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }
    }
}