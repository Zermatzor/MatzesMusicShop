using MatzesMusicShop.Models;
using MatzesMusicShop.ViewModels;
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
        public ActionResult Index(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                // User
                int userID;
                Users tempUser = base.DB.Users.Where(u => u.Mail == userViewModel.Mail).SingleOrDefault();
                // Wenn ein User schon existiert (E-Mail vorhanden)
                // sollen nur die Daten des alten Users geupdated werden.
                if (tempUser != null)
                {
                    userID = tempUser.Id;
                    tempUser.Nachname = userViewModel.Nachname;
                    tempUser.Vorname = userViewModel.Vorname;
                    tempUser.Adresse = userViewModel.Adresse;
                }
                // Sonst einen neuen erstellen
                else
                {
                    userID = base.GetNextID(TableName.Users);
                    // Neuen User in Datenbank anlegen
                    base.DB.Users.Add(new Users() {
                        Id = userID,
                        Adresse = userViewModel.Adresse,
                        Mail = userViewModel.Mail,
                        Vorname = userViewModel.Vorname,
                        Nachname = userViewModel.Nachname
                    });
                }
                base.DB.SaveChanges();
                // Order erstellen und UserID zuweisen
                Orders order = new Orders()
                {
                    Id = base.GetNextID(TableName.Orders),
                    UserId = userID
                };
                base.DB.Orders.Add(order);
                base.DB.SaveChanges();
                // Jedem OrderItem die OrderID zuweisen
                List<OrderItems> orderItems = base.GetSelectedOrderItems();
                foreach (var item in orderItems)
                {
                    item.Id = base.GetNextID(TableName.OrderItems);
                    item.OrderId = order.Id;
                    item.Orders = order;
                    item.CDs = null; // ansonsten wird die CD nochmal angelegt.
                    base.DB.OrderItems.Add(item);
                    base.DB.SaveChanges();
                }

                base.DB.SaveChanges();
                base.EmptyCart();
                return RedirectToAction("Index", "Completion", null);
            }
            return View(userViewModel);
        }
    }
}