// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BushoList.xaml.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IxaCalc
{
    using System.Windows.Controls;
    using GalaSoft.MvvmLight.Messaging;
    using IxaCalc.ViewModel;

    /// <summary>
    /// The busho list.
    /// </summary>
    public partial class BushoList : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BushoList"/> class.
        /// </summary>
        public BushoList()
        {
            // 変数を初期化する必要があります
            this.InitializeComponent();
        }

        #endregion

        private void FilteredListMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var vm = this.DataContext as MainViewModel;
                if (vm != null)
                {
                    vm.SetDeckCommand.Execute(this.FilteredBushoList.SelectedItem);
                }
            }
        }

    }
}