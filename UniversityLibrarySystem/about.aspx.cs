using Classes;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace UniversityLibrarySystem
{
    public partial class about : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = SiteConfig.mixTitle("درباره ما");
            if (Membership.GetUser() != null)
                btnEdit.Visible = Roles.GetRolesForUser(User.Identity.Name).FirstOrDefault().ToLower() == "admin";

        }

        public string SiteSetting()
        {
            try
            {
                if (HttpRuntime.Cache["AboutUS"] == null)
                {
                    using (var EF = new ULSDBEntities())
                    {
                        var qGetAbout = (from item in EF.ULSTbl_SiteSetting
                                         select new {item.AboutUs}).FirstOrDefault();
                        HttpRuntime.Cache.Insert("AboutUS", qGetAbout.AboutUs, null, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration);
                        return qGetAbout.AboutUs;
                    }
                }
            }
            catch
            {
                return "متن این صفحه موجود نیست،لطفا از صفحه مدیریت اقدام به پر کردن صفحه نمایید";
            }
            return HttpRuntime.Cache["AboutUS"].ToString();
        }
    }
}