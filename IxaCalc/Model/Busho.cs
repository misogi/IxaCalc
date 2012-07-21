namespace IxaCalc.Model
{
    using System;
    using System.ComponentModel;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using IxaCalc.Enums;

    /// <summary>
    /// 武将データクラス
    /// </summary>
    public class Busho : INotifyPropertyChanged
    {

        private ImageSource _image;

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

        public int Attack { get; set; }
        public int Defence { get; set; }
        public int AttackGrowth { get; set; }
        public int DefenceGrowth { get; set; }
        public double Tactics { get; set; }
        public double TacticsGrowth { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">武将名</param>
        /// <param name="rare">レアリティ</param>
        /// <param name="soldiernum">指揮兵士数</param>
        /// <param name="lance">槍兵統率</param>
        /// <param name="horse">馬兵統率</param>
        /// <param name="bow">弓兵統率</param>
        /// <param name="weapon">兵器統率</param>
        public Busho(int id, string rare, string name, double cost, int soldiernum, string lance, string bow, string horse, string weapon, int atk , int def, double tac, int atkgrow, int defgrow, double tacgrow)
        {
            Id = id;
            Name = name;
            SoldierNumber = soldiernum;
            Cost = cost;
            Rarity = RankDictionary.rarity[rare];
            Bow = RankDictionary.rank[bow];
            Weapon = RankDictionary.rank[weapon];
            Horse = RankDictionary.rank[horse];
            Lance = RankDictionary.rank[lance];
            Attack = atk;
            Defence = def;
            Tactics = tac;
            AttackGrowth = atkgrow;
            DefenceGrowth = defgrow;
            TacticsGrowth = tacgrow;
            LoadImage();
        }

        public ImageSource Image
        {
            get
            {
                return _image;
            }
        }

        private void LoadImage()
        {
            if (Id != null)
            {
                var urlstr = string.Format("Images/{0}.jpg", Id);
                var uri = new Uri(urlstr, UriKind.Relative);
                var bmp = new BitmapImage(uri);
                _image = bmp;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
