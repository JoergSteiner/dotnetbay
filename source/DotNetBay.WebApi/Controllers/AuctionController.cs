using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using DotNetBay.Core;
using DotNetBay.EF;
using DotNetBay.Model;
using DotNetBay.WebApi.Controllers.Dto;

namespace DotNetBay.WebApi.Controllers
{
    public class AuctionController : ApiController
    {

        private readonly IAuctionService auctionService;
        private readonly IMemberService memberService;

        public AuctionController()
        {
            var repo = new EFMainRepository();
            this.memberService = new SimpleMemberService(repo);

            this.auctionService = new AuctionService(repo, this.memberService);
            if (!this.auctionService.GetAll().Any())
            {
                var me = this.memberService.GetCurrentMember();
                this.auctionService.Save(new Auction
                {
                    Title = "My First Auction",
                    StartDateTimeUtc = DateTime.UtcNow.AddSeconds(20),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                    StartPrice = 72,
                    Seller = me
                });
            }
        }

        [HttpGet]
        [Route("api/Auction")]
        public IHttpActionResult GetAllAuctions()
        {
            var allAuctions = this.auctionService.GetAll();
            List<AuctionDto> auctionDtos = new List<AuctionDto>();
            foreach (Auction a in allAuctions)
            {
                auctionDtos.Add(new AuctionDto(a));
            }
            return this.Ok(auctionDtos);
        }

        [HttpGet]
        [Route("api/Auction/{id}")]
        public IHttpActionResult GetAuction(long id)
        {
            Auction a =
                (from x in this.auctionService.GetAll()
                 where x.Id == id
                 select x).FirstOrDefault();
            return this.Ok(new AuctionDto(a));
        }

        [Route("api/Auction")]
        [HttpPost]
        public IHttpActionResult AddNewAuction([FromBody] AuctionDto dto)
        {
            try
            {
                this.auctionService.Save(dto.GetAuction(this.memberService.GetCurrentMember()));
                Console.WriteLine("Saved auction");
                return this.Created(string.Format("api/Auction/{0}", dto.Id), dto);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/Auction/{id}/image")]
        public async Task<IHttpActionResult> Upload(long id)
        {
            var auction = this.auctionService.GetAll().FirstOrDefault(a => a.Id == id);

            if (auction != null)
            {
                var streamProvider = await this.Request.Content.ReadAsMultipartAsync(); // HERE
                foreach (var file in streamProvider.Contents)
                {
                    var image = await file.ReadAsByteArrayAsync();
                    auction.Image = image;
                    this.auctionService.Save(auction);
                }
                return this.Ok();
            }
            return this.NotFound();
        }

    }
}