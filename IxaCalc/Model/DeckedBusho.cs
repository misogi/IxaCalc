﻿using System;
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

        private int _rank;

        private double _actualLeadership;

        private double _perCost;

        private double _percent;

        private SoldierTypes _currentSoldierType;

        public DeckedBusho(Busho origin, SoldierTypes type)
        {
            _originBusho = origin;
            _currentSoldierType = type;
            _rank = 0;
            this.UpdateDeckedInfo();
        }

        private void UpdateDeckedInfo()
        {
            Percent = CalculatePercent(_originBusho);
            ActualLeadership = _originBusho.SoldierNumber * Percent;
            PerCost = ActualLeadership / _originBusho.Cost;
            BushoAttack = _originBusho.Attack + (_originBusho.AttackGrowth * RankToBonusPoint(Rank));
            BushoDefence = _originBusho.Defence + (_originBusho.DefenceGrowth * RankToBonusPoint(Rank));
            ActualAttack = (BushoAttack + (_originBusho.SoldierNumber * RankDictionary.soldiers[CurrentSoldierType].Attack)) * Percent;
            ActualDefence = (BushoDefence + (_originBusho.SoldierNumber * RankDictionary.soldiers[CurrentSoldierType].Defence)) * Percent;
        }

        private int RankToBonusPoint(int rank)
        {
            int point = 80 * (Rank + 1);

            if (Rank >= 2)
            {
                point += (Rank - 1) * 20;
            }

            if (Rank >= 4)
            {
                point += (Rank - 3) * 20;
            }

            return point;
        }

        private double CalculatePercent(Busho busho)
        {
            var type = _currentSoldierType;
            double percentage = 1.0;
            if (type == SoldierTypes.Spear || type == SoldierTypes.LongSpear)
            {
                percentage = RankToPercentage(busho.Lance) + (Rank * 0.05);
            }
            else if (type == SoldierTypes.Bow || type == SoldierTypes.LongBow)
            {
                percentage = RankToPercentage(busho.Bow) + (Rank * 0.05);
            }
            else if (type == SoldierTypes.Horse || type == SoldierTypes.EliteHorse)
            {
                percentage = RankToPercentage(busho.Horse) + (Rank * 0.05);
            }
            else if (type == SoldierTypes.Hammer)
            {
                percentage = RankToPercentage(busho.Weapon) + (Rank * 0.05);
            }
            else if (type == SoldierTypes.MountArcher)
            {
                percentage = (RankToPercentage(busho.Horse) + RankToPercentage(busho.Bow) + (Rank * 0.05)) / 2;
            }
            else if (type == SoldierTypes.RedArms)
            {
                percentage = (RankToPercentage(busho.Bow) + RankToPercentage(busho.Lance) + (Rank * 0.05)) / 2;
            }
            else if (type == SoldierTypes.Samurai)
            {
                percentage = (RankToPercentage(busho.Horse) + RankToPercentage(busho.Lance) + (Rank * 0.05)) / 2;
            }
            else if (type == SoldierTypes.Gun)
            {
                percentage = (RankToPercentage(busho.Weapon) + RankToPercentage(busho.Lance) + (Rank * 0.05)) / 2;
            }
            else if (type == SoldierTypes.Dragoon)
            {
                percentage = (RankToPercentage(busho.Weapon) + RankToPercentage(busho.Horse) + (Rank * 0.05)) / 2;
            }

            if (percentage >= 1.20)
            {
                percentage = 1.20;
            }

            return percentage;
        }

        public void RankUp()
        {
            if (_rank < 5)
            {
                Rank += 1;
                this.UpdateDeckedInfo();
            }
        }

        public void RankDown()
        {
            if (_rank > 0)
            {
                Rank -= 1;
                this.UpdateDeckedInfo();
            }
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

        public int Rank
        {
            get
            {
                return _rank;
            }
            set
            {
                if (_rank != value)
                {
                    _rank = value;
                    OnPropertyChanged("Rank");
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
