namespace IxaCalc.Model
{
    public class Soldier
    {
        public int Attack { get; set; }

        public int Defence { get; set; }

        public string Name { get; set; }

        public Soldier(string name, int atk, int def)
        {
            Attack = atk;
            Defence = def;
            Name = name;
        }
    }
}
