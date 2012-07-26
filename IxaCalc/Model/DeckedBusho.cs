namespace IxaCalc.Model
{
    using System.Windows.Media;

    using IxaCalc.Enums;

    /// <summary>
    /// デッキに入った武将 武将DBにはない固有データを持つ
    /// </summary>
    public class DeckedBusho : ModelBase
    {
        /// <summary>
        /// 武将データ
        /// </summary>
        private Busho _originBusho;

        /// <summary>
        /// レベルアップ含む武将攻撃力
        /// </summary>
        private int _bushoAttack;

        /// <summary>
        /// レベルアップ含む武将防御
        /// </summary>
        private int _bushoDefence;

        /// <summary>
        /// 兵士含む攻撃力
        /// </summary>
        private double _actualAttack;

        /// <summary>
        /// 兵士含む防御力
        /// </summary>
        private double _actualDefence;

        /// <summary>
        /// 攻撃バーの長さ
        /// </summary>
        private double _actualAttackBar;

        /// <summary>
        /// 防御バーの長さ
        /// </summary>
        private double _actualDefenceBar;

        /// <summary>
        /// コスト1あたり防御
        /// </summary>
        private double _actualDefencePerCost;

        /// <summary>
        /// ランク
        /// </summary>
        private int _rank;

        /// <summary>
        /// 現在兵種の統率
        /// </summary>
        private LeadershipRank _actualLeadership1;

        /// <summary>
        /// 現在兵種の統率 複数兵科の場合
        /// </summary>
        private LeadershipRank _actualLeadership2;

        /// <summary>
        /// 攻撃時の実指揮
        /// </summary>
        private double _actualLeadershipAttack;

        /// <summary>
        /// コスト1あたり攻撃
        /// </summary>
        private double _perCostAttack;

        /// <summary>
        /// 防御時実指揮
        /// </summary>
        private double _actualLeadershipDefence;

        /// <summary>
        /// コストあたり防御力
        /// </summary>
        private double _perCostDefence;

        /// <summary>
        /// 指揮力補正
        /// </summary>
        private double _percent;

        /// <summary>
        /// 現在の兵種
        /// </summary>
        private SoldierTypes _currentSoldierType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="origin">武将データベースの武将</param>
        /// <param name="type">現在の兵種</param>
        public DeckedBusho(Busho origin, SoldierTypes type)
        {
            _originBusho = origin;
            _currentSoldierType = type;
            _rank = 0;
            this.UpdateDeckedInfo();
        }
        #region メソッド
        /// <summary>
        /// デッキ上に表示する情報を更新
        /// </summary>
        private void UpdateDeckedInfo()
        {
            Percent = CalculatePercent(_originBusho);
            BushoAttack = _originBusho.Attack + (_originBusho.AttackGrowth * RankToBonusPoint(Rank));
            BushoDefence = _originBusho.Defence + (_originBusho.DefenceGrowth * RankToBonusPoint(Rank));
            ActualAttack = (BushoAttack + (_originBusho.SoldierNumber * RankDictionary.soldiers[CurrentSoldierType].Attack)) * Percent;
            ActualDefence = (BushoDefence + (_originBusho.SoldierNumber * RankDictionary.soldiers[CurrentSoldierType].Defence)) * Percent;
            ActualDefencePerCost = ActualDefence / _originBusho.Cost;
            ActualAttackBar = ActualAttack > 100000 ? 1.0 : ActualAttack / 100000;
            ActualLeadershipAttack = ActualAttack / RankDictionary.soldiers[CurrentSoldierType].Attack;
            ActualLeadershipDefence = ActualDefence / RankDictionary.soldiers[CurrentSoldierType].Defence;
            PerCostAttack = ActualLeadershipAttack / _originBusho.Cost;
            PerCostDefence = ActualLeadershipDefence / _originBusho.Cost;
            ActualDefenceBar = ActualDefencePerCost > 40000 ? 1.0 : ActualDefencePerCost / 40000;
        }

        /// <summary>
        /// 現在のランクでのステータス上昇値を計算
        /// </summary>
        /// <param name="rank">ランク</param>
        /// <returns>ボーナス値</returns>
        private int RankToBonusPoint(int rank)
        {
            int point = 80 * (Rank + 1);

            if (Rank >= 2)
            {
                point += (Rank - 1) * 20;
            }

            if (Rank >= 4)
            {
                point += (Rank - 3) * 20;
            }

            return point;
        }

        /// <summary>
        /// 統率力補正と該当兵種の統率を計算
        /// TODO persentageを1st,2ndから計算
        /// </summary>
        /// <param name="busho">該当武将</param>
        /// <returns>統率力補正</returns>
        private double CalculatePercent(Busho busho)
        {
            var type = _currentSoldierType;
            double percentage = 1.0;
            if (type == SoldierTypes.Spear || type == SoldierTypes.LongSpear)
            {
                percentage = RankToPercentage(busho.Lance) + (Rank * 0.05);
                ActualLeadershipFirst = AddRankLeadership(busho.Lance, Rank);
                ActualLeadershipSecond = LeadershipRank.Nothing;
            }
            else if (type == SoldierTypes.Bow || type == SoldierTypes.LongBow)
            {
                percentage = RankToPercentage(busho.Bow) + (Rank * 0.05);
                ActualLeadershipFirst = AddRankLeadership(busho.Bow, Rank);
                ActualLeadershipSecond = LeadershipRank.Nothing;
            }
            else if (type == SoldierTypes.Horse || type == SoldierTypes.EliteHorse)
            {
                percentage = RankToPercentage(busho.Horse) + (Rank * 0.05);
                ActualLeadershipFirst = AddRankLeadership(busho.Horse, Rank);
                ActualLeadershipSecond = LeadershipRank.Nothing;
            }
            else if (type == SoldierTypes.Hammer)
            {
                percentage = RankToPercentage(busho.Weapon) + (Rank * 0.05);
                ActualLeadershipFirst = AddRankLeadership(busho.Weapon, Rank);
                ActualLeadershipSecond = LeadershipRank.Nothing;
            }
            else if (type == SoldierTypes.MountArcher)
            {
                percentage = (RankToPercentage(busho.Horse) + RankToPercentage(busho.Bow) + (Rank * 0.05)) / 2;
                ActualLeadershipFirst = AddRankLeadership(busho.Bow, Rank);
                ActualLeadershipSecond = busho.Horse;
            }
            else if (type == SoldierTypes.RedArms)
            {
                percentage = (RankToPercentage(busho.Horse) + RankToPercentage(busho.Lance) + (Rank * 0.05)) / 2;
                ActualLeadershipFirst = AddRankLeadership(busho.Horse, Rank);
                ActualLeadershipSecond = busho.Lance;
            }
            else if (type == SoldierTypes.Samurai)
            {
                percentage = (RankToPercentage(busho.Bow) + RankToPercentage(busho.Lance) + (Rank * 0.05)) / 2;
                ActualLeadershipFirst = AddRankLeadership(busho.Lance, Rank);
                ActualLeadershipSecond = busho.Bow;
            }
            else if (type == SoldierTypes.Gun)
            {
                percentage = (RankToPercentage(busho.Weapon) + RankToPercentage(busho.Lance) + (Rank * 0.05)) / 2;
                ActualLeadershipFirst = AddRankLeadership(busho.Weapon, Rank);
                ActualLeadershipSecond = busho.Lance;
            }
            else if (type == SoldierTypes.Dragoon)
            {
                percentage = (RankToPercentage(busho.Weapon) + RankToPercentage(busho.Horse) + (Rank * 0.05)) / 2;
                ActualLeadershipFirst = AddRankLeadership(busho.Weapon, Rank);
                ActualLeadershipSecond = busho.Horse;
            }

            if (percentage >= 1.20)
            {
                percentage = 1.20;
            }

            return percentage;
        }

        /// <summary>
        /// 統率力をランクに従い足し算する
        /// </summary>
        /// <param name="rank">武将が持つ統率</param>
        /// <param name="up">ランクアップ数</param>
        /// <returns>ランクアップ後統率</returns>
        private LeadershipRank AddRankLeadership(LeadershipRank rank, int up)
        {
            int upedRank = (int)rank + up;
            return (LeadershipRank)upedRank > LeadershipRank.SSS ? LeadershipRank.SSS : (LeadershipRank)upedRank;
        }

        /// <summary>
        /// ランクアップ
        /// </summary>
        public void RankUp()
        {
            if (_rank < 5)
            {
                Rank += 1;
                this.UpdateDeckedInfo();
            }
        }

        /// <summary>
        /// ランクダウン
        /// </summary>
        public void RankDown()
        {
            if (_rank > 0)
            {
                Rank -= 1;
                this.UpdateDeckedInfo();
            }
        }

        /// <summary>
        /// ランクを統率補正に変換
        /// </summary>
        /// <param name="rank">ランク</param>
        /// <returns>統率補正 (倍)</returns>
        private double RankToPercentage(LeadershipRank rank)
        {
            switch (rank)
            {
                case LeadershipRank.F:
                    return 0.8;
                case LeadershipRank.E:
                    return 0.85;
                case LeadershipRank.D:
                    return 0.9;
                case LeadershipRank.C:
                    return 0.95;
                case LeadershipRank.B:
                    return 1.0;
                case LeadershipRank.A:
                    return 1.05;
                case LeadershipRank.S:
                    return 1.1;
                case LeadershipRank.SS:
                    return 1.15;
                case LeadershipRank.SSS:
                    return 1.20;
                default:
                    return 0;
            }
        }
#endregion

        #region プロパティ

        /// <summary>
        /// コスト1あたり防御
        /// </summary>
        public double PerCostDefence
        {
            get
            {
                return _perCostDefence;
            }

            set
            {
                if (_perCostDefence != value)
                {
                    _perCostDefence = value;
                    OnPropertyChanged("PerCostDefence");
                }
            }
        }

        /// <summary>
        /// コスト1あたり攻撃
        /// </summary>
        public double PerCostAttack
        {
            get
            {
                return _perCostAttack;
            }

            set
            {
                if (_perCostAttack != value)
                {
                    _perCostAttack = value;
                    OnPropertyChanged("PerCostAttack");
                }
            }
        }

        /// <summary>
        /// 統率力補正
        /// </summary>
        public double Percent
        {
            get
            {
                return _percent;
            }

            set
            {
                if (_percent != value)
                {
                    _percent = value;
                    OnPropertyChanged("Percent");
                }
            }
        }

        /// <summary>
        /// 攻撃時の武将攻撃を足した実指揮兵数
        /// </summary>
        public double ActualLeadershipAttack
        {
            get
            {
                return _actualLeadershipAttack;
            }

            set
            {
                if (_actualLeadershipAttack != value)
                {
                    _actualLeadershipAttack = value;
                    OnPropertyChanged("ActualLeadershipAttack");
                }
            }
        }

        /// <summary>
        /// 防御時の実指揮数
        /// </summary>
        public double ActualLeadershipDefence
        {
            get
            {
                return _actualLeadershipDefence;
            }

            set
            {
                if (_actualLeadershipDefence != value)
                {
                    _actualLeadershipDefence = value;
                    OnPropertyChanged("ActualLeadershipDefence");
                }
            }
        }

        /// <summary>
        /// 兵士も足した最終的な防御力
        /// </summary>
        public double ActualDefence
        {
            get
            {
                return _actualDefence;
            }

            set
            {
                if (_actualDefence != value)
                {
                    _actualDefence = value;
                    OnPropertyChanged("ActualDefence");
                }
            }
        }

        /// <summary>
        /// コスト1あたりの総防御力
        /// </summary>
        public double ActualDefencePerCost
        {
            get
            {
                return _actualDefencePerCost;
            }

            set
            {
                if (_actualDefencePerCost != value)
                {
                    _actualDefencePerCost = value;
                    OnPropertyChanged("ActualDefencePerCost");
                }
            }
        }

        /// <summary>
        /// 兵士アリ統率補正アリ攻撃力
        /// </summary>
        public double ActualAttack
        {
            get
            {
                return _actualAttack;
            }

            set
            {
                if (_actualAttack != value)
                {
                    _actualAttack = value;
                    OnPropertyChanged("ActualAttack");
                }
            }
        }

        /// <summary>
        /// 現在の兵種の統率
        /// </summary>
        public LeadershipRank ActualLeadershipFirst
        {
            get
            {
                return _actualLeadership1;
            }

            set
            {

                if (_actualLeadership1 != value)
                {
                    _actualLeadership1 = value;
                    OnPropertyChanged("ActualLeadershipFirst");
                }
            }
        }

        /// <summary>
        /// 複数統率の兵種の場合、２番目の兵種の統率 複数でなければNothingが返る
        /// </summary>
        public LeadershipRank ActualLeadershipSecond
        {
            get
            {
                return _actualLeadership2;
            }

            set
            {

                if (_actualLeadership2 != value)
                {
                    _actualLeadership2 = value;
                    OnPropertyChanged("ActualLeadershipSecond");
                }
            }
        }

        /// <summary>
        /// 防御力ゲージの長さ (0.0~1.0)
        /// </summary>
        public double ActualDefenceBar
        {
            get
            {
                return _actualDefenceBar;
            }

            set
            {
                if (_actualDefenceBar != value)
                {
                    _actualDefenceBar = value;
                    OnPropertyChanged("ActualDefenceBar");
                }
            }
        }

        /// <summary>
        /// 攻撃力ゲージの長さ (0.0~1.0)
        /// </summary>
        public double ActualAttackBar
        {
            get
            {
                return _actualAttackBar;
            }

            set
            {
                if (_actualAttackBar != value)
                {
                    _actualAttackBar = value;
                    OnPropertyChanged("ActualAttackBar");
                }
            }
        }

        /// <summary>
        /// 武将攻撃力（レベル・ランク成長分込み）
        /// </summary>
        public int BushoAttack
        {
            get
            {
                return _bushoAttack;
            }

            set
            {
                if (_bushoAttack != value)
                {
                    _bushoAttack = value;
                    OnPropertyChanged("BushoAttack");
                }
            }
        }

        /// <summary>
        /// 武将防御力（レベル・ランク成長分込み）
        /// </summary>
        public int BushoDefence
        {
            get
            {
                return _bushoDefence;
            }

            set
            {
                if (_bushoDefence != value)
                {
                    _bushoDefence = value;
                    OnPropertyChanged("BushoDefence");
                }
            }
        }

        /// <summary>
        /// ランクアップのランク
        /// </summary>
        public int Rank
        {
            get
            {
                return _rank;
            }

            set
            {
                if (_rank != value)
                {
                    _rank = value;
                    OnPropertyChanged("Rank");
                }
            }
        }

        /// <summary>
        /// 現在統率している兵種
        /// </summary>
        public SoldierTypes CurrentSoldierType
        {
            get
            {
                return _currentSoldierType;
            }

            set
            {
                if (_currentSoldierType != value)
                {
                    _currentSoldierType = value;
                    this.UpdateDeckedInfo();
                    OnPropertyChanged("CurrentSoldierType");
                }
            }
        }

        /// <summary>
        /// 武将データベースのデータ
        /// </summary>
        public Busho OriginBusho
        {
            get
            {
                return _originBusho;
            }
        }

        /// <summary>
        /// レア度画像 TODO コンバータに変えること
        /// </summary>
        public ImageSource RarityImage
        {
            get
            {
                return RankDictionary.rarityImage[OriginBusho.Rarity];
            }
        }
        #endregion
    }
}
