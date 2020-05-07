using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SpringBlog.Models;

namespace SpringBlog.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SpringBlog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SpringBlog.Models.ApplicationDbContext context)
        {
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
                    DisplayName = "�rem S.",
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

            #region Add extras posts
            if (!context.Categories.Any(x => x.CategoryName == "Generated Posts") && context.Users.Any())
            {
                var userId = context.Users.First().Id;
                context.Categories.Add(new Category
                {
                    CategoryName = "Generated Posts",
                    Posts = GeneratePosts(userId)
                });
            }
            #endregion
        }

        private List<Post> GeneratePosts(string userId, int count = 100)
        {
            var posts = new List<Post>();

            for (int i = 0; i < count; i++)
            {
                posts.Add(new Post
                {
                    Title = "Generated Post " + (i + 1),
                    AuthorId = userId,
                    Content = "<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>",
                    Slug = "generated-post-" + (i + 1),
                    CreateTime = DateTime.Now,
                    ModificationTime = DateTime.Now
                });
            }

            return posts;
        }
    }
}
