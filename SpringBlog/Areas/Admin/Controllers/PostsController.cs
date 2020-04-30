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
                    Slug = UrlService.URLFriendly(vm.Title),
                    CreateTime = DateTime.Now,
                    ModificationTime = DateTime.Now,
                    PhotoPath =vm.PhotoPath
                    };
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.CategoryId =
                new SelectList(db.Categories.OrderBy(x => x.CategoryName).ToList(), "Id", "CategoryName");
            return View();
        }
    }
}