using MatzesMusicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatzesMusicShop.ViewModels
{
    public class AddToCartViewModel
    {
        public CDs CD { get; set; }
        public int Quantity { get; set; }
    }
}