using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;
using DotNetBay.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DotNetBay.SignalR.Hubs
{
    public class AuctionsHub : Hub
    {
        public static void NotifyNewAuction(Auction auction)
        {
            GlobalHost.ConnectionManager.GetHubContext<AuctionsHub>().Clients.All.NewAuction(auction.Id);
        }

        public static void NotifyBidAccepted(Auction auction, Bid bid)
        {
            GlobalHost.ConnectionManager.GetHubContext<AuctionsHub>().Clients.All.NewBid(auction.Id);
        }

        public static void NotifyAuctionStarted(Auction auction)
        {
            GlobalHost.ConnectionManager.GetHubContext<AuctionsHub>().Clients.All.AuctionStarted(auction.Id);
        }
        public static void NotifyAuctionEnded(Auction auction)
        {
            GlobalHost.ConnectionManager.GetHubContext<AuctionsHub>().Clients.All.AuctionEnded(auction.Id);
        }
    }
}