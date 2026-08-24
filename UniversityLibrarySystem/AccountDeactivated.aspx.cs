using System;
using System.Web;

namespace UniversityLibrarySystem
{
    public partial class AccountDeactivated : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated) {
                string cacheKey = $"PasswordChangedTime_{User.Identity.Name}";
                if (HttpContext.Current.Cache[cacheKey] != null)
                {
                    HttpContext.Current.Cache.Remove(cacheKey);
                }
                System.Web.Security.FormsAuthentication.SignOut();
            }
            else
            {
                Response.Redirect("/");
            }
        }
    }
}