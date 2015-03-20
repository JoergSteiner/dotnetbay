using DotNetBay.Model;
using DotNetBay.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.WPF.ViewModel
{
    class BidViewModel : ViewModelBase
    {

        private MainWindow mainWindow;
        private Auction auction;

        public BidViewModel(MainWindow mainWindow, Auction auction)
        {
            this.mainWindow = mainWindow;
            this.auction = auction;
        }

        public string Title
        {
            get { return auction.Title; }
        }

        public byte[] Image
        {
            get { return auction.Image; }
        }

        public string Description
        {
            get { return auction.Description; }
        }

        public double StartPrice
        {
            get { return auction.StartPrice; }
        }

        public double CurrentPrice
        {
            get { return auction.ActiveBid.Amount; }
        }

        public string NewBid
        {
            set
            {
                Bid b = new Bid();
                b.Amount = Double.Parse(value);
                auction.Bids.Add(b);
            }
        }
    }
}
