namespace IxaCalc
{
    using System.Windows.Controls;
    using System;

    using GalaSoft.MvvmLight.Messaging;
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
                default:
                    this.click_mp3.Stop();
                    this.click_mp3.Play();
                    break;
            }
            msg.Callback(System.Windows.MessageBoxResult.OK);
            }
        }
    }
}
