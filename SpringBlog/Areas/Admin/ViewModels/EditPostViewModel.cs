using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpringBlog.Helpers;

namespace SpringBlog.Areas.Admin.ViewModels
{
    public class EditPostViewModel
    {
        //yeni bir post oluştururken neler lazım?
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required]  
        [MaxLength(200)]
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }

        public string CurrentFeaturedImage { get; set; }
        [Display(Name = "Created")]
        public DateTime CreationTime { get; set; }
        
        [Display(Name = "Last Modified")]
        public DateTime ModificationTime { get; set; }    
        [PostedImage]   
        public HttpPostedFileBase FeaturedImage { get; set; }
        [Required]
        [Display(Name = "Short Url")]
        [StringLength(200)]
        public string Slug { get; set; }
    }
}