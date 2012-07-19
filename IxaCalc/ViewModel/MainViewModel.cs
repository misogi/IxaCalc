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

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}