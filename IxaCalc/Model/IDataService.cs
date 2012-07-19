using System;

namespace IxaCalc.Model
{
    /// <summary>
    /// 初期データ取得用インタフェース
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="callback">
        /// エラー時に使うコールバック
        /// </param>
        void GetData(Action<DataItem, Exception> callback);
    }
}
