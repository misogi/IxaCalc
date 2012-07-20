using GalaSoft.MvvmLight;
using IxaCalc.Model;

namespace IxaCalc.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;

    using GalaSoft.MvvmLight.Command;

    using IxaCalc.Enums;

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// データ取得用オブジェクト
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        /// すべての武将リスト
        /// </summary>
        private ObservableCollection<Busho> _allBushoList;

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<Busho> _bushoList;

        /// <summary>
        /// メインデッキ
        /// </summary>
        private Deck _mainDeck;

        /// <summary>
        /// 全ての兵士数
        /// </summary>
        private int _allSoldier;

        /// <summary>
        /// 現在指定している兵士タイプ
        /// </summary>
        private Soldier _soldier;

        /// <summary>
        /// 全ての攻撃力
        /// </summary>
        private int _allAttack;

        private List<Soldier> _soldierTypes;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// <param name="dataService">
        /// The data Service.
        /// </param>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (items, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    _allBushoList = items;
                });
            SoldierTypes = _dataService.GetSoldierTypes();
            _soldier = SoldierTypes[0];
            this._mainDeck = new Deck();

            SetDeckCommand = new RelayCommand<Busho>(this.Execute);

            RemoveDeckCommand = new RelayCommand<int>(this.Execute);

            ChangeSoldierCommand = new RelayCommand<Soldier>(this.Execute);
            
            ChangeRarityCommand = new RelayCommand<string>(this.Execute);

            this.Execute("極");
        }

        public void Execute(Busho busho)
        {
            if(busho == null)
            {
                return;
            }
            this._mainDeck.Add(busho);
            UpdateAllSum();
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (var key in keys)
            {
                RaisePropertyChanged(key);
            }
        }

        public void Execute(int index)
        {
            this._mainDeck.Remove(index);
            UpdateAllSum();
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (var key in keys)
            {
                RaisePropertyChanged(key);
            }
        }

        public void Execute(Soldier soldier)
        {
            _soldier = soldier;
            UpdateAllSum();
        }

        public void Execute(string raritystr)
        {
            var rarity = RankDictionary.rarity[raritystr];
            var list1 = from p in _allBushoList where p.Rarity == rarity orderby p.Id select p;
            BushoList = new ObservableCollection<Busho>(list1);
        }

        private void UpdateAllSum()
        {
            AllSoldierNumber = this._mainDeck.TotalSoldierNum();
            if (_soldier != null)
            {
                this._mainDeck.SwitchSoldierType(_soldier.SoldierType);
                AllAttack = AllSoldierNumber * _soldier.Attack;
            }
        }

        public RelayCommand<Busho> SetDeckCommand { get; private set; }

        public RelayCommand<int> RemoveDeckCommand { get; private set; }

        public RelayCommand<Soldier> ChangeSoldierCommand { get; private set; }

        public RelayCommand<string> ChangeRarityCommand { get; private set; }
        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Busho> BushoList
        {
            get
            {
                return _bushoList;
            }

            set
            {
                _bushoList = value;
                RaisePropertyChanged("BushoList");
            }
        }

        /// <summary>
        /// デッキに入った1番目の武将
        /// </summary>
        public Busho Busho1
        {
            get
            {
                if(this._mainDeck.Bushos.Count < 1)
                {
                    return null;
                }
                return this._mainDeck.Bushos[0];
            }

            set
            {
                if (this._mainDeck.Bushos[0] == value)
                {
                    return;
                }

                this._mainDeck.Bushos[0] = value;
                RaisePropertyChanged("Busho1");
            }
        }

        /// <summary>
        /// デッキに入った2番目の武将
        /// </summary>
        public Busho Busho2
        {
            get
            {
                if (this._mainDeck.Bushos.Count < 2)
                {
                    return null;
                }
                return this._mainDeck.Bushos[1];
            }

            set
            {
                if (this._mainDeck.Bushos[1] == value)
                {
                    return;
                }

                this._mainDeck.Bushos[1] = value;
                RaisePropertyChanged("Busho2");
            }
        }

        /// <summary>
        /// デッキに入った3番目の武将
        /// </summary>
        public Busho Busho3
        {
            get
            {
                if (this._mainDeck.Bushos.Count < 3)
                {
                    return null;
                }
                return this._mainDeck.Bushos[2];
            }

            set
            {
                if (this._mainDeck.Bushos[2] == value)
                {
                    return;
                }

                this._mainDeck.Bushos[2] = value;
                RaisePropertyChanged("Busho3");
            }
        }

        /// <summary>
        /// デッキに入った1番目の武将
        /// </summary>
        public Busho Busho4
        {
            get
            {
                if (this._mainDeck.Bushos.Count < 4)
                {
                    return null;
                }
                return this._mainDeck.Bushos[3];
            }

            set
            {
                if (this._mainDeck.Bushos[3] == value)
                {
                    return;
                }

                this._mainDeck.Bushos[3] = value;
                RaisePropertyChanged("Busho4");
            }
        }

        /// <summary>
        /// 部隊の合計兵士数
        /// </summary>
        public int AllSoldierNumber
        {
            get
            {
                return _allSoldier;
            }

            set
            {
                if (_allSoldier == value)
                {
                    return;
                }

                _allSoldier = value;
                RaisePropertyChanged("AllSoldierNumber");
            }
        }

        /// <summary>
        /// 部隊の合計兵士数
        /// </summary>
        public int AllAttack
        {
            get
            {
                return _allAttack;
            }

            set
            {
                if (_allAttack == value)
                {
                    return;
                }

                _allAttack = value;
                RaisePropertyChanged("AllAttack");
            }
        }

        /// <summary>
        /// デッキに入った3番目の武将
        /// </summary>
        public List<Soldier> SoldierTypes
        {
            get
            {
                return _soldierTypes;
            }

            set
            {
                _soldierTypes = value;
                RaisePropertyChanged("SoldierTypes");
            }
        }

        /// <summary>
        /// デッキに入った3番目の武将
        /// </summary>
        public Deck MainDeck
        {
            get
            {
                return this._mainDeck;
            }
        }
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}