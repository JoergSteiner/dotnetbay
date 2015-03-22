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
using Xceed.Wpf.Toolkit;
using Microsoft.Win32;
using System.Drawing;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {

        private SellViewModel model;

        public SellView()
        {
            this.InitializeComponent();
            this.model = new SellViewModel(this);
            this.model.Update += this.UpdateAboutChanges;
            this.DataContext = this.model;
        }

        public void UpdateAboutChanges()
        {
            this.DataContext = null;
            this.DataContext = this.model;
        }
    }
}
