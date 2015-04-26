using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.WPF.View;
using Xceed.Wpf.DataGrid.Converters;

namespace DotNetBay.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private ObservableCollection<AuctionViewModel> auctions = new ObservableCollection<AuctionViewModel>();

        public delegate void UpdateAboutChanges();

        public event UpdateAboutChanges Update;

        public MainViewModel(IAuctioneer auctioneer, IAuctionService auctionService)
        {
            this.AddAuctionCommand = new CommandWrapper(this.addNewAuctionAction);
            foreach (var auction in auctionService.GetAll())
            {
                this.auctions.Add(new AuctionViewModel(auction));
            }
            this.auctions.CollectionChanged += this.OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Something in the DS changed");
            this.Update();
        }

        public ICommand AddAuctionCommand { get; private set; }

        private void addNewAuctionAction()
        {
            System.Console.WriteLine("addNewAuctionCommand Called");
            var sellView = new SellView();
            sellView.Show();
            Update();
        }


        public ObservableCollection<AuctionViewModel> Auctions
        {
            get
            {
                return this.auctions;
            }
        }
        
    }

}
