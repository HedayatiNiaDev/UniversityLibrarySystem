using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections.Specialized;

namespace UniversityLibrarySystem
{
    public partial class BookDetail : System.Web.UI.Page
    {
        long id = 0;
        long categoryID = 0;

        string userName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int.TryParse(GetFirstQueryValue(Request.QueryString, "success"), out var reqStatus);
            switch (reqStatus)
            {
                case 1:
                    LiteralMSG.Text = Classes.Components.alert("رزرو با موفقیت ثبت شد", Classes.Components.AlertStyle.success);
                    break;
                case 2:
                    LiteralMSG.Text = Classes.Components.alert("رزرو با موفقیت حذف شد", Classes.Components.AlertStyle.success);
                    break;
            }
            if (RouteData.Values["bookID"] != null)
            {
                try
                {
                    id = long.Parse(RouteData.Values["bookID"].ToString());
                }
                catch
                {

                    MultiViewMain.ActiveViewIndex = 1;
                    return;
                }
            }
            else
            {

                MultiViewMain.ActiveViewIndex = 1;
                return;
            }
            if (User.Identity.IsAuthenticated)
            {
                using (var EF = new ULSDBEntities())
                {
                    var query = (from TableBook in EF.ULSTbl_Books
                                 join TableCategories in EF.ULSTbl_Categories
                                 on TableBook.CategoryId equals TableCategories.ID
                                 where TableBook.Status == true && TableBook.ID == id
                                 select TableBook).FirstOrDefault();
                    if (query == null)
                    {

                        MultiViewMain.ActiveViewIndex = 1;
                        return;
                    }
                    Page.Title = SiteConfig.mixTitle("جزئیات کتاب" + query.BookTitle);

                    if (User.IsInRole("User"))//Role User
                    {
                        var queryIsBookReserved = (from TableRes in EF.ULSTbl_Reserve
                                                   where TableRes.UserName == User.Identity.Name.ToLower() && TableRes.StatusID != ReserveStatus.Delivered && TableRes.BookID == id
                                                   select TableRes).FirstOrDefault();
                        if (queryIsBookReserved != null)
                        {
                            if ((from TableRes in EF.ULSTbl_Reserve
                                 where TableRes.UserName == User.Identity.Name.ToLower() && TableRes.StatusID == Classes.ReserveStatus.Fine
                                 select TableRes.ID).FirstOrDefault() != 0)
                                LiteralMSG.Text = Classes.Components.alert("شما در حال جریمه هستید! لطفاً اقدامات لازم را جهت تسویه حساب انجام دهید،برای مشاهده جریمه ها به پنل دانشجو مراجعه فرمایید", Classes.Components.AlertStyle.danger);
                            if (queryIsBookReserved.StatusID == ReserveStatus.TempReservation)
                            {
                                MultiView1.SetActiveView(DeleteReserveView);
                            }
                            else
                            {
                                MultiView1.SetActiveView(ReservedView);
                            }
                        }
                        else if ((from TableRes in EF.ULSTbl_Reserve
                                  where TableRes.UserName == User.Identity.Name.ToLower() && TableRes.StatusID == Classes.ReserveStatus.Fine
                                  select TableRes.ID).FirstOrDefault() != 0)
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "<a class=\"btn btn-danger\" href=\"./Student/Books?Mode=3\">مشاهده جریمه ها</a></br>شما در حال جریمه هستید! لطفاً اقدامات لازم را جهت تسویه حساب انجام دهید.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (query.Available > 0)
                        {
                            if (Classes.SiteConfig.siteCanRes())
                            {
                                MultiView1.SetActiveView(ReserveView);
                            }
                            else
                            {
                                lblMessage.Visible = true;
                                lblMessage.Text = "سامانه رزرو در حال حاضر غیرفعال است. لطفاً در زمان دیگری تلاش کنید.";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                            MultiView1.SetActiveView(UnavailableView);
                    }
                    else
                        MultiView1.SetActiveView(AdminView);

                }
            }
            else
            {
                using (var EF = new ULSDBEntities())
                {
                    var query = (from TableBook in EF.ULSTbl_Books
                                 join TableCategories in EF.ULSTbl_Categories
                                 on TableBook.CategoryId equals TableCategories.ID
                                 where TableBook.Status == true && TableBook.ID == id
                                 select TableBook).FirstOrDefault();
                    if (query == null)
                    {
                        MultiView1.ActiveViewIndex = 1;
                        return;
                    }
                    MultiView1.SetActiveView(NeedLogin);
                }
            }
        }

        private string GetFirstQueryValue(NameValueCollection queryString, string key)
        {
            string[] values = queryString.GetValues(key);
            if (values != null)
            {
                return values[0];
            }
            return null;
        }
        public string ID2hash() => Hash2ID.ID2hash(id);

        #region Button

        public int GetReservationCount()
        {
            using (var libraryDBEntities = new ULSDBEntities())
            {
                if (User.Identity.IsAuthenticated)
                {
                    var queryCounter = (from TableRes in libraryDBEntities.ULSTbl_Reserve
                                        where TableRes.UserName == User.Identity.Name && TableRes.StatusID != 4
                                        select TableRes).Count();
                    return queryCounter;
                }
                return -1;
            }
        }

        //حذف کردن
        protected void btnDelReserve_Click(object sender, EventArgs e)
        {
            if (id > 0 && User.Identity.Name != null)
            {
                try
                {
                    using (ULSDBEntities lib = new ULSDBEntities())
                    {
                        // پیدا کردن رزرو موجود
                        var reservation = lib.ULSTbl_Reserve
                            .FirstOrDefault(r => r.BookID == id &&r.StatusID==ReserveStatus.TempReservation && r.UserName == User.Identity.Name);

                        if (reservation == null)
                        {
                            LiteralMSG.Text = Classes.Components.alert("رزرو موقت یافت نشد", Classes.Components.AlertStyle.danger);
                            return;
                        }

                        // حذف رزرو و برگرداندن موجودی کتاب
                        var book = lib.ULSTbl_Books.FirstOrDefault(b => b.ID == id);
                        if (book != null)
                        {
                            book.Available += 1;
                        }

                        lib.ULSTbl_Reserve.Remove(reservation);

                        lib.SaveChanges();
                        Response.Redirect("/BookDetail-" + id + "-link");
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Unexception Error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    // ری‌لود صفحه پس از انجام عملیات
                    Response.Redirect("/BookDetail-" + id + "-link?success=2");
                }
            }
            else
            {
                LiteralMSG.Text = Classes.Components.alert("شناسه کتاب یا کاربر نامعتبر است.", Classes.Components.AlertStyle.danger);
            }
        }

        //رزرو کردن
        protected void btnReserve_Click(object sender, EventArgs e)
        {
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                var book = EF.ULSTbl_Books.FirstOrDefault(b => b.ID == id);

                if (book == null || book.Available <= 0)
                {
                    LiteralMSG.Text = Classes.Components.alert("کتاب مورد نظر در حال حاضر موجود نمی باشد", Classes.Components.AlertStyle.danger);
                    return;
                }
                var queryIsBookReserved = (from TableRes in EF.ULSTbl_Reserve
                                           where TableRes.UserName == User.Identity.Name.ToLower() && TableRes.StatusID != ReserveStatus.Delivered && TableRes.BookID == id
                                           select TableRes).FirstOrDefault();
                if (queryIsBookReserved != null)
                {
                    Response.Redirect("Dash");
                    return;
                }
                var query = (from SiteTable in EF.ULSTbl_SiteSetting
                             select new
                             {
                                 SiteTable.MaxUserReserve
                             }).FirstOrDefault();

                if (query != null)
                {
                    if (GetReservationCount() < query.MaxUserReserve)
                    {
                        var queryUser = (from TableUser in EF.ULSTbl_Users
                                         where TableUser.StatusID == UserStatus.Active
                                         select TableUser);
                        if (queryUser != null)
                        {
                            ULSTbl_Reserve TG = new ULSTbl_Reserve
                            {
                                UserName = Membership.GetUser().UserName,
                                BookID = id,
                                CustomCode = null,
                                ReserveStartDate = DateTime.Now,
                                ReserveEndDate = DateTime.Now.AddDays(4),
                                DeliveryDate = null,
                                StatusID = ReserveStatus.TempReservation,
                                Renewal = 0
                            };
                            EF.ULSTbl_Reserve.Add(TG);
                            book.Available--;
                            EF.SaveChanges();
                            Response.Redirect("/BookDetail-" + id + "-link" + "?success=1");
                        }
                        else
                        {
                            LiteralMSG.Text = Classes.Components.alert("شما در حال حاضر مجاز به رزرو کتاب نمی باشید", Classes.Components.AlertStyle.danger);
                        }
                    }
                    else
                    {
                        LiteralMSG.Text = Classes.Components.alert("شما به حداکثر تعداد مجاز برای رزرو کتاب رسیده‌اید", Classes.Components.AlertStyle.danger);
                    }
                }
                else
                {
                    LiteralMSG.Text = Classes.Components.alert("تنظیمات سایت در حال حاضر خالی می‌باشد", Classes.Components.AlertStyle.danger);
                }

            }

        }
        #endregion  



        public string BottomNav()
        {
            string html = "";
            using (var EF = new ULSDBEntities())
            {
                var query = (from TableBook in EF.ULSTbl_Books
                             join TableCategories in EF.ULSTbl_Categories
                             on TableBook.CategoryId equals TableCategories.ID
                             where TableBook.Status == true && TableBook.ID == id
                             select new
                             {
                                 TableBook.CategoryId,
                                 TableBook.BookTitle,
                                 TableCategories.Title
                             }).FirstOrDefault();
                if (query == null)
                {
                    MultiView1.ActiveViewIndex = 1;
                    return "";
                }
                try
                {
                    categoryID = long.Parse(query.CategoryId.ToString());
                    html = @"                    <li>
                        <a href='categories-" + query.CategoryId + @"-" + query.Title + @"' class='breadcrumb-link'>" + query.Title + @"</a>
                    </li>
                    <li class='chevron'><span class='fa fa-chevron-left'></span></li>
                    <li>
                        <span class='breadcrumb-active'>" + query.BookTitle + @"</span>
                    </li>";
                }
                catch
                {
                    MultiView1.ActiveViewIndex = 1;
                    return "";
                }
            }
            return html;
        }

        public string BookShortDetail()
        {
            string html = "";
            try
            {
                using (var EF = new ULSDBEntities())
                {
                    var query = (from TableBook in EF.ULSTbl_Books
                                 where TableBook.Status == true && TableBook.ID == id
                                 select TableBook).FirstOrDefault();
                    if (query != null)
                    {
                        html = @"                                <div class='single-book d-flex align-items-center mb-0'>
                                    <div class='book-img'>
                                        <img src='img/books/" + query.PicName + @"' alt=''>
                                    </div>
                                    <div class='book-caption'>
                                        <h3>" + query.BookTitle + @"</h3>
                                        <p>نویسنده: " + query.AuthorName + @"</p>";
                        if (!string.IsNullOrEmpty(query.TranslatorName))
                        {
                            html += @"<p>مترجم: " + query.TranslatorName + @"</p>";
                        }
                        html += @"
                                        <p>ناشر: " + query.PublisherName + @"</p>
                                        <div class=""disable-select-text mb-2"">";
                        //Special Book Badge
                        html += @"                                        <span class=""p-1";
                        if (query.IsSpecial == null || query.IsSpecial == false)
                            html += " opa-0";
                        html += @" custom-badge-success"">کتاب ویژه</span>";
                        html += @"</div>";
                        html += @"                             <script>
    var currentUrl = window.location.href;
    var urlParts = currentUrl.split('-');
    if (urlParts.length >= 3) {
        urlParts[urlParts.length - 1] = """ + Uri.EscapeDataString(query.BookTitle) + @""";
    }
    var newUrl = urlParts.join('-');
    window.history.pushState({ path: newUrl }, '', newUrl);
                            </script>";
                    }
                    else
                    {
                        MultiView1.ActiveViewIndex = 1;
                        return "";
                    }
                }
            }
            catch
            {
                MultiView1.ActiveViewIndex = 1;
                return "";
            }
            return html;
        }

        public string BookLongDetail()
        {
            string html = "";
            using (var EF = new ULSDBEntities())
            {
                var query = (from TableBook in EF.ULSTbl_Books
                             where TableBook.Status == true && TableBook.ID == id
                             select TableBook).FirstOrDefault();
                if (query != null)
                {
                    html = @" <section class='our-client section-padding best-selling mx-5'>
            <div class='row'>
                <div class='table-book offset-xl-1 col-lg-9'>
                    <table class='details'>
                        <thead>
                            <tr>
                                <th class='first'>نام کتاب</th>
                                <th class='second'>" + query.BookTitle + @"<br>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class='first'>نویسنده</td>
                                <td class='second'>" + query.AuthorName + @"</td>
                            </tr>";
                    if (!string.IsNullOrEmpty(query.TranslatorName))
                    {
                        html += @"                            <tr>
                                <td class='first'>مترجم</td>
                                <td class='second'>" + query.TranslatorName + @"</td>
                            </tr>";
                    }
                    html += @"
                            <tr>
                                <td class='first'>ناشر</td>
                                <td class='second'>" + query.PublisherName + @"</td>
                            </tr>";
                    if (!string.IsNullOrEmpty(query.ISBN))
                    {
                        html += @"                            <tr>
                                <td class='first'>شابک</td>
                                <td class='second'>" + query.ISBN + @"</td>
                            </tr>";
                    }
                    html+=@"                        </tbody>
                    </table>
                </div>
            </div>
        </section>
";
                }
                else
                {
                    MultiView1.ActiveViewIndex = 1;
                    return "";
                }
            }
            return html;
        }
        public string NewBook()
        {
            string html = "";
            using (var EF = new ULSDBEntities())
            {
                var queryNewBook = (from TableBook in EF.ULSTbl_Books
                                    join TableCategory in EF.ULSTbl_Categories
                                    on TableBook.CategoryId equals TableCategory.ID
                                    where TableBook.CategoryId == categoryID && TableBook.ID != id && TableBook.Available > 0 && TableBook.Status == true && TableCategory.Status == true
                                    orderby TableBook.ID descending
                                    select TableBook).Take(6);
                foreach (var tableBook in queryNewBook)
                {
                    html += @"
                        <div class=""col-xxl-3 col-xl-4 col-lg-4 col-md-12 col-sm-6"">
                            <div class='properties pb-30'>";
                    int Available = -1;
                    if (tableBook.Available != null)
                    {
                        if (tableBook.Available == 0)
                        {
                            Available = 0;
                        }
                        else
                        {
                            Available = 1;
                        }
                    }
                    bool IsSpecial = false;
                    if (tableBook.IsSpecial != null)
                    {
                        IsSpecial = tableBook.IsSpecial == true;
                    }
                    html += Components.bookCard(tableBook.ID, tableBook.BookTitle, tableBook.PicName, tableBook.AuthorName, tableBook.PublisherName, Available, IsSpecial);
                    html += @"                            </div>
                        </div>
";
                }
            }
            if (html == "")
                html = "<p>تمامی کتاب‌ها رزرو شده‌اند.</p>";
            return html;
        }
    }
}