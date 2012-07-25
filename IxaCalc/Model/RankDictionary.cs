﻿namespace IxaCalc.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using IxaCalc.Enums;

    public class RankDictionary
    {

        static RankDictionary()
        {
            rarityImage = new Dictionary<RarityRank, ImageSource>();
            leadershipImage = new Dictionary<LeadershipRank, ImageSource>();
            var strs = new string[]{"Common", "Uncommon", "Rare", "SuperRare", "UltraRare"};
            foreach(var str in strs)
            {
                var urlstr = string.Format("Images/{0}.png", str);
                var uri = new Uri(urlstr, UriKind.Relative);
                var bmp = new BitmapImage(uri);
                var type = (RarityRank)Enum.Parse(typeof(RarityRank), str, false);
                rarityImage[type] = bmp;
            }

            foreach (KeyValuePair<string, LeadershipRank> r in rank)
            {
                var urlstr = string.Format("Images/Leadership/{0}.png", r.Value.ToString());
                var uri = new Uri(urlstr, UriKind.Relative);
                var bmp = new BitmapImage(uri);
                leadershipImage[r.Value] = bmp;
            }
        }

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
                { SoldierTypes.Spear, new Soldier("足軽", 11, 11, SoldierTypes.Spear)},
                { SoldierTypes.Bow, new Soldier("弓足軽", 10, 12, SoldierTypes.Bow)},
                { SoldierTypes.Horse, new Soldier("騎馬兵", 12, 10, SoldierTypes.Horse)},
                { SoldierTypes.LongSpear, new Soldier("長槍足軽", 16, 16, SoldierTypes.LongSpear)},
                { SoldierTypes.LongBow, new Soldier("長弓兵", 15, 17, SoldierTypes.LongBow)},
                { SoldierTypes.EliteHorse, new Soldier("精鋭騎馬", 17, 15, SoldierTypes.EliteHorse)},
                { SoldierTypes.Samurai, new Soldier("武士", 18, 18, SoldierTypes.Samurai)},
                { SoldierTypes.MountArcher, new Soldier("弓騎馬", 17, 19, SoldierTypes.MountArcher)},
                { SoldierTypes.RedArms, new Soldier("赤備え", 21, 20, SoldierTypes.RedArms)},
                { SoldierTypes.Hammer, new Soldier("破壊槌", 8, 3, SoldierTypes.Hammer)},
                { SoldierTypes.Gun, new Soldier("鉄砲足軽", 18, 26, SoldierTypes.Gun)},
                { SoldierTypes.Dragoon, new Soldier("騎馬鉄砲", 26, 18, SoldierTypes.Dragoon)}
            };

        public static Dictionary<RarityRank, ImageSource> rarityImage;

        public static Dictionary<LeadershipRank, ImageSource> leadershipImage;
    }
}
