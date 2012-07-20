namespace IxaCalc.Model
{
    public class Deck
    {
        private Busho[] _bushos;

        private int _bushoNum = 0;

        public Busho[] Bushos
        {
            get;
            set;
        }

        public void Add(Busho busho)
        {
            if (_bushoNum <= 3)
            {
                _bushos[_bushoNum++] = busho;
            }
        }

        /// <summary>
        /// 総兵数
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public int TotalSoldierNum()
        {
            int soldier = 0;
            foreach (var busho in _bushos)
            {
                if (busho != null)
                {
                    soldier += busho.SoldierNumber;
                }
            }
            return soldier;
        }
    }
}
