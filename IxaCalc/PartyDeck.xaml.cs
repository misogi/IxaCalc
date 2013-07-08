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
    public partial class PartyDeck : UserControl
    {
        public PartyDeck()
        {
            InitializeComponent();
        }

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
