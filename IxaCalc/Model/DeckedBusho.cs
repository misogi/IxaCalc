using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IxaCalc.Model
{
    using System.ComponentModel;
    using IxaCalc.Enums;

    public class DeckedBusho : INotifyPropertyChanged
    {
        private Busho _originBusho;

        private int _bushoAttack;

        private int _bushoDefence;

        private double _actualAttack;

        private double _actualDefence;

        private int _rankUp;

        private double _actualLeadership;

        private double _perCost;

        private double _percent;

        private SoldierTypes _currentSoldierType;

        public DeckedBusho(Busho origin, SoldierTypes type)
        {
            _originBusho = origin;
            _currentSoldierType = type;
            _rankUp = 1;
            this.UpdateDeckedInfo();
        }

        private void UpdateDeckedInfo()
        {
            Percent = CalculatePercent(_originBusho);
            ActualLeadership = _originBusho.SoldierNumber * Percent;
            PerCost = ActualLeadership / _originBusho.Cost;
            BushoAttack = _originBusho.Attack + (_originBusho.AttackGrowth * 80 * _rankUp);
            BushoDefence = _originBusho.Defence + (_originBusho.DefenceGrowth * 80 * _rankUp);
            ActualAttack = (BushoAttack + (ActualLeadership * RankDictionary.soldiers[CurrentSoldierType].Attack)) * Percent;
            ActualDefence = (BushoDefence + (ActualLeadership * RankDictionary.soldiers[CurrentSoldierType].Defence)) * Percent;
        }

        private double CalculatePercent(Busho busho)
        {
            var type = _currentSoldierType;
            double percentage = 1.0;
            if (type == SoldierTypes.Spear || type == SoldierTypes.LongSpear)
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
            else if (type == SoldierTypes.MountArcher)
            {
                percentage = (RankToPercentage(busho.Horse) + RankToPercentage(busho.Bow)) / 2;
            }
            else if (type == SoldierTypes.RedArms)
            {
                percentage = (RankToPercentage(busho.Bow) + RankToPercentage(busho.Lance)) / 2;
            }
            else if (type == SoldierTypes.Samurai)
            {
                percentage = (RankToPercentage(busho.Horse) + RankToPercentage(busho.Lance)) / 2;
            }
            else if (type == SoldierTypes.Gun)
            {
                percentage = (RankToPercentage(busho.Weapon) + RankToPercentage(busho.Lance)) / 2;
            }
            else if (type == SoldierTypes.Dragoon)
            {
                percentage = (RankToPercentage(busho.Weapon) + RankToPercentage(busho.Horse)) / 2;
            }

            return percentage;
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
                    return 1.20;
                default:
                    return 0;
            }
        }

        public double PerCost
        {
            get
            {
                return _perCost;
            }
            set
            {
                if (_perCost != value)
                {
                    _perCost = value;
                    OnPropertyChanged("PerCost");
                }
            }
        }

        public double Percent
        {
            get
            {
                return _percent;
            }
            set
            {
                if (_percent != value)
                {
                    _percent = value;
                    OnPropertyChanged("Percent");
                }
            }
        }

        public double ActualLeadership
        {
            get
            {
                return _actualLeadership;
            }
            set
            {
                if (_actualLeadership != value)
                {
                    _actualLeadership = value;
                    OnPropertyChanged("ActualLeadership");
                }
            }
        }

        public double ActualDefence
        {
            get
            {
                return _actualDefence;
            }
            set
            {
                if (_actualDefence != value)
                {
                    _actualDefence = value;
                    OnPropertyChanged("ActualDefence");
                }
            }
        }
        
        public double ActualAttack
        {
            get
            {
                return _actualAttack;
            }
            set
            {
                if (_actualAttack != value)
                {
                    _actualAttack = value;
                    OnPropertyChanged("ActualAttack");
                }
            }
        }

        public int BushoAttack
        {
            get
            {
                return _bushoAttack;
            }
            set
            {
                if (_bushoAttack != value)
                {
                    _bushoAttack = value;
                    OnPropertyChanged("BushoAttack");
                }
            }
        }

        public int BushoDefence
        {
            get
            {
                return _bushoDefence;
            }
            set
            {
                if (_bushoDefence != value)
                {
                    _bushoDefence = value;
                    OnPropertyChanged("BushoDefence");
                }
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
                    this.UpdateDeckedInfo();
                    OnPropertyChanged("CurrentSoldierType");
                }
            }
        }
        public Busho OriginBusho
        {
            get
            {
                return _originBusho;
            }
        }

        public ImageSource RarityImage
        {
            get
            {
                return RankDictionary.rarityImage[OriginBusho.Rarity];
            }
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
