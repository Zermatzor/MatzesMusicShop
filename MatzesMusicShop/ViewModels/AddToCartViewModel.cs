using MatzesMusicShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MatzesMusicShop.ViewModels
{
    public class AddToCartViewModel
    {
        public CDs CD { get; set; }
        [Range(1, 99, ErrorMessage = "Please enter correct value")]
        public int Quantity { get; set; }
    }
}