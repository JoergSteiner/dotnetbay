using System.Collections.ObjectModel;
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

        private MainWindow mainWindow;
        private ObservableCollection<AuctionViewModel> auctions = new ObservableCollection<AuctionViewModel>();

        public MainViewModel(IAuctioneer auctioneer, IAuctionService auctionService)
        {
            this.AddAuctionCommand = new CommandWrapper(this.addNewAuctionAction);
            foreach (var auction in auctionService.GetAll())
            {
                this.auctions.Add(new AuctionViewModel(auction));
            }
        }

        public ICommand AddAuctionCommand { get; private set; }

        private void addNewAuctionAction()
        {
            var sellView = new SellView();
            sellView.Show();
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
