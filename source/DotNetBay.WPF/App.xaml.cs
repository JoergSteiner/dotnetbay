using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DotNetBay.Interfaces;
using DotNetBay.Data;
using DotNetBay.Data.FileStorage;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Model;
using DotNetBay.EF;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private IMainRepository mainRepository;
        private IAuctionRunner auctionRunner;

        public App()
        {

            //mainRepository = new FileSystemRepositoryFactory("store").CreateMainRepository();
            mainRepository = new EFMainRepository();

            auctionRunner = new AuctionRunner(mainRepository);
            auctionRunner.Start();
            var memberService = new SimpleMemberService(this.MainRepository);
            var service = new AuctionService(this.MainRepository, memberService);


            if (!service.GetAll().Any())
            {
                var me = memberService.GetCurrentMember();
                service.Save(new Auction
                {
                    Title = "My First Auction",
                    StartDateTimeUtc = DateTime.UtcNow.AddSeconds(20),
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                    StartPrice = 72,
                    Seller = me
                });
            }
        }

        public IMainRepository MainRepository
        {
            get { return this.mainRepository; }
            private set { ; }
        }

        public IAuctionRunner AuctionRunner
        {
            get { return auctionRunner; }
        }

    }
}
