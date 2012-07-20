namespace IxaCalc.Model
{
    using System.Collections.Generic;

    using IxaCalc.Enums;

    public class RankDictionary
    {
        public static Dictionary<string, LeadershipRank> rank = new Dictionary<string, LeadershipRank>
            {
                { "F", LeadershipRank.F },
                { "E", LeadershipRank.E },
                { "D", LeadershipRank.D },
                { "C", LeadershipRank.C },
                { "B", LeadershipRank.B },
                { "A", LeadershipRank.A },
                { ".S", LeadershipRank.S },
                { "SS", LeadershipRank.SS },
                { "SSS", LeadershipRank.SSS }
            };

        public static Dictionary<string, RarityRank> rarity = new Dictionary<string, RarityRank>
            {
                { "序", RarityRank.Common },
                { "上", RarityRank.Uncommon },
                { "特", RarityRank.Rare },
                { "極", RarityRank.SuperRare },
                { "天", RarityRank.UltraRare }
            };

        public static Dictionary<SoldierTypes, Soldier> soldiers = new Dictionary<SoldierTypes, Soldier>
            {
                { SoldierTypes.Lance, new Soldier("足軽", 11, 11, SoldierTypes.Lance)},
                { SoldierTypes.Bow, new Soldier("弓足軽", 10, 12, SoldierTypes.Bow)},
                { SoldierTypes.Horse, new Soldier("騎馬兵", 12, 10, SoldierTypes.Horse)},
                { SoldierTypes.LongLance, new Soldier("長槍足軽", 11, 11, SoldierTypes.LongLance)},
                { SoldierTypes.LongBow, new Soldier("長弓兵", 10, 12, SoldierTypes.LongBow)},
                { SoldierTypes.EliteHorse, new Soldier("精鋭騎馬", 12, 10, SoldierTypes.EliteHorse)},
                { SoldierTypes.Hammer, new Soldier("破壊槌", 8, 3, SoldierTypes.Hammer)},
            };
    }
}
