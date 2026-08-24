using System;
using System.Web;
using System.Web.Security;

namespace UniversityLibrarySystem.Student
{
    public partial class LogOut : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cacheKey = $"PasswordChangedTime_{Membership.GetUser().UserName}";
            if (HttpContext.Current.Cache[cacheKey] != null)
            {
                HttpContext.Current.Cache.Remove(cacheKey);
            }
            FormsAuthentication.SignOut();
            Response.Redirect("/");
        }
    }
}