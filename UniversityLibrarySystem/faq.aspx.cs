using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem
{
    public partial class faq : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = SiteConfig.mixTitle("سوالات متداول");
            if (Membership.GetUser() != null)
                btnEdit.Visible = Roles.GetRolesForUser(User.Identity.Name).FirstOrDefault().ToLower() == "admin";
        }

        public string getFaqs()
        {
            string text = HttpRuntime.Cache["FaqPageCache"] as string;
            try
            {
                if (text == null)
                {
                    using (var EF = new ULSDBEntities())
                    {
                        var qGetFaq = (from item in EF.ULSTbl_Faq
                                       where item.Status == true
                                       select new { item.Question, item.Answer }).ToList();
                        foreach (var item in qGetFaq)
                        {
                            text += @"            <div class=""faq-card"">
                <h5>" + item.Question + @"</h5>
                <p style=""white-space:break-spaces"">" + item.Answer + @"</p>
            </div>";
                        }

                        HttpRuntime.Cache.Insert("FaqPageCache", text, null, DateTime.Now.AddMinutes(3), System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                }
                return text;

            }
            catch
            {
                return "";
            }
        }
    }
}