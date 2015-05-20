﻿using System;
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
                var me = memberService.GetCurrentMember();
                auctionService.Save(new Auction
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
            Console.WriteLine("GetAllAuctions");

            var allAuctions = this.auctionService.GetAll();
            List<AuctionDto> auctionDtos = new List<AuctionDto>();
            foreach (Auction a in allAuctions)
            {
                auctionDtos.Add(new AuctionDto(a));
            }
          return this.Ok("huhu");
            //return this.Ok(auctionDtos);
        }

        [HttpGet]
        [Route("api/Auction/{id}")]
        public IHttpActionResult GetAuction(long id)
        {
            Console.WriteLine("GetAuction");
            Console.WriteLine("Id is: " + id);
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
            Console.WriteLine("AddNewAuction {0}", dto.Title);
          }
          catch (Exception e)
          {
            Console.WriteLine("AddNewAuction {0}", "no title");
          }

          Auction a = new Auction
            {
                Seller = this.memberService.GetCurrentMember(),
                EndDateTimeUtc = dto.EndDateTimeUtc,
                StartDateTimeUtc = dto.StartDateTimeUtc,
                Title = dto.Title,
                StartPrice = dto.StartPrice
            };

            try
            {
                this.auctionService.Save(a);
                return this.Created(string.Format("api/Auction/{0}", a.Id), new AuctionDto(a));
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/Auction/{id}/image")]
        public Task<IHttpActionResult> Upload()
        {
            Console.WriteLine("Upload");
            throw new MissingMethodException("Method not implemented yet");
        }

    }
}