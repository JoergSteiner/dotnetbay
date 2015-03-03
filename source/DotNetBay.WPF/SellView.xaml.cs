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
using DotNetBay.Model;
using DotNetBay.Core;
using Xceed.Wpf.Toolkit;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {

        private MainWindow mainWindow;

        public SellView(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Btn_cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_startAuction(object sender, EventArgs e)
        {
            var mainRepository = ((App)Application.Current).MainRepository;
            var memberService = new SimpleMemberService(mainRepository);
            var auctionService = new AuctionService(mainRepository, memberService);

            var me = memberService.GetCurrentMember();

            var tmp = new Auction
            {
                Title = this.TextBoxTitle.Text,
                StartDateTimeUtc = DateTime.Parse(this.DatePickerStartDate.Text),
                EndDateTimeUtc = DateTime.Parse(this.DatePickerEndDate.Text),
                StartPrice = Int64.Parse(TextBoxStartPrice.Text),
                Seller = me
            };

            auctionService.Save(tmp);
            mainWindow.Auctions.Add(tmp);


        }

        private void Btn_chooseImage(object sender, RoutedEventArgs e)
        {

        }

    }
}
