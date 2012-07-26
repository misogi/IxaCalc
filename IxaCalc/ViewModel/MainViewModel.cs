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
        #region フィールド

        /// <summary>
        /// データ取得用オブジェクト
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        /// 現在のレアリティ
        /// </summary>
        private RarityRank _currentRarity;

        /// <summary>
        /// すべての武将リスト
        /// </summary>
        private ObservableCollection<Busho> _allBushoList;

        /// <summary>
        /// 表示する武将リスト
        /// </summary>
        private ObservableCollection<Busho> _bushoList;

        /// <summary>
        /// メインデッキ
        /// </summary>
        private Deck _mainDeck;

        /// <summary>
        /// 兵種リスト
        /// </summary>
        private List<Soldier> _soldierTypes;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataService">
        /// データ取得用オブジェクト
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

            NextRarityCommand = new RelayCommand<int>(this.NextRarityExecute);

            PreviousRarityCommand = new RelayCommand<int>(this.PreviousRarityExecute);

            RankUpCommand = new RelayCommand<int>(this.RankUpExecute);
            
            RankDownCommand = new RelayCommand<int>(this.RankDownExecute);

            this.Execute("特");
        }

        #region プロパティ

        /// <summary>
        /// デッキにセットするイベント
        /// </summary>
        public RelayCommand<Busho> SetDeckCommand { get; private set; }

        /// <summary>
        /// デッキから削除するイベント
        /// </summary>
        public RelayCommand<int> RemoveDeckCommand { get; private set; }

        /// <summary>
        /// 兵種を切り替えるコマンド
        /// </summary>
        public RelayCommand<string> ChangeSoldierCommand { get; private set; }

        /// <summary>
        /// レアリティを任意のものに切り替えるコマンド
        /// </summary>
        public RelayCommand<string> ChangeRarityCommand { get; private set; }

        /// <summary>
        /// レアリティを次に切り替えるコマンド
        /// </summary>
        public RelayCommand<int> NextRarityCommand { get; private set; }

        /// <summary>
        /// レアリティを手前に切り替えるコマンド
        /// </summary>
        public RelayCommand<int> PreviousRarityCommand { get; private set; }

        /// <summary>
        /// 武将のランクアップコマンド
        /// </summary>
        public RelayCommand<int> RankUpCommand { get; private set; }

        /// <summary>
        /// 武将のランクダウンコマンド
        /// </summary>
        public RelayCommand<int> RankDownCommand { get; private set; }

        /// <summary>
        /// レアリティで指定された武将のリスト
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
        /// 兵士のタイプ
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
        /// 現在選択されている絞り込み用レアリティ
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
        /// 部隊デッキ
        /// </summary>
        public Deck MainDeck
        {
            get
            {
                return this._mainDeck;
            }
        }

        #endregion

        #region メソッド

        /// <summary>
        /// デッキ追加イベント実行
        /// </summary>
        /// <param name="busho">武将</param>
        public void SetDeckExecute(Busho busho)
        {
            if (MainDeck.DeckedBushos.Count >= 4)
            {
                return;
            }

            if (busho == null)
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

        /// <summary>
        /// ランクアップコマンド用メソッド
        /// </summary>
        /// <param name="index">ランクアップする武将のデッキ上での位置</param>
        public void RankUpExecute(int index)
        {
            MainDeck.RankUp(index);
            MessengerInstance.Send(new DialogMessage("up", result => { }));
        }

        /// <summary>
        /// ランクダウンコマンド実行
        /// </summary>
        /// <param name="index">ランクダウンしたい武将のデッキ上での位置</param>
        public void RankDownExecute(int index)
        {
            MainDeck.RankDown(index);
            MessengerInstance.Send(new DialogMessage("down", result => { }));
        }

        /// <summary>
        /// デッキから削除する
        /// </summary>
        /// <param name="index">デッキ上での位置 負の値だとデッキの最後のものを削除</param>
        public void RemoveDeckExecute(int index)
        {
            if (MainDeck.DeckedBushos.Count <= 0)
            {
                return;
            }

            if (index < 0)
            {
                MainDeck.RemoveLast();
            }
            else
            {
                this.MainDeck.Remove(index);
            }

            MessengerInstance.Send(new DialogMessage("cancel", result => { }));
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (var key in keys)
            {
                RaisePropertyChanged(key);
            }
        }

        #region private

        /// <summary>
        /// 兵種を切り替える
        /// </summary>
        /// <param name="str">兵種名（enumに従う）</param>
        private void ChangeSoldierExecute(string str)
        {
            var type = (SoldierTypes)Enum.Parse(typeof(SoldierTypes), str, false);
            MainDeck.SwitchSoldierType(type);
            MessengerInstance.Send(new DialogMessage("click", result => { }));
        }

        /// <summary>
        /// レアリティを切り替える
        /// </summary>
        /// <param name="raritystr">レアリティ名(enumに従う) </param>
        private void Execute(string raritystr)
        {
            CurrentRarity = RankDictionary.Rarity[raritystr];
            var list1 = from p in _allBushoList where p.Rarity == CurrentRarity orderby p.Id select p;
            BushoList = new ObservableCollection<Busho>(list1);
            MessengerInstance.Send(new DialogMessage("busholist", result => { }));
        }

        /// <summary>
        /// 次のレアリティに切り替えるコマンドの処理
        /// </summary>
        /// <param name="index">現在選択されているリスト上での位置</param>
        private void NextRarityExecute(int index)
        {
            if ((int)CurrentRarity > 0)
            {
                CurrentRarity = CurrentRarity - 1;
                var list1 = from p in _allBushoList where p.Rarity == CurrentRarity orderby p.Id select p;
                BushoList = new ObservableCollection<Busho>(list1);
                MessengerInstance.Send(new NotificationMessage<int>(index, "bushoList"));
            }
        }

        /// <summary>
        /// 手前のレアリティに切り替えるコマンドの処理
        /// </summary>
        /// <param name="index">現在選択されているリスト上での位置</param>
        private void PreviousRarityExecute(int index)
        {
            if (CurrentRarity < RarityRank.UltraRare)
            {
                CurrentRarity = CurrentRarity + 1;
                var list1 = from p in _allBushoList where p.Rarity == CurrentRarity orderby p.Id select p;
                BushoList = new ObservableCollection<Busho>(list1);
                MessengerInstance.Send(new NotificationMessage<int>(index, "bushoList"));
            }
        }
        #endregion
        #endregion
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}