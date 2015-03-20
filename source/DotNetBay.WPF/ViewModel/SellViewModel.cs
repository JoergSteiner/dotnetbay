using DotNetBay.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.WPF.ViewModel
{
    class SellViewModel : ViewModelBase
    {

        private MainWindow mainWindow;

        public SellViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string StartPrice { get; set; }
        public string Start { get; set; }
        public string End { set; get; }
        public string Image { get; set; }
    }
}
