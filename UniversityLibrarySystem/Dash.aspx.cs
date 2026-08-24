using System;
using System.Web.UI;
using System.Web.Security;
using Classes;

namespace UniversityLibrarySystem
{
    public partial class Dash : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = SiteConfig.mixTitle("داشبورد");
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name; // دریافت نام کاربری
                string[] roles = Roles.GetRolesForUser(username); // دریافت نقش‌های کاربر
                if (roles.Length > 0)
                {
                    string role = roles[0];
                    if (role.ToLower() == "user")
                        Response.Redirect("/Student/Books");
                    else
                        Response.Redirect("/Manager/Dashboard");
                }
            }
            Response.Redirect("/Login");
        }
    }
}