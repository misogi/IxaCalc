﻿namespace IxaCalc.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using IxaCalc.Enums;
    using IxaCalc.Model;

    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        ///     データ取得用オブジェクト
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        ///     メインデッキ
        /// </summary>
        private readonly Deck _mainDeck;

        /// <summary>
        ///     すべての武将リスト
        /// </summary>
        private ObservableCollection<Busho> _allBushoList;

        /// <summary>
        ///     表示する武将リスト
        /// </summary>
        private ObservableCollection<Busho> _bushoList;

        /// <summary>
        ///     現在のレアリティ
        /// </summary>
        private RarityRank _currentRarity;

        /// <summary>
        ///     兵種リスト
        /// </summary>
        private List<Soldier> _soldierTypes;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataService">
        /// データ取得用オブジェクト
        /// </param>
        public MainViewModel(IDataService dataService)
        {
            this._dataService = dataService;
            this._dataService.GetData(
                (items, error) =>
                    {
                        if (error != null)
                        {
                            // Report error here
                            return;
                        }

                        this._allBushoList = items;
                    });

            this.SoldierTypes = this._dataService.GetSoldierTypes();
            this._mainDeck = new Deck();
            this.SelectedPartyMemberIndex = -1;

            this.BushoListRarityChangeCommand = new RelayCommand(this.BushoListRarityChangeExecute);

            this.SetDeckCommand = new RelayCommand<Busho>(this.SetDeckExecute);

            this.RemoveDeckCommand = new RelayCommand<int>(this.RemoveDeckExecute);

            this.ChangeSoldierCommand = new RelayCommand<string>(this.ChangeSoldierExecute);

            this.ChangeRarityCommand = new RelayCommand<string>(this.Execute);

            this.NextRarityCommand = new RelayCommand<int>(this.NextRarityExecute);

            this.PreviousRarityCommand = new RelayCommand<int>(this.PreviousRarityExecute);

            this.RankUpCommand = new RelayCommand<int>(this.RankUpExecute);

            this.RankDownCommand = new RelayCommand<int>(this.RankDownExecute);

            this.Execute("特");
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     レアリティで指定された武将のリスト
        /// </summary>
        public ObservableCollection<Busho> BushoList
        {
            get
            {
                return this._bushoList;
            }

            set
            {
                this._bushoList = value;
                this.RaisePropertyChanged("BushoList");
            }
        }

        /// <summary>
        /// Gets the busho list rarity change command.
        /// </summary>BushoListRarityChangeCommand
        public RelayCommand BushoListRarityChangeCommand { get; private set; }

        /// <summary>
        ///     レアリティを任意のものに切り替えるコマンド
        /// </summary>
        public RelayCommand<string> ChangeRarityCommand { get; private set; }

        /// <summary>
        ///     兵種を切り替えるコマンド
        /// </summary>
        public RelayCommand<string> ChangeSoldierCommand { get; private set; }

        /// <summary>
        ///     現在選択されている絞り込み用レアリティ
        /// </summary>
        public RarityRank CurrentRarity
        {
            get
            {
                return this._currentRarity;
            }

            set
            {
                if (this._currentRarity != value)
                {
                    this._currentRarity = value;
                    this.RaisePropertyChanged("CurrentRarity");
                }
            }
        }

        /// <summary>
        ///     部隊デッキ
        /// </summary>
        public Deck MainDeck
        {
            get
            {
                return this._mainDeck;
            }
        }

        /// <summary>
        ///     レアリティを次に切り替えるコマンド
        /// </summary>
        public RelayCommand<int> NextRarityCommand { get; private set; }

        /// <summary>
        ///     レアリティを手前に切り替えるコマンド
        /// </summary>
        public RelayCommand<int> PreviousRarityCommand { get; private set; }

        /// <summary>
        ///     武将のランクダウンコマンド
        /// </summary>
        public RelayCommand<int> RankDownCommand { get; private set; }

        /// <summary>
        ///     武将のランクアップコマンド
        /// </summary>
        public RelayCommand<int> RankUpCommand { get; private set; }

        /// <summary>
        ///     デッキから削除するイベント
        /// </summary>
        public RelayCommand<int> RemoveDeckCommand { get; private set; }

        /// <summary>
        ///     デッキにセットするイベント
        /// </summary>
        public RelayCommand<Busho> SetDeckCommand { get; private set; }

        /// <summary>
        ///     現在選択中のレア度
        /// </summary>
        public string SelectedRarity { get; set; }

        /// <summary>
        ///     現在選択中のソート順
        /// </summary>
        public string SelectedSort { get; set; }

        /// <summary>
        ///     現在選択中のコスト
        /// </summary>
        public string SelectedCost { get; set; }

        /// <summary>
        ///     現在選択中の兵種
        /// </summary>
        public Soldier SelectedSoldier { get; set; }

        /// <summary>
        /// 部隊の中で選択中の武将の通し番号
        /// </summary>
        public int SelectedPartyMemberIndex { get; set; }

        /// <summary>
        ///     兵士のタイプ
        /// </summary>
        public List<Soldier> SoldierTypes
        {
            get
            {
                return this._soldierTypes;
            }

            set
            {
                if (this._soldierTypes != value)
                {
                    this._soldierTypes = value;
                    this.RaisePropertyChanged("SoldierTypes");
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The busho list rarity change execute.
        /// </summary>
        public void BushoListRarityChangeExecute()
        {
            ObservableCollection<Busho> rareFiltered;
            if (this.SelectedRarity == null || this.SelectedRarity == "全て")
            {
                rareFiltered = this._allBushoList;
            }
            else
            {
                this.CurrentRarity = RankDictionary.Rarity[this.SelectedRarity];
                var list = from p in this._allBushoList
                           where p.Rarity == this.CurrentRarity
                           select p;
                rareFiltered = new ObservableCollection<Busho>(list);
            }

            ObservableCollection<Busho> costFiltered;

            if (this.SelectedCost == null || this.SelectedCost == "全て")
            {
                costFiltered = this._allBushoList;
            }
            else
            {
                var cost = float.Parse(this.SelectedCost);
                var list = from p in this._allBushoList
                            where p.Cost == cost
                            select p;
                costFiltered = new ObservableCollection<Busho>(list);
            }

            ObservableCollection<Busho> sorted;

            if (this.SelectedSort == null)
            {
                var list = from p in rareFiltered.Intersect(costFiltered)
                         orderby p.Id
                         select p;
                sorted = new ObservableCollection<Busho>(list);
            }
            else 
            {
                var list = from p in rareFiltered.Intersect(costFiltered)
                         orderby p.Id
                         select p;
                sorted = new ObservableCollection<Busho>(list);
            }

            this.BushoList = new ObservableCollection<Busho>(sorted);
            this.RaisePropertyChanged("BushoList");
        }

        /// <summary>
        /// ランクダウンコマンド実行
        /// </summary>
        /// <param name="index">
        /// ランクダウンしたい武将のデッキ上での位置
        /// </param>
        public void RankDownExecute(int index)
        {
            this.MainDeck.RankDown(index);
            this.MessengerInstance.Send(new NotificationMessage<string>("down", "sound"));
        }

        /// <summary>
        /// ランクアップコマンド用メソッド
        /// </summary>
        /// <param name="index">
        /// ランクアップする武将のデッキ上での位置
        /// </param>
        public void RankUpExecute(int index)
        {
            this.MainDeck.RankUp(index);
            this.MessengerInstance.Send(new NotificationMessage<string>("up", "sound"));
        }

        /// <summary>
        /// デッキから削除する
        /// </summary>
        /// <param name="index">
        /// デッキ上での位置 負の値だとデッキの最後のものを削除
        /// </param>
        public void RemoveDeckExecute(int index)
        {
            if (this.MainDeck.DeckedBushos.Count <= 0)
            {
                return;
            }

            if (index < 0)
            {
                this.MainDeck.RemoveLast();
            }
            else
            {
                this.MainDeck.Remove(index);
            }


            this.MessengerInstance.Send(new NotificationMessage<string>("cancel", "sound"));
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (string key in keys)
            {
                this.RaisePropertyChanged(key);
            }
        }

        /// <summary>
        /// デッキ追加イベント実行
        /// </summary>
        /// <param name="busho">
        /// 武将
        /// </param>
        public void SetDeckExecute(Busho busho)
        {
            if (this.MainDeck.DeckedBushos.Count >= 4)
            {
                if (this.SelectedPartyMemberIndex >= 0 && !this.MainDeck.IsContain(busho))
                {
                    int index = this.SelectedPartyMemberIndex;
                    this.MainDeck.Remove(this.SelectedPartyMemberIndex);
                    this.MainDeck.Add(busho, index);
                    this.SelectedPartyMemberIndex = index;
                    this.RaisePropertyChanged("SelectedPartyMemberIndex");
                }
                return;
            }

            if (busho == null)
            {
                return;
            }

            this._mainDeck.Add(busho);
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (string key in keys)
            {
                this.RaisePropertyChanged(key);
            }

            this.RaisePropertyChanged("MainDeck");
        }

        #endregion

        #region Methods

        /// <summary>
        /// 兵種を切り替える
        /// </summary>
        private void ChangeSoldierExecute(string soldierTypeStr)
        {
            if (soldierTypeStr != null)
            {
                SoldierTypes type = (SoldierTypes)Enum.Parse(typeof(SoldierTypes), soldierTypeStr, true);
                var soldier = RankDictionary.Soldiers[type];
                this.SelectedSoldier = soldier;
            }

            if (this.SelectedSoldier != null)
            {
                this.MainDeck.SwitchSoldierType(this.SelectedSoldier.SoldierType);
                this.MessengerInstance.Send(new NotificationMessage<string>("click", "sound"));
            }
        }

        /// <summary>
        /// レアリティを切り替える
        /// </summary>
        /// <param name="raritystr">
        /// レアリティ名(enumに従う) 
        /// </param>
        private void Execute(string raritystr)
        {
            this.CurrentRarity = RankDictionary.Rarity[raritystr];
            IOrderedEnumerable<Busho> list1 = from p in this._allBushoList
                                              where p.Rarity == this.CurrentRarity
                                              orderby p.Id
                                              select p;
            this.BushoList = new ObservableCollection<Busho>(list1);
            this.MessengerInstance.Send(new NotificationMessage<string>("busholist", "sound"));
        }

        /// <summary>
        /// 次のレアリティに切り替えるコマンドの処理
        /// </summary>
        /// <param name="index">
        /// 現在選択されているリスト上での位置
        /// </param>
        private void NextRarityExecute(int index)
        {
            if ((int)this.CurrentRarity > 0)
            {
                this.CurrentRarity = this.CurrentRarity - 1;
                IOrderedEnumerable<Busho> list1 = from p in this._allBushoList
                                                  where p.Rarity == this.CurrentRarity
                                                  orderby p.Id
                                                  select p;
                this.BushoList = new ObservableCollection<Busho>(list1);
                this.MessengerInstance.Send(new NotificationMessage<int>(index, "bushoList"));
            }
        }

        /// <summary>
        /// 手前のレアリティに切り替えるコマンドの処理
        /// </summary>
        /// <param name="index">
        /// 現在選択されているリスト上での位置
        /// </param>
        private void PreviousRarityExecute(int index)
        {
            if (this.CurrentRarity < RarityRank.UltraRare)
            {
                this.CurrentRarity = this.CurrentRarity + 1;
                IOrderedEnumerable<Busho> list1 = from p in this._allBushoList
                                                  where p.Rarity == this.CurrentRarity
                                                  orderby p.Id
                                                  select p;
                this.BushoList = new ObservableCollection<Busho>(list1);
                this.MessengerInstance.Send(new NotificationMessage<int>(index, "bushoList"));
            }
        }

        #endregion

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}