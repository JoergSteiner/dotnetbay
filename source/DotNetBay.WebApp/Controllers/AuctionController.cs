using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

using DotNetBay.Model;
using DotNetBay.WebApp.ViewModels;
using DotNetBay.WebApi.Controllers.Dto;
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

        public ActionResult Create()
        {
            return this.View();
        }

        public ActionResult Detail(long id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(String.Format("{0}/{1}", this.uri, id));
                var auction = JsonConvert.DeserializeObject<AuctionDto>(response.Result);
                if (auction is AuctionDto)
                {
                    return this.View((AuctionDto) auction);
                }
                else
                {
                    return this.View("no");
                }
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(NewAuctionViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                AuctionDto dto = new AuctionDto()
                {
                    Title = model.Title,
                    StartPrice = model.StartPrice,
                    Description = model.Description,
                    StartDateTimeUtc = model.StartDateTimeUtc,
                    EndDateTimeUtc = model.EndDateTimeUtc
                };
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = httpClient.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json"));
                    return this.Content(response.Result.ToString());
                    //return this.RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return this.Content(model.Title);
            }
            return this.Content("Sorry, you entered invalid data");
        }
    }
}