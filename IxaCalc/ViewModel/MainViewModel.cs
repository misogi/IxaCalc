﻿using GalaSoft.MvvmLight;
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
        private Busho[] _deck;

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
            _deck = new Busho[4];
            SetDeckCommand = new RelayCommand<KeyEventArgs>(e =>
                {
                    var cnt = e.OriginalSource as ListBoxItem;
                    if (cnt == null)
                    {
                        return;
                    }

                    var selBusho = cnt.Content as Busho;
                    if (selBusho == null)
                    {
                        return;
                    }

                    if (e.Key == Key.D1)
                    {
                        Busho1 = selBusho;
                    }
                    else if (e.Key == Key.D2)
                    {
                        Busho2 = selBusho;
                    }
                    else if (e.Key == Key.D3)
                    {
                        Busho3 = selBusho;
                    }
                    else if (e.Key == Key.D4)
                    {
                        Busho4 = selBusho;
                    }
                    AllSoldierNumber = this.TotalSoldierNum();
                });
        }

        /// <summary>
        /// 武将リストからデッキにキーボードで入れる
        /// </summary>
        public RelayCommand<KeyEventArgs> SetDeckCommand { get; private set; }

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
                return _deck[0];
            }

            set
            {
                if (_deck[0] == value)
                {
                    return;
                }

                _deck[0] = value;
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
                return _deck[1];
            }

            set
            {
                if (_deck[1] == value)
                {
                    return;
                }

                _deck[1] = value;
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
                return _deck[2];
            }

            set
            {
                if (_deck[2] == value)
                {
                    return;
                }

                _deck[2] = value;
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
                return _deck[3];
            }

            set
            {
                if (_deck[3] == value)
                {
                    return;
                }

                _deck[3] = value;
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
        /// 総兵数
        /// </summary>
        /// <returns>計算した総兵数</returns>
        public int TotalSoldierNum()
        {
            int soldier = 0;
            foreach (var busho in _deck)
            {
                if (busho != null)
                {
                    soldier += busho.SoldierNumber;
                }
            }
            return soldier;
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}