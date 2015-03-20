using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotNetBay.Model;
using DotNetBay.Core;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        readonly ObservableCollection<Auction> auctions = new ObservableCollection<Auction>();

        public MainWindow()
        {
            DataContext = this;
            var mainRepository = ((App)Application.Current).MainRepository;
            var memberService = new SimpleMemberService(mainRepository);
            var auctionServie = new AuctionService(mainRepository, memberService);
            foreach (var auction in auctionServie.GetAll())
            {
                auctions.Add(auction);
            }
            var app = App.Current as App;

            app.AuctionRunner.Auctioneer.AuctionStarted += Auctioneer_AuctionStarted;

            InitializeComponent();
        }

        private void Auctioneer_AuctionStarted(object sender, Core.Execution.AuctionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("auctionstarted");
            CollectionViewSource.GetDefaultView(auctions).Refresh();
        }

        private void Btn_bid(object sender, RoutedEventArgs e)
        {
            var auction = this.AuctionGrid.SelectedItem;
            if (auction.GetType() == typeof(Auction))
            {
                var bidView = new BidView(this, (Auction) auction);
                bidView.ShowDialog(); // Blocking
            }            
        }

        private void Btn_newAuction(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView(this);
            sellView.ShowDialog(); // Blocking
        }


        public ObservableCollection<Auction> Auctions
        {
            get { return this.auctions; }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Halloooooooooooooooooo");
        }
    }
}
