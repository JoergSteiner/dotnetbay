using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for BidView.xaml
    /// </summary>
    public partial class BidView : Window
    {

      private MainWindow mainWindow;
      private Auction ac;

        public BidView(MainWindow mainWindow, Auction ac)
        {
            InitializeComponent();
          this.mainWindow = mainWindow;
        }

        private void btn_placeBid(object sender, RoutedEventArgs e)
        {
          
        }

        private void btn_Cancel(object sender, RoutedEventArgs e)
        {
          this.Close();
        }


    }
}
