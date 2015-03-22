using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DotNetBay.Model;
using System.Windows.Input;
using DotNetBay.WPF.View;

namespace DotNetBay.WPF.ViewModel
{
    public class AuctionViewModel : ViewModelBase
    {

        private Auction auction;


        public AuctionViewModel(Auction auction)
        {
            this.auction = auction;
            this.PlaceBidCommand = new CommandWrapper(this.PlaceBidAction);
        }


        public ICommand PlaceBidCommand { get; private set; }

        private void PlaceBidAction()
        {
            System.Console.WriteLine("PlaceBidAction Called");
            var bidView = new BidView(this.auction);
            bidView.Show();
        }

        public String Title
        {
            get { return this.auction.Title; }
            set { this.auction.Title = value; }
        }

        public byte[] Image
        {
            get { return this.auction.Image; }
            set { this.auction.Image = value; }
        }

        public Boolean IsRunning
        {
            /*
            get { return this.auction.IsRunning; }
            private set { this.auction.IsRunning = value; }
             */
            get { return true; }
            set { }
        }

        public String StartPrice
        {
            get { return this.auction.StartPrice.ToString(); }
            private set { this.auction.StartPrice = Double.Parse(value); }
        }
        public String CurrentPrice
        {
            get { return this.auction.CurrentPrice.ToString(); }
            private set { this.auction.CurrentPrice = Double.Parse(value); }
        }

        public String Bids
        {
            get
            {
                String res = "";
                foreach (var x in this.auction.Bids)
                {
                    if (x.Bidder != null)
                    {
                        res = res + "\n" + x.Bidder.DisplayName + " : " + x.Amount;
                    }
                    else
                    {
                        res = res + "\n" + "Bidder is anonymous";
                    }
                }

                return res;
            }
            private set { }
        }

        public string StartDateTimeUtc
        {
            get { return this.auction.StartDateTimeUtc.ToString(); }
            private set { this.auction.StartDateTimeUtc = DateTime.Parse(value); }
        }
        public string EndDateTimeUtc
        {
            get { return this.auction.EndDateTimeUtc.ToString(); }
            private set { this.auction.EndDateTimeUtc = DateTime.Parse(value); }
        }

        public Member Seller
        {
            get { return this.auction.Seller; }
            private set { this.auction.Seller = value; }
        }

        public Member Winner
        {
            get { return this.auction.Winner; }
            private set { this.auction.Winner = value; }
        }

        public Boolean IsClosed
        {
            get { return this.auction.IsClosed; }
            private set { this.auction.IsClosed = value; }
        }




    }
}
