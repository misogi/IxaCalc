﻿namespace IxaCalc.Model
{
    using IxaCalc.Enums;

    /// <summary>
    /// 武将データクラス
    /// </summary>
    public class Busho
    {
        private SoldierTypes _soldierType;

        private double _allAttack;

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
        public Busho(string name, string rare, int soldiernum, double cost, string lance, string horse, string bow, string weapon)
        {
            Name = name;
            SoldierNumber = soldiernum;
            Cost = cost;
            Rarity = RankDictionary.rarity[rare];
            Bow = RankDictionary.rank[bow];
            Weapon = RankDictionary.rank[weapon];
            Horse = RankDictionary.rank[horse];
            Lance = RankDictionary.rank[lance];
        }

        public Busho(int id, string rare, string name, double cost, int soldiernum, string lance, string bow, string horse, string weapon)
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
        }

        public string CurrentLeadership
        {
            get
            {
                switch (_soldierType)
                {
                    case SoldierTypes.Lance:
                        return Lance.ToString();
                    case SoldierTypes.Bow:
                        return Bow.ToString();
                    case SoldierTypes.Horse:
                        return Horse.ToString();
                    default:
                        return "null";
                }
            }
        }

        public SoldierTypes CurrentSoldierType
        {
            set
            {
                _soldierType = value;
                var soldier = RankDictionary.soldiers[_soldierType];
                LeadershipRank rank;
                switch (_soldierType)
                {
                    case SoldierTypes.Lance:
                        rank = Lance;
                        break;
                    case SoldierTypes.Bow:
                        rank = Bow;
                        break;
                    case SoldierTypes.Horse:
                        rank = Horse;
                        break;
                    default:
                        rank = Lance;
                        break;
                }
                AllAttack = SoldierNumber * soldier.Attack * RankToPercentage(rank);
            }
        }

        public double AllAttack
        {

            get
            {
                return _allAttack;
            }

            set
            {
                if (_allAttack == value)
                {
                    return;
                }

                _allAttack = value;
            }
        }

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
                default:
                    return 1.0;
            }
        }
    }
}
