using System;

namespace IxaCalc.Model
{
    using System.Collections.ObjectModel;

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
        void GetData(Action<ObservableCollection<Busho>, Exception> callback);
    }
}
