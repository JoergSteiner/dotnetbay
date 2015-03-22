using DotNetBay.Core;
using DotNetBay.Model;
using DotNetBay.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNetBay.WPF.ViewModel
{
    class BidViewModel : ViewModelBase
    {

        private Auction auction;
        private BidView bidView;
        private SimpleMemberService memberService;

        public BidViewModel(Auction auction, BidView bidView)
        {
            this.auction = auction;
            this.bidView = bidView;
            this.PlaceBidCommand = new CommandWrapper(this.PlaceBidAction);
            this.CancelPlaceBidCommand = new CommandWrapper(this.CancelPlaceBidAction);

            memberService = new SimpleMemberService(((App)App.Current).MainRepository);
        }


        public ICommand PlaceBidCommand { get; private set; }
        public ICommand CancelPlaceBidCommand { get; private set; }

        private void PlaceBidAction()
        {
            var bids = auction.Bids;
            Bid bid = null;

            double highestBid = (bids.Count > 0)
              ? bids.OrderByDescending(i => i.Amount).First().Amount
              : 0;

            var amount = Int64.Parse(this.NewBid);

            bid = new Bid();
            bid.Amount = amount;
            bid.Auction = auction;
            bid.Bidder = memberService.GetCurrentMember();
            bid.ReceivedOnUtc = DateTime.Now;

            bid.Accepted = (amount > 0 && amount > highestBid) ? true : false;
            bids.Add(bid);
        }

        private void CancelPlaceBidAction()
        {
            this.bidView.Close();
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
            get { return (auction.ActiveBid != null) ? auction.ActiveBid.Amount : 0; }
        }

        public string NewBid
        {
            set;
            get;
        }
    }
}
