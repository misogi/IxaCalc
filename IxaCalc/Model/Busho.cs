namespace IxaCalc.Model
{
    using IxaCalc.Enums;

    /// <summary>
    /// 武将データクラス
    /// </summary>
    public class Busho
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// レア度
        /// </summary>
        public RarityRank Rarity { get; set; }
    }
}
