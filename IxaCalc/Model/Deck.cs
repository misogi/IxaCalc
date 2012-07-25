namespace IxaCalc.Model
{
    using System.Windows;
    using System.Collections.Generic;
    using System.ComponentModel;

    using IxaCalc.Enums;

    public class Deck : INotifyPropertyChanged
    {
        private List<DeckedBusho> _deckedBushos;

        private SoldierTypes _currentSoldierType;

        private double _totalAttack;

        private double _totalDefence;

        private double _totalAttackBar;

        private double _totalDefenceBar;
		
        private double _totalAttackPerCost;

        private double _totalDefencePerCost;

        private double _totalCost;

        private int _totalSoldierNumber;

        private double _totalSoldierNumberPerCost;

        private Visibility[] _deckedVisibility = new Visibility[4];

        public Deck()
        {
            _deckedBushos = new List<DeckedBusho>(4);
            UpdateDeckVisibility();
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
                UpdateDeckVisibility();
                OnPropertyChanged("DeckedBushos");
            }
        }

        public void Remove(int index)
        {
            if (index < DeckedBushos.Count)
            {
                DeckedBushos.RemoveAt(index);
                this.CalculateTotalStatus();
                UpdateDeckVisibility();
                OnPropertyChanged("DeckedBushos");
            }
        }

        public void RankUp(int index)
        {
            DeckedBushos[index].RankUp();
            this.CalculateTotalStatus();
        }

        public void RankDown(int index)
        {
            DeckedBushos[index].RankDown();
            this.CalculateTotalStatus();
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

        public SoldierTypes CurrentSoldierType
        {
            get
            {
                return _currentSoldierType;
            }

            set
            {
                if (_currentSoldierType != value)
                {
                    _currentSoldierType = value;
                    OnPropertyChanged("CurrentSoldierType");
                    OnPropertyChanged("CurrentSoldierTypeName");
                }
            }
        }

        public string CurrentSoldierTypeName
        {
            get
            {
                return RankDictionary.soldiers[_currentSoldierType].Name;
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
        public double TotalAttackBar
        {
            get
            {
                return _totalAttackBar;
            }

            set
            {
                _totalAttackBar = value;
                OnPropertyChanged("TotalAttackBar");
            }
        }

        /// <summary>
        /// 総攻撃力
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public double TotalDefenceBar
        {
            get
            {
                return _totalDefenceBar;
            }

            set
            {
                _totalDefenceBar = value;
                OnPropertyChanged("TotalDefenceBar");
            }
        }

        /// <summary>
        /// 総攻撃力
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public double TotalAttackPerCost
        {
            get
            {
                return _totalAttackPerCost;
            }

            set
            {
                _totalAttackPerCost = value;
                OnPropertyChanged("TotalAttackPerCost");
            }
        }

        /// <summary>
        /// 総攻撃力
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public int TotalSoldierNumber
        {
            get
            {
                return _totalSoldierNumber;
            }

            set
            {
                _totalSoldierNumber = value;
                OnPropertyChanged("TotalSoldierNumber");
            }
        }

        /// <summary>
        /// 総攻撃力
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public double TotalSoldierNumberPerCost
        {
            get
            {
                return _totalSoldierNumberPerCost;
            }

            set
            {
                _totalSoldierNumberPerCost = value;
                OnPropertyChanged("TotalSoldierNumberPerCost");
            }
        }

        /// <summary>
        /// 総攻撃力
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public double TotalDefencePerCost
        {
            get
            {
                return _totalDefencePerCost;
            }

            set
            {
                _totalDefencePerCost = value;
                OnPropertyChanged("TotalDefencePerCost");
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

        /// <summary>
        /// デッキに入った3番目の武将
        /// </summary>
        public Visibility[] DeckedVisibility
        {
            get
            {
                return _deckedVisibility;
            }
        }

        private void UpdateDeckVisibility()
        {
            var bushonum = DeckedBushos.Count;
            for (int i = 0; i < 4; i++)
            {
                _deckedVisibility[i] = i < bushonum ? Visibility.Visible : Visibility.Collapsed;
            }
            OnPropertyChanged("DeckedVisibility");
        }

        private void CalculateTotalStatus()
        {
            double totalAtk = 0;
            double totalDef = 0;
            double totalCost = 0;
            int soldier = 0;
            var type = CurrentSoldierType;
            if (CurrentSoldierType != null)
            {
                foreach (var decked in DeckedBushos)
                {
                    soldier += decked.OriginBusho.SoldierNumber;
                    totalAtk += decked.ActualAttack;
                    totalDef += decked.ActualDefence;
                    totalCost += decked.OriginBusho.Cost;
                }
            }
            TotalAttack = totalAtk;
            TotalDefence = totalDef;
            TotalAttackBar = TotalAttack > 400000 ? 1.0 : TotalAttack / 400000;
            TotalDefenceBar = TotalDefence > 400000 ? 1.0 : TotalDefence / 400000;
            TotalCost = totalCost;
            TotalSoldierNumber = soldier;
            TotalSoldierNumberPerCost = TotalSoldierNumber / TotalCost;
            TotalAttackPerCost = TotalAttack / TotalCost;
            TotalDefencePerCost = TotalDefence / TotalCost;
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
            CurrentSoldierType = type;
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
