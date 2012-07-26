namespace IxaCalc.Model
{
    using System.ComponentModel;

    /// <summary>
    /// モデルの基底クラス プロパティ変更に対応
    /// </summary>
    public class ModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティ変更イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティ変更時メソッド
        /// </summary>
        /// <param name="propertyName">プロパティ名文字列</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
