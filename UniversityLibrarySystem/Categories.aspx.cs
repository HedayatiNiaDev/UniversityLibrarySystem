using Classes;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem
{
    public partial class Categories : System.Web.UI.Page
    {
        int CatID = 0;
        int numCard = 12;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (RouteData.Values["categoryID"] != null)
            {
                try
                {
                    CatID = int.Parse(RouteData.Values["categoryID"].ToString());
                    using (var EF = new ULSDBEntities())
                    {
                        var query = (from Table in EF.ULSTbl_Categories
                                     where Table.ID == CatID && Table.Status == true
                                     select Table).FirstOrDefault();
                        if (query != null)
                        {
                            Page.Title = SiteConfig.mixTitle("دسته بندی");

                        }
                        else
                        {
                            Response.StatusCode = 404;
                        }
                    }
                }
                catch
                {
                    CatID = 0;
                    Response.Redirect("/categories");
                }
            }
            else
            {
                CatID = 0;
                Page.Title = SiteConfig.mixTitle("دسته بندی");
            }

            if (string.IsNullOrEmpty(LastID.Text))
            {
                LastID.Text = long.MaxValue.ToString();
                Books.Text = GetBook();
            }
        }

        public string GetLinksTitle()
        {
            if (CatID != 0)
            {
                using (var EF = new ULSDBEntities())
                {
                    var query = (from Table in EF.ULSTbl_Categories
                                 where Table.ID == CatID && Table.Status == true
                                 select Table).FirstOrDefault();
                    if (query != null)
                        return @"                    <li>
                        <a href=""/Categories"" class=""breadcrumb-link"">دسته بندی</a>
                    </li>
                    <li class=""chevron""><span class=""fa fa-chevron-left""></span></li>
                    <li>
                        <span class=""breadcrumb-active"">" + query.Title + @"</span>
                    </li>
";
                }
            }
            return @"                    <li>
                        <span class='breadcrumb-active'>دسته بندی</span>
                    </li>";
        }


        public string LinksCategories()
        {
            string html = "";
            var categories = SiteConfig.GetCategories(); // دریافت دسته‌بندی‌ها از کش یا پایگاه داده

            if (categories != null && categories.Any())
            {
                foreach (var item in categories)
                {
                    html += @"
                <a href='/Categories-" + item.ID + @"-" + item.Title + @"' class='container'>
                    <svg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='currentColor' class='bi bi-chevron-left' viewBox='0 0 16 16'>
                        <path fill-rule='evenodd' d='M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0'/>
                    </svg>
                    " + item.Title + @"
                </a>";
                }
            }

            return html;
        }

        public string GetBook()
        {
            string html = "";
            long lsID = long.Parse(LastID.Text);
            using (var EF = new ULSDBEntities())
            {
                var queryNewBook = (from TableBook in EF.ULSTbl_Books
                                    join TableCategory in EF.ULSTbl_Categories
                                    on TableBook.CategoryId equals TableCategory.ID
                                    where lsID >= TableBook.ID && TableBook.Status == true && TableCategory.Status == true && (CatID == 0 || TableBook.CategoryId == CatID)
                                    orderby TableBook.ID descending
                                    select TableBook).Take(numCard+1).ToList();

                if (queryNewBook.Count == numCard+1)
                {
                    NextPage.Visible = true;
                    queryNewBook.RemoveAt(numCard);
                }
                else
                {
                    NextPage.Visible = false;
                }
                try
                {
                    if (queryNewBook != null)
                        LastID.Text = (queryNewBook[numCard - 1].ID-1).ToString();
                }
                catch
                {
                    LastID.Text = "0";
                }
                foreach (var tableBook in queryNewBook)
                {
                    bool isSpecial = tableBook.IsSpecial == true;
                    int Available = -1;
                    if (tableBook.Available != null)
                    {
                        if (tableBook.Available == 0)
                            Available = 0;
                        else
                            Available = 1;
                    }
                    html += @"                        <div class=""col-xl-3 col-lg-4 col-md-4 col-6"">
                            <div class='properties pb-30'>";
                    html += Components.bookCard(tableBook.ID, tableBook.BookTitle, tableBook.PicName, tableBook.AuthorName, tableBook.PublisherName, Available, isSpecial);
                    html += @"                            </div>
                        </div>";
                }
            }
            return html;
        }

        protected void NextPage_Click(object sender, EventArgs e) =>
            Books.Text += GetBook();
    }
}