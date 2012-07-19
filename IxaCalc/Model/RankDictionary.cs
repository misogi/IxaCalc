namespace IxaCalc.Model
{
    using System.Collections.Generic;

    using IxaCalc.Enums;

    public class RankDictionary
    {
        public static Dictionary<string, LeadershipRank> rank = new Dictionary<string, LeadershipRank>
            {
                { "f", LeadershipRank.F },
                { "e", LeadershipRank.E },
                { "d", LeadershipRank.D },
                { "c", LeadershipRank.C },
                { "b", LeadershipRank.B },
                { "a", LeadershipRank.A },
                { "s", LeadershipRank.S },
                { "ss", LeadershipRank.SS },
                { "sss", LeadershipRank.SSS }
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
