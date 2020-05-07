using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
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


    }
}