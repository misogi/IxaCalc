namespace IxaCalc
{
    using System.Windows.Controls;
    using System;

    using GalaSoft.MvvmLight.Messaging;

    using IxaCalc.ViewModel;
    /// <summary>
    /// 最初に表示されるページ
    /// </summary>
    public partial class MainPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class. 
        /// コンストラクタ
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            Messenger.Default.Register(this, (Action<DialogMessage>)ShowMessage);
        }

        public void ToggleSoundButton()
        {
            this.isSoundButton.IsChecked = this.isSoundButton.IsChecked == true ? false : true;
        }
        
        private void ShowMessage(DialogMessage msg)
        {
            if (msg.Content == "busholist")
            {
                ResetBushoListSelection();
            }

            if (this.isSoundButton.IsChecked == true)
            {
                switch (msg.Content)
                {
                    case "up":
                        this.up_mp3.Stop();
                        this.up_mp3.Play();
                        break;
                    case "down":
                        this.down_mp3.Stop();
                        this.down_mp3.Play();
                        break;
                    case "cancel":
                        this.cancel_mp3.Stop();
                        this.cancel_mp3.Play();
                        break;
                    case "selchange":
                        this.selchange_mp3.Stop();
                        this.selchange_mp3.Play();
                        break;
                    case "busholist":
                        this.click_mp3.Stop();
                        this.click_mp3.Play();
                        break;
                    default:
                        this.click_mp3.Stop();
                        this.click_mp3.Play();
                        break;
                }
                msg.Callback(System.Windows.MessageBoxResult.OK);
            }
        }

        private void bushoList_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var vm = this.DataContext as MainViewModel;
                if (vm != null)
                {
                    vm.SetDeckCommand.Execute(this.bushoList.SelectedItem);
                }
            }
        }

        public void ChangeBushoListSelectionUp()
        {
            var index = this.bushoList.SelectedIndex;
            if (index >= 0)
            {
                this.bushoList.SelectedIndex = index - 1;
            }
        }

        public void ChangeBushoListSelectionDown()
        {
            var index = this.bushoList.SelectedIndex;
            if (index < this.bushoList.Items.Count - 1)
            {
                this.bushoList.SelectedIndex = index + 1;
            }
        }

        private void bushoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.isSoundButton.IsChecked == true)
            {
                this.selchange_mp3.Stop();
                this.selchange_mp3.Play();
            }
        }

        public void ResetBushoListSelection()
        {
            this.bushoList.SelectedIndex = 0;
        }

    }
}
