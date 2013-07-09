namespace IxaCalc.Model
{
    using System;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using IxaCalc.Enums;

    /// <summary>
    /// 兵士ステータス
    /// </summary>
    public class Soldier
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">兵種名</param>
        /// <param name="atk">攻撃力</param>
        /// <param name="def">防御力</param>
        /// <param name="type">兵士タイプenum</param>
        public Soldier(string name, int atk, int def, SoldierTypes type)
        {
            this.Attack = atk;
            this.Defence = def;
            this.Name = name;
            this.SoldierType = type;
            this.Icon = this.LoadImage();
        }

        #region プロパティ

        /// <summary>
        /// 兵士タイプ
        /// </summary>
        public SoldierTypes SoldierType { get; set; }

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// 防御力
        /// </summary>
        public int Defence { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        public ImageSource Icon { get; private set; }

        #endregion

        /// <summary>
        /// カード画像を読み込む
        /// </summary>
        private ImageSource LoadImage()
        {
            var typeName = this.SoldierType.ToString();
            var urlstr = string.Format("Images/{0}.png", typeName);
            var uri = new Uri(urlstr, UriKind.Relative);
            var bmp = new BitmapImage(uri);

            return bmp;
        }
    }
}
