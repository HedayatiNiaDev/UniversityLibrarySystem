using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = SiteConfig.getSiteName();
        }

        public string SpecialBook()
        {
            const string cacheKey = "SpecialBookCacheKey";
            if (HttpRuntime.Cache[cacheKey] != null)
                return HttpRuntime.Cache[cacheKey] as string;

            string html = "";
            using (var EF = new ULSDBEntities())
            {
                var queryNewBook = (from TableBook in EF.ULSTbl_Books
                                    join TableCategory in EF.ULSTbl_Categories
                                    on TableBook.CategoryId equals TableCategory.ID
                                    where TableBook.Available > 0 && TableBook.IsSpecial == true && TableBook.Status == true && TableCategory.Status == true
                                    orderby TableBook.ID descending
                                    select TableBook).Take(10);
                foreach (var tableBook in queryNewBook)
                {
                    html += @"<div class='properties pb-20'>";
                    html += Components.bookCard(tableBook.ID, tableBook.BookTitle, tableBook.PicName, tableBook.AuthorName, tableBook.PublisherName, -1, tableBook.IsSpecial == true);
                    html += @"</div>";
                }
            }
            if (html == "")
                html = "<p>تمامی کتاب‌ها رزرو شده‌اند.</p>";

            HttpRuntime.Cache.Insert(cacheKey, html, null, DateTime.Now.AddSeconds(20), System.Web.Caching.Cache.NoSlidingExpiration);
            return html;
        }
        public string CategoriesBookHScroll()
        {
            const string cacheKey = "CategoriesBookHScrollCacheKey";
            if (HttpRuntime.Cache[cacheKey] != null)
                return HttpRuntime.Cache[cacheKey] as string;

            string html = "";
            var categories = SiteConfig.GetCategories(); // دریافت دسته‌بندی‌ها از کش یا پایگاه داده

            if (categories != null && categories.Any())
            {
                foreach (var tableCategory in categories.OrderByDescending(c => c.ID))
                {
                    html += @"
            <div class='category-item'>
                <a href='Categories-" + tableCategory.ID + "-" + Uri.EscapeDataString(tableCategory.Title) + @"' class='category-img'><img src='../img/categories/" + tableCategory.PicName + @"' alt='" + tableCategory.Title + @"'
                        loading='lazy'></a>
                <a class='category-caption' href='Categories-" + tableCategory.ID + "-" + tableCategory.Title.Replace(" ", "_").Replace("\u200C", "_") + @"'>" + tableCategory.Title + @"</a>
            </div>
            ";
                }
            }

            HttpRuntime.Cache.Insert(cacheKey, html, null, DateTime.Now.AddSeconds(20), System.Web.Caching.Cache.NoSlidingExpiration);
            return html;
        }

        public string NewBook()
        {
            const string cacheKey = "NewBookCacheKey";
            if (HttpRuntime.Cache[cacheKey] != null)
                return HttpRuntime.Cache[cacheKey] as string;

            string html = "";
            using (var EF = new ULSDBEntities())
            {
                var queryNewBook = (from TableBook in EF.ULSTbl_Books
                                    join TableCategory in EF.ULSTbl_Categories
                                    on TableBook.CategoryId equals TableCategory.ID
                                    where TableBook.Available > 0 && TableBook.Status == true && TableCategory.Status == true
                                    orderby TableBook.ID descending
                                    select TableBook).Take(6);
                foreach (var tableBook in queryNewBook)
                {
                    html += @"<div class='col-xl-2 col-lg-3 col-md-4 col-6'>
                        <div class='properties pb-30'>";
                    bool isSpecial = tableBook.IsSpecial == true;
                    html += Components.bookCard(tableBook.ID, tableBook.BookTitle, tableBook.PicName, tableBook.AuthorName, tableBook.PublisherName, -1, isSpecial);
                    html += @"</div>
                    </div>";
                }
            }
            if (html == "")
                html = "<p>تمامی کتاب‌ها رزرو شده‌اند.</p>";

            HttpRuntime.Cache.Insert(cacheKey, html, null, DateTime.Now.AddSeconds(20), System.Web.Caching.Cache.NoSlidingExpiration);
            return html;
        }
    }
}