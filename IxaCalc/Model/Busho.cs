namespace IxaCalc.Model
{
    using System;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using IxaCalc.Enums;

    /// <summary>
    /// 武将データクラス
    /// </summary>
    public class Busho : ModelBase
    {
        /// <summary>
        /// カード画像
        /// </summary>
        private ImageSource _image;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">武将ID</param>
        /// <param name="rare">レアリティ</param>
        /// <param name="name">武将名</param>
        /// <param name="cost">コスト</param>
        /// <param name="soldiernum">指揮兵士数</param>
        /// <param name="lance">槍兵統率</param>
        /// <param name="bow">弓兵統率</param>
        /// <param name="horse">馬兵統率</param>
        /// <param name="weapon">兵器統率</param>
        /// <param name="atk">攻撃力</param>
        /// <param name="def">防御力</param>
        /// <param name="tac">兵法</param>
        /// <param name="atkgrow">攻撃成長</param>
        /// <param name="defgrow">防御成長</param>
        /// <param name="tacgrow">兵法成長</param>
        public Busho(int id, string rare, string name, double cost, int soldiernum, string lance, string bow, string horse, string weapon, int atk, int def, double tac, int atkgrow, int defgrow, double tacgrow)
        {
            Id = id;
            Name = name;
            SoldierNumber = soldiernum;
            Cost = cost;
            Rarity = RankDictionary.Rarity[rare];
            this.RarityImage = RankDictionary.RarityImage[this.Rarity];
            Bow = RankDictionary.Rank[bow];
            Weapon = RankDictionary.Rank[weapon];
            Horse = RankDictionary.Rank[horse];
            Lance = RankDictionary.Rank[lance];
            Attack = atk;
            Defence = def;
            Tactics = tac;
            AttackGrowth = atkgrow;
            DefenceGrowth = defgrow;
            TacticsGrowth = tacgrow;
            LoadImage();
        }

        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// レア度
        /// </summary>
        public RarityRank Rarity { get; set; }

        public ImageSource RarityImage {get; set;}

        /// <summary>
        /// 指揮兵士数
        /// </summary>
        public int SoldierNumber { get; set; }

        /// <summary>
        /// コスト
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// 槍兵統率
        /// </summary>
        public LeadershipRank Lance { get; set; }

        /// <summary>
        /// 馬兵統率
        /// </summary>
        public LeadershipRank Horse { get; set; }

        /// <summary>
        /// 弓兵統率
        /// </summary>
        public LeadershipRank Bow { get; set; }

        /// <summary>
        /// 兵器統率
        /// </summary>
        public LeadershipRank Weapon { get; set; }

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// 防御力
        /// </summary>
        public int Defence { get; set; }

        /// <summary>
        /// 攻撃力成長
        /// </summary>
        public int AttackGrowth { get; set; }

        /// <summary>
        /// 防御力成長
        /// </summary>
        public int DefenceGrowth { get; set; }

        /// <summary>
        /// 兵法
        /// </summary>
        public double Tactics { get; set; }

        /// <summary>
        /// 兵法成長
        /// </summary>
        public double TacticsGrowth { get; set; }

        /// <summary>
        /// カード画像
        /// </summary>
        public ImageSource Image
        {
            get
            {
                return _image;
            }
        }

        #endregion
        /// <summary>
        /// カード画像を読み込む
        /// </summary>
        private void LoadImage()
        {
            var urlstr = string.Format("Images/{0}.jpg", Id);
            var uri = new Uri(urlstr, UriKind.Relative);
            var bmp = new BitmapImage(uri);
            _image = bmp;
        }
    }
}
