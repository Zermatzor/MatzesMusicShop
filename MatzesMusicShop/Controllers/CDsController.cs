using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MatzesMusicShop.Models;
using MatzesMusicShop.ViewModels;

namespace MatzesMusicShop.Controllers
{
    public class CDsController : BaseController
    {
        // GET: CDs
        public ActionResult Index()
        {
            return View(base.DB.CDs.ToList());
        }

        // GET: CDs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDs cDs = base.DB.CDs.Find(id);
            if (cDs == null)
            {
                return HttpNotFound();
            }
            AddToCartViewModel viewModel = new ViewModels.AddToCartViewModel()
            {
                CD = cDs,
                Quantity = 1
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(AddToCartViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < viewModel.Quantity; i++)
                {
                    base.AddToSelectedOrderItems(viewModel.CD.Id);
                }
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: CDs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CDs/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Artist,Album,PictureUrl,Price,Beschreibung")] CDs cDs)
        {
            if (ModelState.IsValid)
            {
                cDs.Id = base.GetNextID(TableName.CDs);
                base.DB.CDs.Add(cDs);
                base.DB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDs);
        }

        // GET: CDs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDs cDs = base.DB.CDs.Find(id);
            if (cDs == null)
            {
                return HttpNotFound();
            }
            return View(cDs);
        }

        // POST: CDs/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Artist,Album,PictureUrl,Price,Beschreibung")] CDs cDs)
        {
            if (ModelState.IsValid)
            {
                base.DB.Entry(cDs).State = EntityState.Modified;
                base.DB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDs);
        }

        // GET: CDs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDs cDs = base.DB.CDs.Find(id);
            if (cDs == null)
            {
                return HttpNotFound();
            }
            return View(cDs);
        }

        // POST: CDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CDs cDs = base.DB.CDs.Find(id);
            base.DB.CDs.Remove(cDs);
            base.DB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
