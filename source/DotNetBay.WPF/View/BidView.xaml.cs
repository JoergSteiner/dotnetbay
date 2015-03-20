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
using DotNetBay.Core;
using DotNetBay.Model;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for BidView.xaml
    /// </summary>
    public partial class BidView : Window
    {

        private MainWindow mainWindow;
        private Auction auction;

        public BidView(MainWindow mainWindow, Auction auction)
        {
            InitializeComponent();
            this.DataContext = new BidViewModel(mainWindow, auction);
            this.mainWindow = mainWindow;
            this.auction = auction;
        }

        private void btn_placeBid(object sender, RoutedEventArgs e)
        {
            var bids = auction.Bids;
            double highestBid;
            Bid bid = null;
            var memberService = new SimpleMemberService(((App)App.Current).MainRepository);

            if (bids.Count > 0)
            {
                highestBid = bids.OrderByDescending(i => i.Amount).First().Amount;
            }
            else
            {
                highestBid = 0;
            }

            var amount = Int64.Parse(this.TextBoxBid.Text);
            if (amount > 0 && amount > highestBid)
            {
                bid = new Bid();
                bid.Amount = amount;
                bid.Accepted = true;
                bid.Auction = auction;
                bid.Bidder = memberService.GetCurrentMember();
                bid.ReceivedOnUtc = DateTime.Now;
            }
            else if (amount > 0)
            {
                bid = new Bid();
                bid.Amount = amount;
                bid.Accepted = false;
                bid.Auction = auction;
                bid.Bidder = memberService.GetCurrentMember();
                bid.ReceivedOnUtc = DateTime.Now;
            }
            else
            {
            }

            if (bid != null)
            {
                bids.Add(bid);
                auction.ActiveBid = bid;
                auction.IsClosed = false;
                mainWindow.Auctions.Remove(auction);
                mainWindow.Auctions.Add(auction);

            }

            
        }

        private void btn_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
