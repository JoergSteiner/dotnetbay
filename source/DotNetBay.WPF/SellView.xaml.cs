using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DotNetBay.Model;
using DotNetBay.Core;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {
        public SellView()
        {
            InitializeComponent();
        }

        private void Btn_cancel(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void Btn_startAuction(object sender, EventArgs e)
        {
            var mainRepository = ((App)Application.Current).MainRepository;
            var memberService = new SimpleMemberService(mainRepository);
            var auctionServie = new AuctionService(mainRepository, memberService);

            var me = memberService.GetCurrentMember();
            auctionServie.Save(new Auction
            {
                Title = "newAuction",
                StartDateTimeUtc = DateTime.UtcNow.AddSeconds(20),
                EndDateTimeUtc = DateTime.UtcNow.AddDays(14),
                StartPrice = 72,
                Seller = me
            });

            System.Diagnostics.Debug.WriteLine("Halloooooooooooooooooo");
        }

    }
}
