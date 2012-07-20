using GalaSoft.MvvmLight;
using IxaCalc.Model;

namespace IxaCalc.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
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
        /// ウェルカム用タイトル
        /// </summary>
        private ObservableCollection<Busho> _bushoList;

        /// <summary>
        /// デッキ用
        /// </summary>
        private Deck _deck;

        private int _allSoldier;

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

                    BushoList = items;
                });
            _deck = new Deck();

            SetDeckCommand = new RelayCommand<Busho>(this.Execute);

            RemoveDeckCommand = new RelayCommand<int>(this.Execute);
        }

        public void Execute(Busho busho)
        {
            _deck.Add(busho);
            AllSoldierNumber = _deck.TotalSoldierNum();
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (var key in keys)
            {
                RaisePropertyChanged(key);
            }
        }

        public void Execute(int index)
        {
            _deck.Remove(index);
            AllSoldierNumber = _deck.TotalSoldierNum();
            string[] keys = { "Busho1", "Busho2", "Busho3", "Busho4" };
            foreach (var key in keys)
            {
                RaisePropertyChanged(key);
            }
        }

        public RelayCommand<Busho> SetDeckCommand { get; private set; }

        public RelayCommand<int> RemoveDeckCommand { get; private set; }

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
                if(_deck.Bushos.Count < 1)
                {
                    return null;
                }
                return _deck.Bushos[0];
            }

            set
            {
                if (_deck.Bushos[0] == value)
                {
                    return;
                }

                _deck.Bushos[0] = value;
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
                if (_deck.Bushos.Count < 2)
                {
                    return null;
                }
                return _deck.Bushos[1];
            }

            set
            {
                if (_deck.Bushos[1] == value)
                {
                    return;
                }

                _deck.Bushos[1] = value;
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
                if (_deck.Bushos.Count < 3)
                {
                    return null;
                }
                return _deck.Bushos[2];
            }

            set
            {
                if (_deck.Bushos[2] == value)
                {
                    return;
                }

                _deck.Bushos[2] = value;
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
                if (_deck.Bushos.Count < 4)
                {
                    return null;
                }
                return _deck.Bushos[3];
            }

            set
            {
                if (_deck.Bushos[3] == value)
                {
                    return;
                }

                _deck.Bushos[3] = value;
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

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}