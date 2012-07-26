namespace IxaCalc.Model
{
    using IxaCalc.Enums;

    /// <summary>
    /// 兵士ステータス
    /// </summary>
    public class Soldier
    {
        #region プロパティ

        /// <summary>
        /// 兵士タイプ
        /// </summary>
        public SoldierTypes SoldierType { get; set; }

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// 防御力
        /// </summary>
        public int Defence { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">兵種名</param>
        /// <param name="atk">攻撃力</param>
        /// <param name="def">防御力</param>
        /// <param name="type">兵士タイプenum</param>
        public Soldier(string name, int atk, int def, SoldierTypes type)
        {
            Attack = atk;
            Defence = def;
            Name = name;
            SoldierType = type;
        }
    }
}
