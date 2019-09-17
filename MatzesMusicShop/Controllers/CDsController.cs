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
            // CdViewModel hat die CD und das Durchschnitts-Rating als Eigenschaften
            List<CdViewModel> cdViewModelList = new List<CdViewModel>();
            foreach (CDs cd in base.DB.CDs)
            {
                // Liste an Ratings zur CD die nicht null sind ermitteln
                List<int> ratingList = (from Comments c in base.DB.Comments
                                 where c.CdID == cd.Id && c.Rating != null
                                 select c.Rating.Value).ToList();
                // Mittelwert berechnen
                int avg = 0;
                if (ratingList.Count > 0)
                {
                    ratingList.ForEach(r => avg += r);
                    avg /= ratingList.Count;
                } 
                // Viewmodel zur Liste hinzufügen
                cdViewModelList.Add(new CdViewModel()
                {
                    CD = cd,
                    AvgRating = avg
                });
            }

            return View(cdViewModelList);
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
    }
}
