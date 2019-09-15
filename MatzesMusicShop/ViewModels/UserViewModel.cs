using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MatzesMusicShop.Models;
using System.Linq;
using System.Web;

namespace MatzesMusicShop.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Vorname { get; set; }
        [Required]
        public string Nachname { get; set; }
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string Adresse { get; set; }
        public Users user { get; set; }
    }
}