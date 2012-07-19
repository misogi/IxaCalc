using GalaSoft.MvvmLight;
using IxaCalc.Model;

namespace IxaCalc.ViewModel
{
    using System.Collections.ObjectModel;

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
        }

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

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}