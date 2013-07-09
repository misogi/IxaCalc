namespace IxaCalc.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using IxaCalc.Enums;

    /// <summary>
    /// マスタデータ辞書
    /// </summary>
    public class RankDictionary
    {
        /// <summary>
        /// レアリティ文字列からレアリティを取得
        /// </summary>
        private static Dictionary<string, RarityRank> rarity;

        /// <summary>
        /// レアリティから画像を取得
        /// </summary>
        private static Dictionary<RarityRank, ImageSource> rarityImage;

        /// <summary>
        /// 統率力ランクから画像を取得
        /// </summary>
        private static Dictionary<LeadershipRank, ImageSource> leadershipImage;

        /// <summary>
        /// 兵種から兵士ステータスと取得
        /// </summary>
        private static Dictionary<SoldierTypes, Soldier> soldiers = new Dictionary<SoldierTypes, Soldier>
            {
                { SoldierTypes.Spear, new Soldier("足軽", 11, 11, SoldierTypes.Spear) },
                { SoldierTypes.Bow, new Soldier("弓足軽", 10, 12, SoldierTypes.Bow) },
                { SoldierTypes.Horse, new Soldier("騎馬兵", 12, 10, SoldierTypes.Horse) },
                { SoldierTypes.LongSpear, new Soldier("長槍足軽", 16, 16, SoldierTypes.LongSpear) },
                { SoldierTypes.LongBow, new Soldier("長弓兵", 15, 17, SoldierTypes.LongBow) },
                { SoldierTypes.EliteHorse, new Soldier("精鋭騎馬", 17, 15, SoldierTypes.EliteHorse) },
                { SoldierTypes.Samurai, new Soldier("武士", 18, 18, SoldierTypes.Samurai) },
                { SoldierTypes.MountArcher, new Soldier("弓騎馬", 17, 19, SoldierTypes.MountArcher) },
                { SoldierTypes.RedArms, new Soldier("赤備え", 21, 20, SoldierTypes.RedArms) },
                { SoldierTypes.Gun, new Soldier("鉄砲足軽", 18, 26, SoldierTypes.Gun) },
                { SoldierTypes.Grenade, new Soldier("炮烙火矢", 23, 23, SoldierTypes.Grenade) },
                { SoldierTypes.Dragoon, new Soldier("騎馬鉄砲", 26, 18, SoldierTypes.Dragoon) }
            };

        /// <summary>
        /// The rank.
        /// </summary>
        private static Dictionary<string, LeadershipRank> rank;

        /// <summary>
        /// static武将データ辞書 コンストラクタ
        /// </summary>
        static RankDictionary()
        {
            rarityImage = new Dictionary<RarityRank, ImageSource>();
            leadershipImage = new Dictionary<LeadershipRank, ImageSource>();
            leadershipImage[LeadershipRank.Nothing] = null;

            rank = new Dictionary<string, LeadershipRank>
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

            rarity = new Dictionary<string, RarityRank>
            {
                { "序", RarityRank.Common },
                { "上", RarityRank.Uncommon },
                { "特", RarityRank.Rare },
                { "極", RarityRank.SuperRare },
                { "天", RarityRank.UltraRare }
            };

            var strs = new[] { "Common", "Uncommon", "Rare", "SuperRare", "UltraRare" };
            foreach (var str in strs)
            {
                var urlstr = string.Format("Images/{0}.png", str);
                var uri = new Uri(urlstr, UriKind.Relative);
                var bmp = new BitmapImage(uri);
                var type = (RarityRank)Enum.Parse(typeof(RarityRank), str, false);
                rarityImage[type] = bmp;
            }

            foreach (KeyValuePair<string, LeadershipRank> r in Rank)
            {
                var urlstr = string.Format("Images/Leadership/{0}.png", r.Value.ToString());
                var uri = new Uri(urlstr, UriKind.Relative);
                var bmp = new BitmapImage(uri);
                leadershipImage[r.Value] = bmp;
            }
        }

        /// <summary>
        /// 兵種タイプを兵種ステータスに変換
        /// </summary>
        public static Dictionary<SoldierTypes, Soldier> Soldiers
        {
            get { return soldiers; }
            set { soldiers = value; }
        }

        /// <summary>
        /// 文字列からランク値を取得
        /// </summary>
        public static Dictionary<string, LeadershipRank> Rank
        {
            get
            {
                return rank;
            }
        }

        /// <summary>
        /// レアリティ文字列からレアリティを取得
        /// TODO: 消す
        /// </summary>
        public static Dictionary<string, RarityRank> Rarity
        {
            get
            {
                return rarity;
            }
        }

        /// <summary>
        /// レアリティから画像を取得
        /// </summary>
        public static Dictionary<RarityRank, ImageSource> RarityImage
        {
            get
            {
                return rarityImage;
            }
        }

        /// <summary>
        /// 統率力ランクから画像を取得
        /// </summary>
        public static Dictionary<LeadershipRank, ImageSource> LeadershipImage
        {
            get
            {
                return leadershipImage;
            }
        }
    }
}
