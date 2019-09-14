using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatzesMusicShop.ViewModels
{
    public class LayoutViewModel
    {
        public int CartItemsCount { get; set; } = 0;
        public decimal Price { get; set; } = 0;
    }
}