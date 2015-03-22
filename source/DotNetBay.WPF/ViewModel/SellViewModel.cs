using DotNetBay.WPF.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Model;
using System.Windows;
using Microsoft.Win32;

namespace DotNetBay.WPF.ViewModel
{
    class SellViewModel : ViewModelBase
    {

        private SellView sellView;

        public delegate void UpdateAboutChanges();

        public event UpdateAboutChanges Update;

        public SellViewModel(SellView view)
        {
            this.sellView = view;
            this.StartNewAuctionCommand = new CommandWrapper(this.AddNewAuctionAction);
            this.CancelAuctionCommand = new CommandWrapper(this.CancelNewAuction);
            this.ChooseImageCommand = new CommandWrapper(this.ChooseImage);

            this.StartDate = DateTime.Now.AddMinutes(5);
            this.EndDate = DateTime.Now.AddDays(1);

        }

        public ICommand StartNewAuctionCommand { get; private set; }
        public ICommand CancelAuctionCommand { get; private set; }
        public ICommand ChooseImageCommand { get; private set; }

        private void CancelNewAuction()
        {
            this.sellView.Close();
        }

        private void ChooseImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;)|*.BMP;*.JPG;";
            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckFileExists)
                {
                    this.ImagePath = openFileDialog.FileName;
                    this.Update();
                }
            }
        }

        private void AddNewAuctionAction()
        {
            int height = 50;
            var mainRepository = ((App)Application.Current).MainRepository;
            var memberService = new SimpleMemberService(mainRepository);
            var auctionService = new AuctionService(mainRepository, memberService);
            var me = memberService.GetCurrentMember();

            if (this.DataValid())
            {
                double ratio = System.Drawing.Image.FromFile(this.ImagePath, true).Width / System.Drawing.Image.FromFile(this.ImagePath, true).Height;
                byte[] arr = null;
                Bitmap image = new Bitmap(System.Drawing.Image.FromFile(this.ImagePath, true), new System.Drawing.Size((int)(height * ratio), height));
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(image, typeof(byte[]));

                var tmp = new Auction
                {
                    Title = this.Title,
                    StartDateTimeUtc = this.StartDate,
                    EndDateTimeUtc = this.EndDate,
                    StartPrice = Int64.Parse(this.StartPrice),
                    Seller = me,
                    Image = arr
                };
                auctionService.Save(tmp);
            }
            else
            {
                System.Windows.MessageBox.Show("Some data is missing or invalid", "Error");
            }
        }

        private Boolean DataValid()
        {
            Boolean valid = true;
            try
            {
                valid = valid && "" != this.Title.Trim();
                valid = valid && this.StartDate >= DateTime.Now;
                valid = valid && this.EndDate >= DateTime.Now;
                valid = valid && this.EndDate > this.StartDate;
                valid = valid && Int64.Parse(this.StartPrice) > 0;
                valid = valid && "" != this.ImagePath.Trim();
            }
            catch (Exception ex)
            {
                valid = false;
                Console.WriteLine(ex.StackTrace);
            }
            return valid;
        }

        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string StartPrice { get; set; }

        private DateTime startDate;
        public DateTime StartDate { get; set; }

        private DateTime endDate;
        public DateTime EndDate { get; set; }
    }


}
