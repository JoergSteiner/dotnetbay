using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Core;
using DotNetBay.Model;
using Newtonsoft.Json;

namespace DotNetBay.WPF.Services
{
    class RemoteAuctionService : IAuctionService
    {
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

            using (HttpClient httpClient = new HttpClient())
            {
                AuctionDto dto = new AuctionDto(auction);

                StringWriter sw = new StringWriter();
                JsonSerializer serializer = new JsonSerializer();
                JsonWriter writer = new JsonTextWriter(sw);
                serializer.Serialize(writer, auction);
                Console.WriteLine(sw.ToString());
                httpClient.PostAsync(url, new StringContent(sw.ToString(), System.Text.Encoding.UTF8, "application/json"));

                return auction;
            }
        }

        public Model.Bid PlaceBid(Model.Auction auction, double amount)
        {
            var url = "http://localhost:9001/api/Bid/{id}/bids";


            throw new NotImplementedException();
        }
    }
}
