using MatzesMusicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatzesMusicShop.ViewModels
{
    public class CdViewModel
    {
        public CDs CD { get; set; }
        public decimal AvgRating { get; set; }
    }
}