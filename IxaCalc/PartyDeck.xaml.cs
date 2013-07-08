using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IxaCalc
{
    /// <summary>
    /// 部隊のデッキを表示するユーザーコントロール
    /// </summary>
    public partial class PartyDeck : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PartyDeck()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  防御か攻撃かによって、デッキ済武将の表示テンプレートを切り替える。
        /// </summary>
        /// <param name="sender">送信者</param>
        /// <param name="e">パラメータ</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var box = sender as ComboBox;
            var item = box.SelectedItem as ComboBoxItem;

            if ((string)item.Content == "Def")
            {
                var tmp = this.Resources["DefTemplate"] as DataTemplate;
                this.DeckedBushos.ItemTemplate = tmp;
            }
            else
            {
                var tmp = this.Resources["AtkTemplate"] as DataTemplate;
                this.DeckedBushos.ItemTemplate = tmp;
            }
        }
    }
}
