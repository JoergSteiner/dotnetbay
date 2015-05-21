using DotNetBay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetBay.WebApi.Controllers.Dto
{
    public class AuctionDto
    {
        public AuctionDto(Auction a)
        {
            this.Id = a.Id;
            this.StartPrice = a.StartPrice;
            this.Title = a.Title;
            this.Description = a.Description;
            this.Image = a.Image;
            this.CurrentPrice = a.CurrentPrice;
            this.StartDateTimeUtc = a.StartDateTimeUtc;
            this.EndDateTimeUtc = a.EndDateTimeUtc;
            this.CloseDateTimeUtc = a.CloseDateTimeUtc;
            this.SellerName = a.Seller != null ? a.Seller.DisplayName : null;
            this.FinalWinnerName = a.Winner != null ? a.Winner.DisplayName : null;
            this.CurrentWinnerName = a.ActiveBid != null ? a.ActiveBid.Bidder.DisplayName : null;
            this.IsClosed = a.IsClosed;
            this.IsRunning = a.IsRunning;
            this.Bids = new List<BidDto>();
        }

        public Auction GetAuction(Member seller)
        {
            Auction a = new Auction()
            {
                Id = this.Id,
                StartPrice = this.StartPrice,
                Title = this.Title,
                Description = this.Description,
                Image = this.Image,
                CurrentPrice = this.CurrentPrice,
                StartDateTimeUtc = this.StartDateTimeUtc,
                EndDateTimeUtc = this.EndDateTimeUtc,
                CloseDateTimeUtc = this.CloseDateTimeUtc,
                Seller = seller,
                IsClosed = this.IsClosed,
                IsRunning = this.IsRunning,
            };
            return a;
        }

        public long Id { get; set; }

        public double StartPrice { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public double CurrentPrice { get; set; }

        /// <summary>
        /// Gets or sets the UTC DateTime values to avoid wrong data when serializing the values
        /// </summary>
        public DateTime StartDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the UTC DateTime values to avoid wrong data when serializing the values
        /// </summary>
        public DateTime EndDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the UTC DateTime values to avoid wrong data when serializing the values
        /// </summary>
        public DateTime CloseDateTimeUtc { get; set; }

        public String SellerName { get; set; }

        public String CurrentWinnerName { get; set; }

        public String FinalWinnerName { get; set; }

        public List<BidDto> Bids { get; set; }

        public bool IsClosed { get; set; }

        public bool IsRunning { get; set; }
    }
}