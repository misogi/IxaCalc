﻿using GalaSoft.MvvmLight;
using IxaCalc.Model;

namespace IxaCalc.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
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
            this._mainDeck = new Deck();

            SetDeckCommand = new RelayCommand<Busho>(this.SetDeckExecute);

            RemoveDeckCommand = new RelayCommand<int>(this.RemoveDeckExecute);

            ChangeSoldierCommand = new RelayCommand<string>(this.ChangeSoldierExecute);
            
            ChangeRarityCommand = new RelayCommand<string>(this.Execute);

            RankUpCommand = new RelayCommand<int>(this.RankUpExecute);
            RankDownCommand = new RelayCommand<int>(this.RankDownExecute);

            this.Execute("特");
        }

        public void SetDeckExecute(Busho busho)
        {
            if(busho == null)
            {
                return;
            }
            this._mainDeck.Add(busho);
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (var key in keys)
            {
                RaisePropertyChanged(key);
            }
        }

        public void RankUpExecute(int index)
        {
            MainDeck.RankUp(index);
        }

        public void RankDownExecute(int index)
        {
            MainDeck.RankDown(index);
        }

        public void RemoveDeckExecute(int index)
        {
            this._mainDeck.Remove(index);
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (var key in keys)
            {
                RaisePropertyChanged(key);
            }
        }

        public void ChangeSoldierExecute(string str)
        {
            var type = (SoldierTypes)Enum.Parse(typeof(SoldierTypes), str, false);
            MainDeck.SwitchSoldierType(type);
        }

        public void Execute(string raritystr)
        {
            var rarity = RankDictionary.rarity[raritystr];
            var list1 = from p in _allBushoList where p.Rarity == rarity orderby p.Id select p;
            BushoList = new ObservableCollection<Busho>(list1);
        }

        public RelayCommand<Busho> SetDeckCommand { get; private set; }

        public RelayCommand<int> RemoveDeckCommand { get; private set; }

        public RelayCommand<string> ChangeSoldierCommand { get; private set; }

        public RelayCommand<string> ChangeRarityCommand { get; private set; }

        public RelayCommand<int> RankUpCommand { get; private set; }

        public RelayCommand<int> RankDownCommand { get; private set; }

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