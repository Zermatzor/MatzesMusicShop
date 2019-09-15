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
            List<OrderItems> orderList = base.GetSelectedOrderItems();
            List<CartItemViewModel> cartItemList = new List<CartItemViewModel>();

            foreach (var order in orderList)
            {
                cartItemList.Add(new CartItemViewModel()
                {
                    CdID = order.CDId,
                    OrderItemID = order.Id,
                    Album = order.CDs.Album,
                    Artist = order.CDs.Artist,
                    Anzahl = order.Quantity.Value,
                    Preis = order.Quantity.Value * order.CDs.Price.Value
                });
            }

            return View(cartItemList);
        }

        public ActionResult AddItem(int? id)
        {
            base.AddToSelectedOrderItems(id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveItem(int? id)
        {
            base.RemoveFromSelectedOrderItems(id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveAll(int? id)
        {
            base.RemoveAllFromSelectedOrderItems(id);
            return RedirectToAction("Index");
        }
    }
}