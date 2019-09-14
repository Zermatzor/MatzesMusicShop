using MatzesMusicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatzesMusicShop.Controllers
{
    public class PartialsController : BaseController
    {
        // GET: Partials
        public ActionResult Navigation()
        {
            base.LayoutViewModel.CartItemsCount = base.GetCartItemsCount();
            base.LayoutViewModel.Price = base.GetCartItemPrice();

            return PartialView("_Navigation", base.LayoutViewModel);
        }
    }
}