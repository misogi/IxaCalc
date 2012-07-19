namespace IxaCalc.Model
{
    /// <summary>
    /// 標準のデータ
    /// </summary>
    public class DataItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataItem"/> class. 
        /// コンストラクタ
        /// </summary>
        /// <param name="title">
        /// タイトル名
        /// </param>
        public DataItem(string title)
        {
            this.Title = title;
        }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title
        {
            get;
            private set;
        }
    }
}
