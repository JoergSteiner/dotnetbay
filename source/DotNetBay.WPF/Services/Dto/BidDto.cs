using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetBay.Model;

namespace DotNetBay.WPF.Services
{
    public class BidDto
    {
        public BidDto(Bid b)
        {
            this.Id = b.Id;
            this.ReceivedOnUtc = b.ReceivedOnUtc;
            this.TransactionId = b.TransactionId;
            this.BidderName = b.Bidder.DisplayName;
            this.Amount = b.Amount;
            this.Accepted = b.Accepted;
        }

        public long Id { get; set; }

        public DateTime ReceivedOnUtc { get; set; }

        public Guid TransactionId { get; set; }

        public string BidderName { get; set; }

        public double Amount { get; set; }

        public bool? Accepted { get; set; }
    }
}