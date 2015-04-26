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
using DotNetBay.WPF.ViewModel;
using System.ComponentModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel model;

        public MainWindow()
        {

            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            this.InitializeComponent();
            var app = App.Current as App;
            var memberService = new SimpleMemberService(app.MainRepository);
            var auctionService = new AuctionService(app.MainRepository, memberService);

            model = new MainViewModel(app.AuctionRunner.Auctioneer, auctionService);
            model.Update += this.UpdateAboutChanges;
            this.DataContext = model;
        }

        public void UpdateAboutChanges()
        {
            this.DataContext = null;
            this.DataContext = this.model;
            Console.WriteLine("MainWindow.xaml.cs has been notified");
        }

    }
}
