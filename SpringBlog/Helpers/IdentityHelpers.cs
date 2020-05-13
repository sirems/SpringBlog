using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SpringBlog.Models;

namespace SpringBlog.Helpers
{
    //extension metot için static
    public static class IdentityHelpers
    {
        public static string DisplayName(this IIdentity identity)
        {
            //login'in yaptın giriş yapan hakkında ekstra bilgiler, hakettiği ünvanlar.claimleri bıraktığın cookie içine şifreliyo. onu alabilmek için claimsten çekiyoruz.veritabanına gidip bakmyo
            var claims = ((ClaimsIdentity) identity).Claims;
            string displayName=claims.FirstOrDefault(x => x.Type == "DisplayName").Value;

            return displayName;
        }
        public static string ProfilePhoto(this IIdentity identity)
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = identity.GetUserId();
                var user = db.Users.Find(userId);
                return user.ProfilePhoto;
            }
        }

        public static void SeedRolesAndUsers()
        {
            var context=new ApplicationDbContext();
            //https://stackoverflow.com/questions/19280527/mvc-5-seed-users-and-roles
            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "irem@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "irem@gmail.com",
                    Email = "irem@gmail.com",
                    DisplayName = "İrem S.",
                    EmailConfirmed = true
                };

                manager.Create(user, "Password1.");
                manager.AddToRole(user.Id, "admin");

                #region Seed Categories and Posts

                if (!context.Categories.Any())
                {
                    context.Categories.Add(new Category
                    {
                        CategoryName = "Sample Category 1",
                        Slug = "sample-category-1",
                        Posts = new List<Post>
                        {
                            new Post
                            {
                                Title = "Sample Post 1",
                                AuthorId = user.Id,
                                Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed dictum sapien augue, a hendrerit lacus blandit eu. Quisque accumsan imperdiet pellentesque. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed tempus, arcu a tincidunt viverra, sem velit maximus ipsum, quis fermentum lorem ex euismod augue. Praesent elementum tincidunt dui in finibus. Nunc nec vehicula metus, ut tempus nunc. Aliquam erat volutpat. Fusce semper congue mauris. Integer venenatis risus ut leo dictum semper. Fusce nisi justo, semper ut dolor sed, commodo lacinia dui.</p>",
                                Slug = "sample-post-1",
                                CreateTime = DateTime.Now,
                                ModificationTime = DateTime.Now,

                            },
                            new Post
                            {
                                Title = "Sample Post 2",
                                AuthorId = user.Id,
                                Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed dictum sapien augue, a hendrerit lacus blandit eu. Quisque accumsan imperdiet pellentesque. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed tempus, arcu a tincidunt viverra, sem velit maximus ipsum, quis fermentum lorem ex euismod augue. Praesent elementum tincidunt dui in finibus. Nunc nec vehicula metus, ut tempus nunc. Aliquam erat volutpat. Fusce semper congue mauris. Integer venenatis risus ut leo dictum semper. Fusce nisi justo, semper ut dolor sed, commodo lacinia dui.</p>",
                                Slug = "sample-post-2",
                                CreateTime = DateTime.Now,
                                ModificationTime = DateTime.Now,

                            },
                        }
                    });
                }
                #endregion

            }

            context.SaveChanges();
            context.Dispose();
        }


    }
}