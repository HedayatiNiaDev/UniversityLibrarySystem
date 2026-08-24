using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace UniversityLibrarySystem
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = SiteConfig.mixTitle("تماس با ما");
            if (Membership.GetUser() != null)
                btnEdit.Visible = Roles.GetRolesForUser(User.Identity.Name).FirstOrDefault().ToLower() == "admin";

        }

        public string GetContactUs()
        {
            var html = "";
            try
            {
                if (HttpRuntime.Cache["GetContactUSPage"] ==null)
                {
                    using (var EF = new UniversityLibrarySystem.ULSDBEntities())
                    {
                        var query = (from tableSiteSettings in EF.ULSTbl_SiteSetting
                                     where tableSiteSettings.ID != 0
                                     select tableSiteSettings).FirstOrDefault();
                        html += @"
                    <div class=""media contact-info"">
                        <span class=""contact-info__icon""><i class=""ti-home""></i></span>
                        <div class=""media-body"">
                            <h3>" + query.Address + @"</h3>
                            <p>آدرس مجموعه</p>
                        </div>
                    </div>
                    <div class=""media contact-info"">
                        <span class=""contact-info__icon""><i class=""ti-tablet""></i></span>
                        <div class=""media-body"">
                            <h3>
                                <a href=""tel:" + query.Telephone + @""">" + query.Telephone + @"</a>
                            </h3>
                            <p>شماره تلفن مجموعه</p>
                        </div>
                    </div>
                    <div class=""media contact-info"">
                        <span class=""contact-info__icon""><i class=""ti-email""></i></span>
                        <div class=""media-body"">
                            <h3>
                                <a href=""mailto:" + query.Email + @"""
                                    class=""__cf_email__"">" + query.Email + @"</a>
                            </h3>
                            <p>ایمیل مجموعه</p>
                        </div>
                    </div>
                </div>
                <div class=""col-12 col-md-6 col-lg-4 map"">
                    <iframe
                        src=""" + query.MapLink + @"""
                        style=""border: 0; height: 420px; width: 100%;"" allowfullscreen="""" loading=""lazy""
                        referrerpolicy=""no-referrer-when-downgrade""></iframe>
                </div>
            </div>";
                    }
                    HttpRuntime.Cache.Insert("GetContactUSPage", html, null, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                else
                {
                    html += HttpRuntime.Cache["GetContactUSPage"].ToString();
                }
                return html;
            }
            catch
            {
                return "";
            }
        }
    }
}