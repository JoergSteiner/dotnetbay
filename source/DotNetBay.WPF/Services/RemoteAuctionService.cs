using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using DotNetBay.Core;
using DotNetBay.Model;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Xceed.Wpf.Toolkit;

namespace DotNetBay.WPF.Services
{
    class RemoteAuctionService : IAuctionService
    {

        public delegate void NewAuction();
        public event NewAuction NewAuctionEvent;

        public delegate void BidAccepted();
        public event BidAccepted BidAcceptedEvent;

        public delegate void AuctionStarted();
        public event AuctionStarted AuctionStartedEvent;

        public delegate void AuctionEnded();
        public event AuctionEnded AuctionEndedEvent;


        public RemoteAuctionService()
        {
            var hubConnection = new HubConnection("http://localhost:25119/");
            var hub = hubConnection.CreateHubProxy("AuctionsHub");

            hub.On<long>("NewAuction", (long id) =>
            {
                if (this.NewAuctionEvent != null) this.NewAuctionEvent();
            });

            hub.On<long>("NewBid", (long id) =>
            {
                if (this.BidAcceptedEvent != null) this.BidAcceptedEvent();
            });

            hub.On<long>("AuctionStarted", (long id) =>
            {
                if (this.AuctionStartedEvent != null) this.AuctionStartedEvent();
            });

            hub.On<long>("AuctionEnded", (long id) =>
            {
                if (this.AuctionEndedEvent != null) this.AuctionEndedEvent();
            });

            hubConnection.Start().Wait();
            Console.WriteLine(hubConnection.State.ToString());
        }

        public IQueryable<Model.Auction> GetAll()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync("http://localhost:9001/api/Auction");
                List<Auction> auctions = JsonConvert.DeserializeObject<List<Auction>>(response.Result);
                return auctions.AsQueryable();
            }
        }

        public Model.Auction Save(Model.Auction auction)
        {

            var url = "http://localhost:9001/api/Auction";
            AuctionDto dto = new AuctionDto(auction);
            this.PostAsync(url, dto);
            return auction;
        }

        public Model.Bid PlaceBid(Model.Auction auction, double amount)
        {
            var url = "http://localhost:9001/api/Bid/{id}/bids";
            Bid b = new Bid();
            b.Amount = amount;

            BidDto dto = new BidDto(b);
            this.PostAsync(url, dto);
            return b;
        }

        private String SerializeObjectToJSON(Object o)
        {
            StringWriter sw = new StringWriter();
            JsonSerializer serializer = new JsonSerializer();
            JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, o);
            return sw.ToString();
        }

        private void PostAsync(String url, Object o)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.PostAsync(url, new StringContent(this.SerializeObjectToJSON(o), System.Text.Encoding.UTF8, "application/json"));
            }
        }
    }
}
