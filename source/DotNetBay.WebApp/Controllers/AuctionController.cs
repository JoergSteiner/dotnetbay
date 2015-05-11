using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DotNetBay.Model;
using Newtonsoft.Json;

namespace DotNetBay.WebApp.Controllers
{
    public class AuctionController : Controller
    {
        readonly string uri = "http://localhost:9001/api/Auction";

        public ActionResult Index()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                List<Auction> auctions = JsonConvert.DeserializeObject<List<Auction>>(response.Result);
                return this.View(auctions);
            }
        }
    }
}