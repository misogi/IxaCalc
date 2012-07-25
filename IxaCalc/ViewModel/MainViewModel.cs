using GalaSoft.MvvmLight;
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
    using GalaSoft.MvvmLight.Messaging;

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

        private RarityRank _currentRarity;

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
            MessengerInstance.Send(new DialogMessage("click", result => { }));
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (var key in keys)
            {
                RaisePropertyChanged(key);
            }
        }

        public void RankUpExecute(int index)
        {
            MainDeck.RankUp(index);
            MessengerInstance.Send(new DialogMessage("up", result => { }));
        }

        public void RankDownExecute(int index)
        {
            MainDeck.RankDown(index);
            MessengerInstance.Send(new DialogMessage("down", result => { }));
        }
        
        public void RemoveDeckExecute(int index)
        {
            this._mainDeck.Remove(index);
            MessengerInstance.Send(new DialogMessage("cancel", result => { }));
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
            MessengerInstance.Send(new DialogMessage("click", result => { }));
        }

        public void Execute(string raritystr)
        {
            CurrentRarity = RankDictionary.rarity[raritystr];
            var list1 = from p in _allBushoList where p.Rarity == CurrentRarity orderby p.Id select p;
            BushoList = new ObservableCollection<Busho>(list1);
            MessengerInstance.Send(new DialogMessage("busholist", result => { }));
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
                if (_soldierTypes != value)
                {
                    _soldierTypes = value;
                    RaisePropertyChanged("SoldierTypes");
                }
            }
        }

        /// <summary>
        /// デッキに入った3番目の武将
        /// </summary>
        public RarityRank CurrentRarity
        {
            get
            {
                return _currentRarity;
            }

            set
            {
                if (_currentRarity != value)
                {
                    _currentRarity = value;
                    RaisePropertyChanged("CurrentRarity");
                }
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