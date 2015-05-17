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
            using (HttpClient httpClient = new HttpClient())
            {

                Console.WriteLine("In Save in RemoteAuctionService");

                AuctionDto dto = new AuctionDto(auction);

                StringWriter sw = new StringWriter();
                JsonSerializer serializer = new JsonSerializer();
                JsonWriter writer = new JsonTextWriter(sw);
                serializer.Serialize(writer, auction);

                Console.WriteLine(sw.ToString());
                return null;
            }
        }

        public Model.Bid PlaceBid(Model.Auction auction, double amount)
        {
            throw new NotImplementedException();
        }
    }
}
