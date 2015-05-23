using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DotNetBay.Core;
using DotNetBay.EF;
using DotNetBay.Interfaces;
using DotNetBay.Model;
using DotNetBay.WebApi.Controllers.Dto;

namespace DotNetBay.WebApi.Controllers
{
    public class BidController : ApiController
    {
        private readonly IAuctionService auctionService;
        private readonly IMemberService memberService;

        private IMainRepository repo;

        public BidController()
        {
            this.repo = new EFMainRepository();
            this.memberService = new SimpleMemberService(this.repo);

            this.auctionService = new AuctionService(this.repo, this.memberService);
        }

        [HttpGet]
        [Route("api/Bid/{id}/bids")]
        public IHttpActionResult getAllBids(long id)
        {
            List<BidDto> bidList = new List<BidDto>();
            var auction =   (from x in auctionService.GetAll()
                            where x.Id == id
                            select x).FirstOrDefault();
            foreach (var b in auction.Bids)
            {
                bidList.Add(new BidDto(b));
            }
            return this.Ok(bidList);
        }

        [HttpPost]
        [Route("api/Bid/{id}/bids")]
        public IHttpActionResult placeBid(long id, [FromBody] BidDto dto)
        {
            Auction a = (from x in this.auctionService.GetAll()
                where x.Id == id
                select x).FirstOrDefault();
            if (a == null)
            {
                return this.NotFound();
            }

            Bid bid = this.auctionService.PlaceBid(a, dto.Amount);
            return this.Created(string.Format("api/bids/{0}", bid.TransactionId), new BidDto(bid));
        }

        [HttpGet]
        [Route("api/Bid/{bidId}")]
        public IHttpActionResult GetBidByTransationId(Guid bidId)
        {
            Bid b = this.repo.GetBidByTransactionId(bidId);
            if (b != null)
            {
                return this.Ok(new BidDto(b));
            }
            else
            {
                return this.NotFound();
            }
        }




    } 
}