namespace IxaCalc.Model
{
    using IxaCalc.Enums;

    public class Soldier
    {
        public SoldierTypes SoldierType { get; set; }

        public int Attack { get; set; }

        public int Defence { get; set; }

        public string Name { get; set; }

        public Soldier(string name, int atk, int def, SoldierTypes type)
        {
            Attack = atk;
            Defence = def;
            Name = name;
            SoldierType = type;
        }
    }
}
