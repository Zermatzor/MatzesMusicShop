using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatzesMusicShop.ViewModels
{
    public class DetailViewModel
    {
        public AddToCartViewModel AddToCartViewModel { get; set; }
        public CommentViewModel CommentViewModel { get; set; }
        public List<CommentViewModel> CommentListViewModel { get; set; }
    }
}