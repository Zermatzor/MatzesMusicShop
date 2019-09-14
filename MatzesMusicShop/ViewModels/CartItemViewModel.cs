using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatzesMusicShop.ViewModels
{
    public class CartItemViewModel
    {
        public int CdID { get; set; }
        public int OrderItemID { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int Anzahl { get; set; }
        public decimal Preis { get; set; }
    }
}