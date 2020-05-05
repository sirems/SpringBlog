using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SpringBlog.Areas.Admin.ViewModels;
using SpringBlog.Helpers;
using SpringBlog.Models;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController
    {
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }
        // GET: Admin/Posts
        public ActionResult New()
        {
            ViewBag.CategoryId =
                new SelectList(db.Categories.OrderBy(x => x.CategoryName).ToList(), "Id", "CategoryName");
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult New(NewPostViewModel vm)
        {
            

            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    CategoryId = vm.CategoryId,
                    Title = vm.Title,
                    Content = vm.Content,
                    AuthorId = User.Identity.GetUserId(),
                    Slug = UrlService.URLFriendly(vm.Slug),
                    CreateTime = DateTime.Now,
                    ModificationTime = DateTime.Now,
                    PhotoPath =this.SaveImage(vm.FeaturedImage)
                    };
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId =
                new SelectList(db.Categories.OrderBy(x => x.CategoryName).ToList(), "Id", "CategoryName");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();
            TempData["SuccessMessage"] = "The post has been deleted successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string ConvertToSlug(string title)
        {
            return UrlService.URLFriendly(title);
        }
    }
}