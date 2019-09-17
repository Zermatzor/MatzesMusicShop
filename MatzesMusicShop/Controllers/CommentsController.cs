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
        public ActionResult Index()
        {
            return View(new CommentViewModel());
        }
        // GET: Comments
        //public ActionResult Index(int? CdID)
        //{
        //    List<Comments> commentList = base.DB.Comments.Where(c => c.CdID == CdID).ToList();
        //    List<CommentViewModel> commentViewModelList = new List<CommentViewModel>();

        //    commentList.ForEach(com => commentViewModelList.Add(new CommentViewModel()
        //    {
        //        Rating = com.Rating.Value,
        //        Text = com.Text
        //    }));

        //    return View(commentViewModelList);
        //}
    }
}