using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatzesMusicShop.ViewModels
{
    public class CommentViewModel
    {
        public int CdID { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public List<SelectListItem> RatingItems { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem(){
                Text = "5 Sterne", Value = "5"
            },
            new SelectListItem(){
                Text = "4 Sterne", Value = "4"
            },
            new SelectListItem(){
                Text = "3 Sterne", Value = "3"
            },
            new SelectListItem(){
                Text = "2 Sterne", Value = "2"
            },
            new SelectListItem(){
                Text = "1 Sterne", Value = "1"
            }
        };
    }
}