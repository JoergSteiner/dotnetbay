using DotNetBay.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DotNetBay.Model;

namespace DotNetBay.EF
{
    public class EFMainRepository : IMainRepository
    {

        private readonly MainDbContext context;

        public EFMainRepository()
        {
            this.context = new MainDbContext();
        }
        public Database Database
        {
            get
            {
                return this.context.Database;
            }
        }

        public IQueryable<Model.Auction> GetAuctions()
        {
            List<Auction> list = this.context.Auctions.ToList();
            return list.AsQueryable();
        }

        public IQueryable<Model.Member> GetMembers()
        {
            return this.context.Members.Include(m => m.Bids);
        }

        public Model.Auction Add(Model.Auction auction)
        {
            this.context.Auctions.Add(auction);
            return auction;
        }

        public Model.Auction Update(Model.Auction auction)
        {
            return auction;
        }

        public Model.Bid Add(Model.Bid bid)
        {
            this.context.Bids.Add(bid);
            return bid;
        }

        public Model.Bid GetBidByTransactionId(Guid transactionId)
        {
            return this.context.Bids.FirstOrDefault(b => b.TransactionId == transactionId);
        }

        public Model.Member Add(Model.Member member)
        {
            this.context.Members.Add(member);
            return member;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
