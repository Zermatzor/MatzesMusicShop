using MatzesMusicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using MatzesMusicShop.ViewModels;

namespace MatzesMusicShop.Controllers
{
    /// <summary>
    /// Enum für die Tabellen-Namen. 
    /// </summary>
    public enum TableName { CDs, OrderItems, Orders, Users}

    /// <summary>
    /// Beinhaltet Controller-Member die von mehreren Controllern benötigt werden
    /// </summary>
    public class BaseController : Controller
    {
        private List<OrderItems> selectedOrderItems;

        public LayoutViewModel LayoutViewModel { get; set; } = new LayoutViewModel();     
        public MMSDBEntities DB { get; } = new MMSDBEntities();

        /// <summary>
        /// Get-Methode für die ausgewählten OrderItems der aktuellen Session
        /// </summary>
        /// <returns></returns>
        protected List<OrderItems> GetSelectedOrderItems()
        {
            if ((List<OrderItems>)this.Session["Cart"] != null)
            {
                return selectedOrderItems = (List<OrderItems>)this.Session["Cart"];
            }
            else
            {
                return selectedOrderItems = new List<OrderItems>();
            }
        }

        /// <summary>
        /// Setzt das cart-Objekt der Session auf null
        /// </summary>
        internal void EmptyCart()
        {
            this.Session["Cart"] = null;
        }

        /// <summary>
        /// Fügt eine ausgewählte CD der Liste an Orderitems und der Session["Cart"]
        /// hinzu. Ist eine CD bereits in den OrderItems vorhanden, wird lediglich die Eigenschaft
        /// Quantity erhöht.
        /// </summary>
        /// <param name="id">ID der CD</param>
        protected void AddToSelectedOrderItems(int? id)
        {
            if (id != null)
            {
                List<OrderItems> orderItemList = GetSelectedOrderItems();
                OrderItems orderItem = orderItemList.Find(x => x.CDId == id);

                if (orderItem == null)
                {
                    orderItem = new OrderItems()
                    {
                        CDId = id.Value,
                        CDs = DB.CDs.Find(id),
                        Id = GetNextID(TableName.OrderItems),
                        OrderId = -1,
                        Orders = null,
                        Quantity = 1
                    };
                    orderItemList.Add(orderItem);
                }
                else
                {
                    orderItem.Quantity++;
                }

                this.Session["Cart"] = orderItemList;
            }
        }

        /// <summary>
        /// Entfernt eine CD anhand derID aus dem Warenkorb
        /// </summary>
        /// <param name="id"></param>
        public void RemoveFromSelectedOrderItems(int? id)
        {
            List<OrderItems> orderItemList = GetSelectedOrderItems();
            OrderItems orderItem = orderItemList.Find(x => x.CDId == id.Value);

            if (id != null)
            {
                if (orderItem != null)
                {
                    if (orderItem.Quantity > 1)
                    {
                        orderItem.Quantity--;
                    }
                    else
                    {
                        orderItemList.Remove(orderItem);
                    }
                    this.Session["Cart"] = orderItemList;
                }
            }
        }

        /// <summary>
        /// Entfernt alle CDs mit gleicher ID aus dem Warenkorb
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAllFromSelectedOrderItems(int? id)
        {
            List<OrderItems> orderItemList = GetSelectedOrderItems();
            if (id != null)
            {
                orderItemList.RemoveAll(x => x.CDId == id.Value);
                this.Session["Cart"] = orderItemList;
            }
        }

        /// <summary>
        /// Gibt zum angegebenen Tabellennamen die höchste ID + 1 zurück
        /// </summary>
        /// <param name="tn">Enum-Element der Tabellenbezeichner</param>
        /// <returns>die nächste ID</returns>
        protected int GetNextID(TableName tn)
        {
            int max = 0;
            switch (tn)
            {
                case TableName.CDs:
                    {
                        if (DB.CDs.Count() > 0)
                        {
                            max = DB.CDs.Max(x => x.Id);
                        }
                        break;
                    }
                case TableName.OrderItems:
                    {
                        if (DB.OrderItems.Count() > 0)
                        {
                            max = DB.OrderItems.Max(x => x.Id);
                        }
                        break;
                    }
                case TableName.Orders:
                    {
                        if (DB.Orders.Count() > 0)
                        {
                            max = DB.Orders.Max(x => x.Id);
                        }
                        break;
                    }
                case TableName.Users:
                    {
                        if (DB.Users.Count() > 0)
                        {
                            max = DB.Users.Max(x => x.Id);
                        }
                        break;
                    }
            }
            return max + 1;
        } 

        /// <summary>
        /// Gibt die für die Anzeige benötigte Anzahl an Items im Warenkorb an.
        /// Ergibt sich aus der Anzahl an OrderItems mit der jeweiligen Quantity
        /// </summary>
        /// <returns>Anzahl der sich im Warenkorb befindlichen CD's</returns>
        internal int GetCartItemsCount()
        {
            int count = 0;
            List<OrderItems> orderItemList = (List<OrderItems>)this.Session["Cart"];
            if (orderItemList != null)
            {
                orderItemList.ForEach(c => count += c.Quantity.Value);
            }
            else
            {
                count = 0;
            }
            return count;
        }

        /// <summary>
        /// Gibt den Gesamtpreis aller sich im Warenkorb befindlichen Artikel wieder
        /// </summary>
        /// <returns>Gesamtpreis</returns>
        internal decimal GetCartItemPrice()
        {
            decimal price = 0;
            List<OrderItems> orderItemList = (List<OrderItems>)this.Session["Cart"];
            if (orderItemList != null)
            {
                orderItemList.ForEach(x => price += (x.CDs.Price * x.Quantity).Value);
            }
            return price;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}