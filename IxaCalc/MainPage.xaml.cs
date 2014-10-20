namespace IxaCalc
{
    using System;
    using System.Windows.Controls;
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
            Messenger.Default.Register(this, (Action<NotificationMessage<string>>)this.MakeSound);
            Messenger.Default.Register(this, (Action<NotificationMessage<int>>)this.SetBushoListSelection);
        }

        #region メソッド
        #region public
        /// <summary>
        /// 武将リストの選択を一番上にする 
        /// TODO なくす
        /// </summary>
        public void ResetBushoListSelection()
        {
            if (this.bushoList.Items.Count > 0)
            {
                this.bushoList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 武将リストの選択を一つ上に変更
        /// </summary>
        public void ChangeBushoListSelectionUp()
        {
            var index = this.bushoList.SelectedIndex;
            if (index > 0)
            {
                this.bushoList.SelectedIndex = index - 1;
            }
        }

        /// <summary>
        /// 武将リストの選択を一つ下に
        /// </summary>
        public void ChangeBushoListSelectionDown()
        {
            var index = this.bushoList.SelectedIndex;
            if (index < this.bushoList.Items.Count - 1)
            {
                this.bushoList.SelectedIndex = index + 1;
            }
        }

        /// <summary>
        /// サウンドOn/offボタンをトグルする
        /// </summary>
        public void ToggleSoundButton()
        {
            this.isSoundButton.IsChecked = this.isSoundButton.IsChecked == true ? false : true;
        }

        #endregion
        #region private

        /// <summary>
        /// 武将リストの選択が変わった時に実行されるイベントメソッド
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">引数</param>
        private void BushoListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.isSoundButton.IsChecked == true)
            {
                this.selchange_mp3.Stop();
                this.selchange_mp3.Play();
            }
        }

        /// <summary>
        /// クリックイベント処理
        /// 武将リストでダブルクリックした場合、デッキに入れる
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">引数</param>
        private void BushoListMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        /// <summary>
        /// 武将リストの選択場所を変更
        /// </summary>
        /// <param name="noti">インデックス値を持つメッセージ</param>
        private void SetBushoListSelection(NotificationMessage<int> noti)
        {
            var index = noti.Content;
            var count = this.bushoList.Items.Count;
            if (index < count - 1)
            {
                this.bushoList.SelectedIndex = noti.Content;
            }
            else
            {
                this.bushoList.SelectedIndex = count - 1;
            }
        }

        /// <summary>
        /// サウンドを鳴らす
        /// </summary>
        /// <param name="msg">メッセージ</param>
        private void MakeSound(NotificationMessage<string> msg)
        {
            if (msg.Content == "busholist")
            {
                this.ResetBushoListSelection();
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
            }
        }
        #endregion
        #endregion
    }
}
