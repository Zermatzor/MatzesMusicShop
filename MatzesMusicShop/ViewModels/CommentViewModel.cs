using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatzesMusicShop.ViewModels
{
    public class CommentViewModel
    {
        public int CdID { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Title { get; set; }
        public int Rating { get; set; }
        public string Date { get; set; }
    }
}