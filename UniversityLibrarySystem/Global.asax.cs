using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;

namespace UniversityLibrarySystem
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/js/jquery-3.6.0.min.js", // مسیر فایل jQuery 
                DebugPath = "~/js/jquery-3.6.0.js",
                CdnPath = "https://lib.arvancloud.ir/jquery/3.6.0/jquery.min.js",
                CdnDebugPath = "https://lib.arvancloud.ir/jquery/3.6.0/jquery.js"
            });

            // تنظیم Routeها
            RouteTable.Routes.MapPageRoute("AdminPanel", "Manager/{Page}", "~/Manager/Default.aspx");
            RouteTable.Routes.MapPageRoute("UserPanel", "Student/{Page}", "~/Student/Default.aspx");
            RouteTable.Routes.MapPageRoute("Home", "Home/", "~/Default.aspx");
            RouteTable.Routes.MapPageRoute("Login", "Login/", "~/Login.aspx");
            RouteTable.Routes.MapPageRoute("Register", "Register/", "~/Register.aspx");
            RouteTable.Routes.MapPageRoute("Search", "Search/", "~/Search.aspx");
            RouteTable.Routes.MapPageRoute("BookDetails", "BookDetail-{bookID}-{bookTitle}/", "~/BookDetail.aspx", true, new RouteValueDictionary(), new RouteValueDictionary { { "bookID", @"\d+" } });
            RouteTable.Routes.MapPageRoute("Categories", "Categories-{categoryID}-{categoryTitle}/", "~/Categories.aspx", true, new RouteValueDictionary(), new RouteValueDictionary { { "categoryID", @"\d+" } });
            RouteTable.Routes.MapPageRoute("CategoriesN", "Categories/", "~/Categories.aspx");
            RouteTable.Routes.MapPageRoute("ContactUs", "ContactUs", "~/ContactUs.aspx");
            RouteTable.Routes.MapPageRoute("AboutUs", "AboutUs", "~/About.aspx");
            RouteTable.Routes.MapPageRoute("Faq", "Faq", "~/Faq.aspx");
            RouteTable.Routes.MapPageRoute("Dashboard", "-", "~/Dash.aspx");
            RouteTable.Routes.MapPageRoute("DashboardM", "Dash", "~/Dash.aspx");
            RouteTable.Routes.MapPageRoute("DashboardL", "Dashboard", "~/Dash.aspx");
            RouteTable.Routes.MapPageRoute("AccountDeactivated", "AccountDeactivated", "~/AccountDeactivated.aspx");
            RouteTable.Routes.MapPageRoute("ResetPassword", "ResetPassword", "~/ResetPassword.aspx");
            RouteTable.Routes.MapPageRoute("ForgetPassword", "ForgetPassword", "~/ForgetPassword.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        private DateTime GetPasswordChangedTime(string username, out bool isSafe)
        {
            DateTime passwordChangedTime = DateTime.MinValue;
            isSafe = true;

            // کلید کش برای ذخیره‌سازی داده‌ها
            string cacheKey = $"PasswordChangedTime_{username}";

            // بررسی وجود داده در کش
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                var cachedData = (DateTime)HttpRuntime.Cache[cacheKey];
                passwordChangedTime = cachedData;
                isSafe = true;
                return passwordChangedTime;
            }

            try
            {
                using (var connection = new ULSDBEntities())
                {
                    var query = connection.ULSTbl_Users
                        .AsNoTracking()
                        .Where(u => u.UserName == username)
                        .Select(u => new { u.chpass, u.StatusID })
                        .FirstOrDefault();

                    if (query != null)
                    {
                        passwordChangedTime = (DateTime)query.chpass;
                        isSafe = query.StatusID > 0;

                        // ذخیره‌سازی داده‌ها در کش به مدت 5 دقیقه
                        HttpRuntime.Cache.Insert(cacheKey, passwordChangedTime, null, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                }
            }
            catch
            {
                // خطا را لاگ کنید یا مدیریت کنید
            }

            return passwordChangedTime;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (app != null)
            {
                // استفاده از HashSet برای مسیرهای استثنا
                var excludePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "/Manager",
            "/Student"
        };

                // ذخیره‌سازی URL در یک متغیر برای جلوگیری از دسترسی مکرر
                var requestUrl = app.Request.Url;
                string url = requestUrl.AbsoluteUri;
                string absolutePath = requestUrl.AbsolutePath;

                // بررسی اگر URL با "/" پایان می‌یابد و از ریشه نیست.
                if (url.EndsWith("/", StringComparison.OrdinalIgnoreCase) && !absolutePath.Equals("/", StringComparison.OrdinalIgnoreCase))
                {
                    // بررسی مسیرهای استثنا
                    if (!IsExcludedPath(absolutePath, excludePaths))
                    {
                        // ریدایرکت به URL بدون "/"
                        Response.Redirect(url.TrimEnd('/'));
                        return;
                    }
                }
            }
            #region CultureInfo
            var cultureInfo = new Classes.CultureInfo.Persian();
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            #endregion
        }

        private bool IsExcludedPath(string path, HashSet<string> excludePaths)
        {
            // بررسی وجود مسیر در لیست استثناها
            return excludePaths.Any(excludePath => path.StartsWith(excludePath, StringComparison.OrdinalIgnoreCase));
        }


        //private bool IsExcludedPath(string path, string[] excludePaths)
        //{
        //    foreach (string excludePath in excludePaths)
        //    {
        //        if (path.StartsWith(excludePath, StringComparison.OrdinalIgnoreCase))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                if (User?.Identity?.IsAuthenticated == true)
                {
                    // دریافت کوکی احراز هویت
                    System.Web.HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName];
                    if (authCookie != null)
                    {
                        // استخراج اطلاعات از کوکی
                        System.Web.Security.FormsAuthenticationTicket authTicket = System.Web.Security.FormsAuthentication.Decrypt(authCookie.Value);
                        if (authTicket != null)
                        {
                            // تاریخ ایجاد کوکی (با دقت کامل)
                            DateTime cookieCreationTime = authTicket.IssueDate;

                            // دریافت تاریخ تغییر رمز عبور از دیتابیس (با دقت کامل)
                            string username = User.Identity.Name;
                            bool isSafe;
                            DateTime dbPasswordChangedTime = GetPasswordChangedTime(username, out isSafe);
                            if (Membership.GetUser(username).IsLockedOut == null)
                            {
                                if (Request.Cookies["userId"] != null)
                                {
                                    Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
                                }
                            }
                            // اگر تاریخ ایجاد کوکی قدیمی‌تر از تاریخ تغییر رمز عبور باشد، کاربر را ساین‌اوت کنید
                            if (cookieCreationTime < dbPasswordChangedTime || !isSafe || Membership.GetUser(username).IsLockedOut == true)
                            {
                                string cacheKey = $"PasswordChangedTime_{username}";
                                if (HttpRuntime.Cache[cacheKey] != null)
                                {
                                    HttpRuntime.Cache.Remove(cacheKey);
                                }
                                System.Web.Security.FormsAuthentication.SignOut();
                                Session.Abandon();

                                // حذف کوکی احراز هویت
                                System.Web.HttpContext.Current.Response.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName].Expires = DateTime.UtcNow.AddYears(-1);
                            }
                        }
                    }
                }

            }
            catch { }
        }
        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}