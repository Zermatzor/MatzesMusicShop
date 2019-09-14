using MatzesMusicShop.Models;
using MatzesMusicShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatzesMusicShop.Controllers
{
    public class ShoppingCartController : BaseController
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<OrderItems> orderItems = base.GetSelectedOrderItems();
            List<CartItemViewModel> itemListViewModel = new List<CartItemViewModel>();

            foreach (var item in orderItems)
            {
                CartItemViewModel cViewModel = new CartItemViewModel()
                {
                    CdID = item.CDId,
                    OrderItemID = item.Id,
                    Album = item.CDs.Album,
                    Artist = item.CDs.Artist,
                    Anzahl = item.Quantity.Value,
                    Preis = item.Quantity.Value * item.CDs.Price.Value
                };
                itemListViewModel.Add(cViewModel);
            }

            return View(itemListViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(AddToCartViewModel viewModel)
        {
            for (int i = 0; i < viewModel.Quantity; i++)
            {
                base.AddToSelectedOrderItems(viewModel.CD.Id);
            }
            return RedirectToAction("Index", "CDs");
        }

        public ActionResult AddItem(int? id)
        {
            base.AddToSelectedOrderItems(id);
            return RedirectToAction("Index", "ShoppingCart");
        }
        public ActionResult RemoveItem(int? id)
        {
            base.RemoveFromSelectedOrderItems(id);
            return RedirectToAction("Index", "ShoppingCart");
        }
        public ActionResult RemoveAll(int? id)
        {
            base.RemoveAllFromSelectedOrderItems(id);
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}