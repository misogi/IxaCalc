namespace IxaCalc.Model
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using IxaCalc.Enums;

    public class Deck : INotifyPropertyChanged
    {
        private List<DeckedBusho> _deckedBushos;

        private SoldierTypes _currentSoldierType;

        private double _totalAttack;

        private double _totalDefence;

        private double _totalCost;

        public Deck()
        {
            _deckedBushos = new List<DeckedBusho>(4);
        }

        public List<DeckedBusho> DeckedBushos
        {
            get
            {
                return _deckedBushos;
            }
        }

        public void Add(Busho busho)
        {
            if (DeckedBushos.Count < 4 && !this.IsContain(busho))
            {
                var decked = new DeckedBusho(busho, _currentSoldierType);
                DeckedBushos.Add(decked);
                this.CalculateTotalStatus();
                OnPropertyChanged("DeckedBushos");
            }
        }

        public void Remove(int index)
        {
            if (index < DeckedBushos.Count)
            {
                DeckedBushos.RemoveAt(index);
                this.CalculateTotalStatus();
                OnPropertyChanged("DeckedBushos");
            }
        }

        /// <summary>
        /// 総兵数
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public int TotalSoldierNum()
        {
            int soldier = 0;
            foreach (var busho in DeckedBushos)
            {
                if (busho != null)
                {
                    soldier += busho.OriginBusho.SoldierNumber;
                }
            }
            return soldier;
        }

        /// <summary>
        /// 総攻撃力
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public double TotalAttack
        {
            get
            {
                return _totalAttack;
            }

            set
            {
                _totalAttack = value;
                OnPropertyChanged("TotalAttack");
            }
        }

        /// <summary>
        /// 総攻撃力
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public double TotalDefence
        {
            get
            {
                return _totalDefence;
            }

            set
            {
                _totalDefence = value;
                OnPropertyChanged("TotalDefence");
            }
        }

        /// <summary>
        /// 総攻撃力
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public double TotalCost
        {
            get
            {
                return _totalCost;
            }

            set
            {
                _totalCost = value;
                OnPropertyChanged("TotalCost");
            }
        }

        private void CalculateTotalStatus()
        {
            double totalAtk = 0;
            double totalDef = 0;
            double totalCost = 0;
            var type = _currentSoldierType;
            if (_currentSoldierType != null)
            {
                int i = 0;
                foreach (var decked in DeckedBushos)
                {
                    totalAtk += decked.ActualAttack;
                    totalDef += decked.ActualDefence;
                    totalCost += decked.OriginBusho.Cost;
                }
            }
            TotalAttack = totalAtk;
            TotalDefence = totalDef;
            TotalCost = totalCost;
        }

        private bool IsContain(Busho cmpBusho)
        {
            bool cond = false;
            foreach (var decked in DeckedBushos)
            {
                if (decked.OriginBusho.Id == cmpBusho.Id)
                {
                    cond = true;
                    break;
                }
            }
            return cond;
        }

        public void SwitchSoldierType(SoldierTypes type)
        {
            _currentSoldierType = type;
            foreach (var decked in DeckedBushos)
            {
                decked.CurrentSoldierType = _currentSoldierType;
            }
            this.CalculateTotalStatus();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
