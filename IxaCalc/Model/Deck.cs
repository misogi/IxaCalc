namespace IxaCalc.Model
{
    using System.Collections.Generic;

    public class Deck
    {
        private List<Busho> _bushos;

        public Deck()
        {
            _bushos = new List<Busho>(4);
        }

        public List<Busho> Bushos
        {
            get
            {
                return _bushos;
            }
        }

        public void Add(Busho busho)
        {
            if (Bushos.Count < 4)
            {
                Bushos.Add(busho);
            }
        }

        public void Remove(int index)
        {
            if (index < Bushos.Count)
            {
                Bushos.RemoveAt(index);
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
