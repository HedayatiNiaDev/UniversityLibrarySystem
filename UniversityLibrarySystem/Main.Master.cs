using Classes;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace UniversityLibrarySystem
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!SiteConfig.siteStatus())
            {
                if (Membership.GetUser() == null)
                {
                    Response.StatusCode = 503;
                    Response.End();
                }
                else
                {
                    string username = Membership.GetUser().UserName;
                    if (Roles.GetRolesForUser(username)[0] == "User")
                    {
                        Response.StatusCode = 503;
                        Response.End();
                    }
                    else
                    {
                        Response.Write(@"<div class=""alert alert-warning text-center text-dark"" role=""alert"">
  سامانه غیرفعال است،شما می توانید از طریق <a href=""/Manager/SystemSettings"" class=""text-warning"">تنظیمات سامانه</a> آن را فعال کنید
</div>");
                    }
                }
            }
            else if (Membership.GetUser() != null)
            {
                string username = Membership.GetUser().UserName;
                if (Roles.GetRolesForUser(username)[0] == "Admin")
                {
                    if (!SiteConfig.siteCanRes())
                    {
                        Response.Write(@"<div class=""alert alert-warning text-center text-dark"" role=""alert"">
  سامانه رزرو در حال حاضر غیرفعال است،شما می توانید از طریق <a href=""/Manager/SystemSettings"" class=""text-warning"">تنظیمات سامانه</a> آن را فعال کنید
</div>");
                    }
                }
            }
            if (Membership.GetUser() != null && Roles.IsUserInRole(Membership.GetUser().UserName, "User"))
                MultiViewBtns.ActiveViewIndex = 1;
            else if (Membership.GetUser() != null)
                MultiViewBtns.ActiveViewIndex = 2;
            else
                MultiViewBtns.ActiveViewIndex = 0;
        }

        public string GetShortAboutUs()
        {
            try
            {
                string result = HttpRuntime.Cache["ShortAboutUs"] as string;
                if (result == null)
                {
                    using (var EF = new ULSDBEntities())
                    {
                        var qGetAbout = (from item in EF.ULSTbl_SiteSetting
                                         select item).FirstOrDefault();
                        HttpRuntime.Cache.Insert("ShortAboutUs", qGetAbout.ShortAboutUs, null, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                        result = qGetAbout.ShortAboutUs;
                    }
                }
                return result;
            }
            catch
            {
                return "متن مورد نظر یافت نشد";
            }
        }

        public string CategoriesNavbar()
        {
            try
            {
                string html = "";
                var categories = SiteConfig.GetCategories().Take(12); // دریافت دسته‌بندی‌ها از کش یا پایگاه داده

                if (categories != null && categories.Any())
                {
                    int counter = 1; // شروع شمارش از 1
                    var queryGetCategories = categories
                        .OrderByDescending(c => c.ID) // مرتب‌سازی بر اساس ID به صورت نزولی
                        .Take(12) // گرفتن ۱۲ دسته‌بندی اول
                        .Select(c => new { c.ID, c.Title }); // انتخاب فیلدهای مورد نیاز

                    foreach (var category in queryGetCategories)
                    {
                        if ((counter - 1) % 3 == 0) // شروع یک ستون جدید هر ۳ آیتم
                        {
                            if (counter != 1) // بستن div و ul قبلی قبل از شروع جدید
                            {
                                html += "</ul></div>";
                            }
                            html += "<div class='col'><ul>";
                        }

                        html += @"<li><a href='Categories-" + category.ID + "-" + category.Title + @"'>" + category.Title + @"</a></li>";

                        counter++;
                    }

                    // بستن div و ul برای آخرین دسته بندی
                    if ((counter - 1) % 3 != 0)
                    {
                        html += "</ul></div>";
                    }
                }

                return html;
            }
            catch (System.Web.HttpRequestValidationException)
            {
                Response.StatusCode = 403;
                Response.End();
                return "";
            }
        }

        public string CategoriesGroup(int index)
        {
            string html = "";
            var categories = SiteConfig.GetCategories().Take(5); // دریافت دسته‌بندی‌ها از کش یا پایگاه داده

            if (categories != null && categories.Any())
            {
                var queryGetCategories = categories
                    .OrderByDescending(c => c.ID) // مرتب‌سازی بر اساس ID به صورت نزولی
                    .Select(c => new { c.ID, c.Title }); // انتخاب فیلدهای مورد نیاز

                if (index == 1)
                {
                    // گرفتن ۵ دسته‌بندی اول
                    var firstGroup = queryGetCategories.Take(5);
                    foreach (var category in firstGroup)
                    {
                        html += @"<li><a href='Categories-" + category.ID + "-" + category.Title + @"'>" + category.Title + @"</a></li>";
                    }
                }
                else
                {
                    // گرفتن ۵ دسته‌بندی بعدی (با رد کردن ۵ مورد اول)
                    var secondGroup = queryGetCategories.Skip(5).Take(5);
                    foreach (var category in secondGroup)
                    {
                        html += @"<li><a href='Categories-" + category.ID + "-" + category.Title + @"'>" + category.Title + @"</a></li>";
                    }
                }
            }

            return html;
        }

        //public string ContactUs()
        //{
        //    try
        //    {
        //        string html = "";
        //        using (var EF = new UniversityLibrarySystem.ULSDBEntities())
        //        {

        //            var query = (from tableSiteSettings in EF.ULSTbl_SiteSetting
        //                         where tableSiteSettings.ID != 0
        //                         select new
        //                         {
        //                             tableSiteSettings.Email,
        //                             tableSiteSettings.Telephone
        //                         }).FirstOrDefault();
        //            if (!string.IsNullOrEmpty(query.Telephone))
        //            {
        //                html += @"                                                <li><a href=""tel:" + query.Telephone + @""">تماس</a></li>";
        //            }
        //            if (!string.IsNullOrEmpty(query.Email))
        //            {
        //                html += @"                                                <li><a href=""mailto:" + query.Email + @""">ایمیل</a></li>";
        //            }
        //            return html;
        //        }
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}
    }
}