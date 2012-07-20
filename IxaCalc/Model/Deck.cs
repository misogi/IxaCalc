namespace IxaCalc.Model
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using IxaCalc.Enums;

    public class Deck : INotifyPropertyChanged
    {
        private List<Busho> _bushos;

        private double[] _actualLeadership = new double[4];

        private double[] _perCost = new double[4];

        private double[] _percent = new double[4];

        private SoldierTypes _currentSoldierType;

        private double _totalAttack;

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
            if (Bushos.Count < 4 && !this.IsContain(busho))
            {
                Bushos.Add(busho);
                TotalAttack = this.CalculateTotalAttack();
            }
        }

        public void Remove(int index)
        {
            if (index < Bushos.Count)
            {
                _actualLeadership = new double[4];
                _perCost = new double[4];
                _percent = new double[4];
                Bushos.RemoveAt(index);
                TotalAttack = this.CalculateTotalAttack();
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

        public double[] Percent
        {
            get
            {
                return _percent;
            }
        }

        public double[] PerCost
        {
            get
            {
                return _perCost;
            }
        }

        public double[] ActualLeadership
        {
            get
            {
                return _actualLeadership;
            }
        }

        private double CalculateTotalAttack()
        {
            double total = 0;
            var type = _currentSoldierType;
            if (_currentSoldierType != null)
            {
                int i = 0;
                foreach (var busho in _bushos)
                {
                    double percentage = 1.0;
                    if (type == SoldierTypes.Lance || type == SoldierTypes.LongLance)
                    {
                        percentage = RankToPercentage(busho.Lance);
                    }
                    else if (type == SoldierTypes.Bow || type == SoldierTypes.LongBow)
                    {
                        percentage = RankToPercentage(busho.Bow);
                    }
                    else if (type == SoldierTypes.Horse || type == SoldierTypes.EliteHorse)
                    {
                        percentage = RankToPercentage(busho.Horse);
                    }
                    else if (type == SoldierTypes.Hammer)
                    {
                        percentage = RankToPercentage(busho.Weapon);
                    }

                    _percent[i] = percentage;
                    _actualLeadership[i] = busho.SoldierNumber * percentage;
                    _perCost[i] = busho.SoldierNumber * percentage / busho.Cost;
                    total += busho.SoldierNumber * percentage * RankDictionary.soldiers[type].Attack;
                    i++;
                }
            }
            OnPropertyChanged("PerCost");
            OnPropertyChanged("Percent");
            OnPropertyChanged("ActualLeadership");
            return total;
        }

        private bool IsContain(Busho cmpBusho)
        {
            bool cond = false;
            foreach (var busho in _bushos)
            {
                if(busho.Id == cmpBusho.Id)
                {
                    cond = true;
                    break;
                }
            }
            return cond;
        }

        private double RankToPercentage(LeadershipRank rank)
        {
            switch (rank)
            {
                case LeadershipRank.F:
                    return 0.8;
                case LeadershipRank.E:
                    return 0.85;
                case LeadershipRank.D:
                    return 0.9;
                case LeadershipRank.C:
                    return 0.95;
                case LeadershipRank.B:
                    return 1.0;
                case LeadershipRank.A:
                    return 1.05;
                case LeadershipRank.S:
                    return 1.1;
                case LeadershipRank.SS:
                    return 1.15;
                case LeadershipRank.SSS:
                    return 1.15;
                default:
                    return 1.0;
            }
        }

        /// <summary>
        /// 総兵数
        /// </summary>
        public void SwitchSoldierType(SoldierTypes type)
        {
            _currentSoldierType = type;
            TotalAttack = this.CalculateTotalAttack();
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
