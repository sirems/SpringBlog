using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpringBlog.Enums;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class CommentsController : AdminBaseController
    {
        // GET: Admin/Comments
        public ActionResult Index()
        {
            return View(db.Comments.OrderByDescending(x=>x.CreationTime).ToList());
        }

        [HttpPost]
        public ActionResult ChangeState(int id, bool isPublished)
        {

            var comment = db.Comments.Find(id);
            comment.State = isPublished ? CommentState.Approved : CommentState.Rejected;
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);

        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var comment = db.Comments.Find(id);
            db.Comments.RemoveRange(comment.Children);
            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}