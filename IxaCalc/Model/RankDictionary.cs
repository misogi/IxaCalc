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
    }
}
