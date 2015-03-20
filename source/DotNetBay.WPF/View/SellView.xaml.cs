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

        private MainWindow mainWindow;

        public SellView(MainWindow mainWindow)
        {
            InitializeComponent();
            this.DataContext = new SellViewModel();
            this.mainWindow = mainWindow;
        }

        private void Btn_cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_startAuction(object sender, EventArgs e)
        {
            var mainRepository = ((App)Application.Current).MainRepository;
            var memberService = new SimpleMemberService(mainRepository);
            var auctionService = new AuctionService(mainRepository, memberService);
            var me = memberService.GetCurrentMember();
            byte[] arr = null;

            Boolean valid = true;
            try
            {
                valid = valid && this.TextBoxTitle.Text.Trim() != "";
                //valid = valid && DateTime.Parse(this.DatePickerStartDate.Text) >= DateTime.Now;
                valid = valid && DateTime.Parse(this.DatePickerEndDate.Text) >= DateTime.Now;
                valid = valid && DateTime.Parse(this.DatePickerEndDate.Text) > DateTime.Parse(this.DatePickerStartDate.Text);
                valid = valid && Int64.Parse(TextBoxStartPrice.Text) > 0;
                valid = valid && this.TextBoxImagePath.Text.Trim() != "";

                double ratio = System.Drawing.Image.FromFile(this.TextBoxImagePath.Text, true).Width / System.Drawing.Image.FromFile(this.TextBoxImagePath.Text, true).Height;
                int height = 50;

                Bitmap image = new Bitmap(System.Drawing.Image.FromFile(this.TextBoxImagePath.Text, true), new System.Drawing.Size((int)(height * ratio), height));
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(image, typeof(byte[]));
            }
            catch (Exception ex)
            {
                valid = false;
            }
            finally
            {
            }

            if (valid)
            {
                var tmp = new Auction
                {
                    Title = this.TextBoxTitle.Text,
                    StartDateTimeUtc = DateTime.Parse(this.DatePickerStartDate.Text),
                    EndDateTimeUtc = DateTime.Parse(this.DatePickerEndDate.Text),
                    StartPrice = Int64.Parse(TextBoxStartPrice.Text),
                    Seller = me,
                    Image = arr
                };

                auctionService.Save(tmp);
                mainWindow.Auctions.Add(tmp);
            }
            else
            {
                System.Windows.MessageBox.Show("Some data is missing or invalid", "Error");
            }


        }

        private void Btn_chooseImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;)|*.BMP;*.JPG;";
            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckFileExists)
                {
                    this.TextBoxImagePath.Text = openFileDialog.FileName;

                    /*

                    System.Diagnostics.Debug.WriteLine(openFileDialog.FileName);
                    Bitmap image = (Bitmap)System.Drawing.Image.FromFile(@openFileDialog.FileName, true);

                    ImageConverter converter = new ImageConverter();
                    byte[] arr = (byte[])converter.ConvertTo(image, typeof(byte[]));
                     * 
                     * */


                }
            }
        }

    }
}
