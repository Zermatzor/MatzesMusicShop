using MatzesMusicShop.Models;
using MatzesMusicShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatzesMusicShop.Controllers
{
    public class CommentsController : BaseController
    {
        public ActionResult CommentForm(int? id)
        {
            return PartialView("_CommentForm", new CommentViewModel() { CdID = id.Value });
        }

        [HttpPost]
        public ActionResult CommentForm(CommentViewModel cViewModel)
        {
            if (ModelState.IsValid)
            {
                Comments comment = new Comments()
                {
                    Id = base.GetNextID(TableName.Comments),
                    CdID = cViewModel.CdID,
                    Title = cViewModel.Title,
                    Rating = cViewModel.Rating,
                    Text = cViewModel.Text,
                    TimeStamp = DateTime.Now
                };
                base.DB.Comments.Add(comment);
                base.DB.SaveChanges();
            }
            return RedirectToAction("Details", "CDs", new { id = cViewModel.CdID });
        }
        
        public ActionResult CommentList()
        {
            int id = (int)Session["CdID"];
            List<CommentViewModel> commentViewModelList = new List<CommentViewModel>();
            foreach (Comments comment in base.DB.Comments.Where(c=> c.CdID == id))
            {
                commentViewModelList.Add(new CommentViewModel()
                {
                    CdID = comment.CdID,
                    Title = comment.Title,
                    Rating = comment.Rating.Value,
                    Text = comment.Text,
                    DateTime = comment.TimeStamp.Value
                });
            }
            return PartialView("_CommentList", commentViewModelList);
        }
    }
}