using Classes;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem.Manager
{
    public partial class Reservations : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            multiView.ActiveViewIndex = 0;
            PanelSearch.Visible = multiView.ActiveViewIndex == 0;
            if (Request.QueryString["Value"] != null)
            {
                using (var EF = new ULSDBEntities())
                {
                    var query = (from book in EF.ULSTbl_Books
                                 where book.Status == true
                                 select book.ID).FirstOrDefault();
                    if (query != null)
                    {
                        multiView.SetActiveView(New);
                        PanelSearch.Visible = false;
                    }
                    else
                    {
                        LiteralMessage.Text = Components.alert("کتاب فعالی وجود ندارد", Components.AlertStyle.danger);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;
                    }
                }
            }
            if (Request.QueryString["View"] != null)
            {
                string HashId = Request.QueryString["View"];
                int id = Hash2ID.HAsh2ID(HashId);
                multiView.ActiveViewIndex = 1;
                PanelSearch.Visible = multiView.ActiveViewIndex == 0;
                Clean();
                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    literalView.Text = "";
                    var query = (from tblRes in EF.ULSTbl_Reserve
                                 where tblRes.ID == id
                                 join tblUser in EF.ULSTbl_Users
                                 on tblRes.UserName equals tblUser.UserName
                                 join tblBooks in EF.ULSTbl_Books
                                 on tblRes.BookID equals tblBooks.ID
                                 orderby tblRes.ID descending
                                 select new
                                 {
                                     UserName = tblRes.UserName,
                                     tblBooks.PicName,
                                     tblBooks.BookTitle,
                                     tblUser.FullName,
                                     UserStatus = tblUser.StatusID,
                                     tblRes.ReserveStartDate,
                                     tblRes.ReserveEndDate,
                                     tblRes.DeliveryDate,
                                     tblRes.StatusID,
                                     tblRes.Renewal,
                                     tblBooks.ISBN,
                                     tblRes.CustomCode
                                 }).FirstOrDefault();
                    if ((from TableRes in EF.ULSTbl_Reserve
                         where TableRes.UserName == query.UserName.ToLower() && TableRes.StatusID == ReserveStatus.Fine
                         select TableRes).FirstOrDefault() == null)
                    {
                        LiteralMessage.Text = Classes.Components.alert("کاربر مورد نظر در حال جریمه می باشد", Classes.Components.AlertStyle.danger);
                    }
                    if (query != null)
                    {
                        switch ((int)query.StatusID)
                        {
                            case 1:
                                multiButton.ActiveViewIndex = 0;
                                break;
                            case 2:
                                multiButton.ActiveViewIndex = 1;
                                break;
                            case 3:
                                TimeSpan timeSpan = DateTime.Now - (DateTime)query.ReserveEndDate;
                                var getLiability = (from site in EF.ULSTbl_SiteSetting
                                                    where site.ID != 0
                                                    select site.Liability).FirstOrDefault();
                                if (getLiability != null)
                                {
                                    lblFine.Text += timeSpan.Days * getLiability;
                                    lblFine.Text += "ريال";
                                }
                                else
                                {
                                    lblFine.Text += "خطا در محاسبه";
                                }
                                multiButton.ActiveViewIndex = 2;
                                break;
                        }
                        ImageNopic.ImageUrl = query.PicName != null ? "../img/books/" + query.PicName : "../img/Error/no-photo.png";
                        txtBookName.Text += query.BookTitle;
                        txtStudentName.Text += query.FullName;
                        txtStudentUserName.Text += query.UserName;
                        txtStudentStatus.Text += UserStatus.UserStatusToText(query.UserStatus);
                        txtStartDate.Text += ConvertToPersianDate(query.ReserveStartDate);
                        txtEndDate.Text += ConvertToPersianDate(query.ReserveEndDate);
                        txtDeliveryDate.Text += ConvertToPersianDate(query.DeliveryDate);
                        txtStatus.Text += ReserveStatus.ReserveStatusToText(query.StatusID);
                        DivDeliveryDate.Visible = query.StatusID > ReserveStatus.Fine;
                        DivRenewal.Visible = query.StatusID > ReserveStatus.TempReservation;
                        txtRenewal.Text += query.Renewal.ToString();
                        txtISBN.Text += query.ISBN;
                        txtOrderCode.Text += string.IsNullOrEmpty(query.CustomCode) ? "--" : query.CustomCode;
                        DivOrderCode.Visible = query.StatusID > ReserveStatus.TempReservation;

                    }

                }

            }
        }

        private void Clean()
        {
            txtBookName.Text = "";
            txtStudentName.Text = "";
            txtStudentUserName.Text = "";
            txtStudentStatus.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtDeliveryDate.Text = "";
            txtStatus.Text = "";
            txtRenewal.Text = "";
            txtISBN.Text = "";
            txtOrderCode.Text = "";
        }


        #region [HashID]

        private Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string ID2hash(object id)
        {
            Random m = new Random();
            string RandomText = RandomString(40);
            return RandomText + id.ToString() + m.Next(10000, 99999).ToString();
        }
        #endregion

        public string ConvertToPersianDate(DateTime? date)
        {
            if (date == null)
            {
                return "--";
            }
            var persianCalendar = new System.Globalization.PersianCalendar();
            var persianDate = $"{persianCalendar.GetYear(date.Value)}/{persianCalendar.GetMonth(date.Value):00}/{persianCalendar.GetDayOfMonth(date.Value):00}";
            return persianDate;
        }


        #region [LoadTabel]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinqDataSourceNews_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                string text = string.Empty;
                string mode = string.Empty;
                string uid = string.Empty;
                if (!string.IsNullOrEmpty(Request.QueryString["txtText"]))
                {
                    text = Request.QueryString["txtText"].ToString();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
                {
                    uid = Request.QueryString["uid"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["Mode"]))
                {
                    mode = Request.QueryString["Mode"].ToString().Trim();
                }
                var qGetInfo = (from item in EF.ULSTbl_Books
                                join ite in EF.ULSTbl_Categories
                                on item.CategoryId equals ite.ID
                                from res in EF.ULSTbl_Reserve
                                join it in EF.ULSTbl_Users
                                on res.UserName equals it.UserName
                                where (string.IsNullOrEmpty(text) || item.BookTitle.Contains(text)) && res.BookID == item.ID
                                && (string.IsNullOrEmpty(mode) || mode == "0" || ((mode == "1" && res.StatusID == ReserveStatus.TempReservation) || (mode == "2" && res.StatusID == ReserveStatus.Reservation) || (mode == "3" && res.StatusID == ReserveStatus.Fine) || (mode == "4" && res.StatusID == ReserveStatus.Delivered)))
                                && (string.IsNullOrEmpty(uid) || uid == res.UserName)
                                orderby res.ID descending
                                select new
                                {
                                    UserName = res.UserName,
                                    UserID = it.ID,
                                    res.ID,
                                    BookID = item.ID,
                                    BookTitle = item.BookTitle,
                                    PicName = item.PicName,
                                    Status = res.StatusID,
                                    CatName = ite.Title,
                                    NameFamily = it.FullName,
                                    ReserveStartDate = res.ReserveStartDate,
                                    ReserveEndDate = res.ReserveEndDate,
                                    DeliveryDate = res.DeliveryDate,
                                    Renewal = res.Renewal,
                                    ISBN = item.ISBN,
                                    AdderPic = it.PicName,
                                    UserStatus = it.StatusID
                                }).AsEnumerable().Select(x => new
                                {
                                    UserNameLink = ID2hash(x.UserID),
                                    UserName = x.UserName,
                                    x.ID,
                                    x.BookID,
                                    x.BookTitle,
                                    x.PicName,
                                    x.NameFamily,
                                    ResTempStartDate = (x.ReserveStartDate != null) ? ConvertToPersianDate(x.ReserveStartDate) : "--",
                                    ResTempEndDate = x.ReserveEndDate != null ? ConvertToPersianDate(x.ReserveEndDate) : "--",
                                    StatusHtml = ReserveStatus.badgeStatus(x.Status),
                                    DelEndDate = x.DeliveryDate != null ? ConvertToPersianDate(x.DeliveryDate) : "--",
                                    UserStatus = UserStatusHtml(x.UserStatus),
                                    ButtonsHtml = @"<a class='btn btn-primary' href='?View=" + ID2hash(x.ID) + @"'>جزئیات</a>",
                                    Renewal = x.Renewal,
                                    x.CatName,
                                    x.ISBN,
                                    x.AdderPic
                                }).ToList();


                if (qGetInfo == null)
                    e.Result = "<div class='alert alert-danger mb-2' role='alert'>اطلاعاتی درج نشده است</div>";
                else
                    e.Result = qGetInfo;
            }
        }

        public string UserStatusHtml(int? id)
        {
            string text = "";
            if (id == UserStatus.Active)
            {
                text += @"<td><span class='badge bg-label-success me-1'>فعال</span></td>";
            }
            else if (id == UserStatus.NeedVerify)
            {
                text += @"<td><span class='badge bg-label-warning me-1'>تایید نشده</span></td>";
            }
            else if (id == UserStatus.Fine)
            {
                text += @"<td><span class='badge bg-label-danger me-1'>در حال جریمه</span></td>";
            }
            else if (id == -2)
            {
                text += @"<td><span class='badge bg-label-dark me-1'>غیرفعال(جریمه)</span></td>";
            }
            else
            {
                text += @"<td><span class='badge bg-label-dark me-1'>غیرفعال</span></td>";
            }


            return text;
        }

        protected void btnRes_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["View"] != null)
            {
                string HashId = Request.QueryString["View"];
                int id = Hash2ID.HAsh2ID(HashId);
                try
                {
                    using (var EF = new ULSDBEntities())
                    {
                        var getTempRes = (from tblres in EF.ULSTbl_Reserve
                                          where tblres.StatusID == ReserveStatus.TempReservation && tblres.ID == id
                                          join tblBook in EF.ULSTbl_Books
                                          on tblres.BookID equals tblBook.ID
                                          join tblCat in EF.ULSTbl_Categories
                                          on tblBook.CategoryId equals tblCat.ID
                                          select tblres).FirstOrDefault();
                        var getReserveDay = (from site in EF.ULSTbl_SiteSetting
                                             where site.ID != 0
                                             select site.ReserveDay).FirstOrDefault();
                        getTempRes.CustomCode = txtCustomCode.Text;
                        getTempRes.ReserveStartDate = DateTime.Now;
                        getTempRes.ReserveEndDate = DateTime.Now.Add(TimeSpan.Parse(getReserveDay.ToString()));
                        getTempRes.StatusID = ReserveStatus.Reservation;
                        EF.SaveChanges();
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = multiView.ActiveViewIndex == 0;

                        LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;
                    }
                }
                catch
                {
                    LiteralMessage.Text = Components.alert("عملیات با شکست مواجعه شد", Components.AlertStyle.danger);
                    multiView.ActiveViewIndex = 0;
                    PanelSearch.Visible = true;
                    return;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["View"] != null)
            {
                string HashId = Request.QueryString["View"];
                int id = Hash2ID.HAsh2ID(HashId);
                try
                {
                    using (var EF = new ULSDBEntities())
                    {
                        var getTempRes = (from tblres in EF.ULSTbl_Reserve
                                          where tblres.StatusID == ReserveStatus.TempReservation && tblres.ID == id
                                          select tblres).FirstOrDefault();
                        if (getTempRes != null)
                        {
                            var book = (from b in EF.ULSTbl_Books
                                        where b.ID == getTempRes.BookID
                                        select b).FirstOrDefault();
                            if (book != null)
                            {
                                book.Available += 1;
                                EF.ULSTbl_Reserve.Remove(getTempRes);
                                EF.SaveChanges();
                                LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                                multiView.ActiveViewIndex = 0;
                                PanelSearch.Visible = true;
                                return;
                            }
                            else
                            {
                                LiteralMessage.Text = Components.alert("عملیات با شکست مواجعه شد", Components.AlertStyle.danger);
                                multiView.ActiveViewIndex = 0;
                                PanelSearch.Visible = true;
                                return;
                            }
                        }
                        else
                        {
                            LiteralMessage.Text = Components.alert("عملیات با شکست مواجعه شد", Components.AlertStyle.danger);
                            multiView.ActiveViewIndex = 0;
                            PanelSearch.Visible = true;
                            return;
                        }

                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = multiView.ActiveViewIndex == 0;


                    }
                }
                catch
                {
                    LiteralMessage.Text = Components.alert("عملیات با شکست مواجعه شد", Components.AlertStyle.danger);
                    multiView.ActiveViewIndex = 0;
                    PanelSearch.Visible = true;
                    return;
                }
            }

        }
        //Delivered
        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["View"] != null)
            {
                string HashId = Request.QueryString["View"];
                int id = Hash2ID.HAsh2ID(HashId);
                try
                {
                    using (var EF = new ULSDBEntities())
                    {
                        var getTempRes = (from tblres in EF.ULSTbl_Reserve
                                          where tblres.StatusID == ReserveStatus.Reservation || tblres.StatusID == ReserveStatus.Fine && tblres.ID == id
                                          join tblBook in EF.ULSTbl_Books
                                          on tblres.BookID equals tblBook.ID
                                          join tblCat in EF.ULSTbl_Categories
                                          on tblBook.CategoryId equals tblCat.ID
                                          select tblres).FirstOrDefault();
                        getTempRes.DeliveryDate = DateTime.Now;
                        if (getTempRes.StatusID == ReserveStatus.Fine)
                        {
                            string fineText="";
                            TimeSpan timeSpan = DateTime.Now - (DateTime)getTempRes.ReserveEndDate;
                            var getLiability = (from site in EF.ULSTbl_SiteSetting
                                                where site.ID != 0
                                                select site.Liability).FirstOrDefault();
                            if (getLiability != null)
                            {
                                fineText += timeSpan.Days * getLiability;
                            }
                            else
                            {
                                fineText += "خطا در محاسبه";
                            }
                            getTempRes.CustomCode += "\nپرداخت جریمه به مبلغ " + fineText + " ریال انجام شد و رسید آن دریافت گردید.";
                        }
                        getTempRes.StatusID = ReserveStatus.Delivered;
                        EF.SaveChanges();
                        LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;
                    }
                }
                catch
                {
                    LiteralMessage.Text = Components.alert("عملیات با شکست مواجعه شد", Components.AlertStyle.danger);
                    multiView.ActiveViewIndex = 0;
                    PanelSearch.Visible = true;
                    return;
                }
            }
        }

        protected void btnRen_Click(object sender, EventArgs e)
        {
            string HashId = Request.QueryString["View"];
            int id = Hash2ID.HAsh2ID(HashId);
            try
            {
                using (var EF = new ULSDBEntities())
                {
                    var getTempRes = (from tblres in EF.ULSTbl_Reserve
                                      where tblres.StatusID == ReserveStatus.Reservation && tblres.ID == id
                                      join tblBook in EF.ULSTbl_Books
                                      on tblres.BookID equals tblBook.ID
                                      join tblCat in EF.ULSTbl_Categories
                                      on tblBook.CategoryId equals tblCat.ID
                                      select tblres).FirstOrDefault();
                    var getReserveDay = (from site in EF.ULSTbl_SiteSetting
                                         where site.ID != 0
                                         select site).FirstOrDefault();
                    int? Renewal;
                    try
                    {
                        Renewal = getTempRes.Renewal;
                        if (Renewal == null)
                        {
                            Renewal = 0;
                        }
                    }
                    catch
                    {
                        Renewal = 0;
                    }
                    if (Renewal < getReserveDay.ReserveAgain)
                    {
                        getTempRes.ReserveEndDate = ((DateTime)getTempRes.ReserveEndDate).Add(TimeSpan.Parse(getReserveDay.ReserveDay.ToString()));
                        getTempRes.Renewal = Renewal + 1;
                        EF.SaveChanges();
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = multiView.ActiveViewIndex == 0;

                        LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;
                    }
                    else
                    {
                        LiteralMessage.Text = Components.alert("رزرو مورد نظر به حداکثر تعداد تمدید رسیده است", Components.AlertStyle.danger);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;

                    }
                }
            }
            catch
            {
            }
        }

        public int GetReservationCount(string userID)
        {
            using (var libraryDBEntities = new ULSDBEntities())
            {
                var queryCounter = (from TableRes in libraryDBEntities.ULSTbl_Reserve
                                    where TableRes.UserName == userID.ToLower() && TableRes.StatusID != 4
                                    select TableRes).Count();
                return queryCounter;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string userText = txtUsernameNew.Text;
                string bookISBN = txtISBNNew.Text;
                string detail = txtDetail.Text;
                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    if ((from TableUser in EF.ULSTbl_Users where TableUser.UserName == userText.ToLower() select TableUser).FirstOrDefault() == null)
                    {
                        LiteralMessage.Text = Classes.Components.alert("نام کاربری وارد شده نادرست است یا غیرفعال می‌باشد", Classes.Components.AlertStyle.danger);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;
                    }

                    if ((from TableRes in EF.ULSTbl_Reserve
                         where TableRes.UserName == userText.ToLower() && TableRes.StatusID == Classes.ReserveStatus.Fine
                         select TableRes.ID).FirstOrDefault() != 0)
                    {

                        LiteralMessage.Text = Classes.Components.alert("کاربر مورد نظر در حال جریمه می باشد", Classes.Components.AlertStyle.danger);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;
                    }

                    var book = EF.ULSTbl_Books.FirstOrDefault(b => b.ISBN.Replace("-", "") == bookISBN.Replace("-", "") && b.Status == true);

                    if (book == null || book.Available <= 0)
                    {
                        LiteralMessage.Text = Classes.Components.alert("کتاب مورد نظر در حال حاضر موجود نمی باشد", Classes.Components.AlertStyle.danger);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;
                    }
                    var queryIsBookReserved = (from TableRes in EF.ULSTbl_Reserve
                                               where TableRes.UserName == userText.ToLower() && TableRes.StatusID != ReserveStatus.Delivered && TableRes.BookID == book.ID
                                               select TableRes).FirstOrDefault();
                    if (queryIsBookReserved != null)
                    {
                        LiteralMessage.Text = Classes.Components.alert("امکان رزرو یک کتاب بیش از یک‌بار وجود ندارد", Classes.Components.AlertStyle.danger);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;
                    }
                    var query = (from SiteTable in EF.ULSTbl_SiteSetting
                                 select new
                                 {
                                     SiteTable.MaxUserReserve
                                 }).FirstOrDefault();

                    if (query != null)
                    {
                        if (GetReservationCount(userText) < query.MaxUserReserve)
                        {
                            var queryUser = (from TableUser in EF.ULSTbl_Users
                                             where TableUser.StatusID == UserStatus.Active && TableUser.RoleID == Classes.UserRole.User
                                             select TableUser);
                            if (queryUser != null)
                            {
                                ULSTbl_Reserve TG = new ULSTbl_Reserve
                                {
                                    UserName = userText,
                                    BookID = book.ID,
                                    CustomCode = detail,
                                    ReserveStartDate = DateTime.Now,
                                    ReserveEndDate = DateTime.Now.AddDays(4),
                                    DeliveryDate = null,
                                    StatusID = ReserveStatus.Reservation,
                                    Renewal = 0
                                };
                                EF.ULSTbl_Reserve.Add(TG);
                                book.Available--;
                                EF.SaveChanges();
                                LiteralMessage.Text = Classes.Components.alert("عملیات با موفقیت انجام شد", Classes.Components.AlertStyle.success);
                                multiView.ActiveViewIndex = 0;
                                PanelSearch.Visible = true;
                                return;
                            }
                            else
                            {
                                LiteralMessage.Text = Classes.Components.alert("کاربر مورد نظر در حال حاضر مجاز به رزرو کتاب نمی باشید", Classes.Components.AlertStyle.danger);
                                multiView.ActiveViewIndex = 0;
                                PanelSearch.Visible = true;
                                return;
                            }
                        }
                        else
                        {
                            LiteralMessage.Text = Classes.Components.alert("کاربر مورد نظر به حداکثر تعداد مجاز برای رزرو کتاب رسیده‌اید", Classes.Components.AlertStyle.danger);
                            multiView.ActiveViewIndex = 0;
                            PanelSearch.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                        LiteralMessage.Text = Classes.Components.alert("تنظیمات سایت در حال حاضر خالی می‌باشد", Classes.Components.AlertStyle.danger);
                        multiView.ActiveViewIndex = 0;
                        PanelSearch.Visible = true;
                        return;
                    }

                }

            }
        }
    }
    #endregion



}
