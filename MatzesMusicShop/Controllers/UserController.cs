using MatzesMusicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatzesMusicShop.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // POST: User
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Vorname,Nachname,Mail,Adresse")] Users user)
        {
            if (ModelState.IsValid)
            {
                // User
                user.Id = base.GetNextID(TableName.Users);
                base.DB.Users.Add(user);
                base.DB.SaveChanges();
                // Order
                Orders order = new Orders()
                {
                    Id = base.GetNextID(TableName.Orders),
                    UserId = user.Id
                };
                base.DB.Orders.Add(order);
                base.DB.SaveChanges();
                // OrderItems
                List<OrderItems> orderItems = base.GetSelectedOrderItems();
                foreach (var item in orderItems)
                {
                    item.Id = base.GetNextID(TableName.OrderItems);
                    item.OrderId = order.Id;
                    item.Orders = order;
                    item.CDs = null; 
                    base.DB.OrderItems.Add(item);
                    base.DB.SaveChanges();
                }

                base.DB.SaveChanges();
                base.EmptyCart();
                return RedirectToAction("Index", "Completion", null);
            }
            return View(user);
        }
    }
}