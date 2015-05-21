using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetBay.WebApp.ViewModels
{
    public class NewAuctionViewModel
    {

        [Required]
        public String Title { get; set; }

        [Required]
        public String Description { get; set; }

        [Display(Name = "Start Price")]
        [Range(1, 1000)]
        [Required]
        public double StartPrice { get; set; }


        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Start Date and Time (UTC)")]
        [Required]
        public DateTime StartDateTimeUtc { get; set; }

        [Display(Name = "End Date and Time (UTC)")]
        [Required]
        public DateTime EndDateTimeUtc { get; set; }

    }
}