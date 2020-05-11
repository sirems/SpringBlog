using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpringBlog.Models;

namespace SpringBlog.ViewModels
{
    public class ShowPostViewModel
    {
        public Post Post { get; set; }
        public CommentViewModel CommentViewModel { get; set; }
    }
}