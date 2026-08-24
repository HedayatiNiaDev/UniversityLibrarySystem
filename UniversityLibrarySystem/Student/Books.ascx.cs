using BotDetect;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem.Student
{
    public partial class Books : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

        public string GetDateBasedOnStatus(int? status, DateTime? tempDate, DateTime? Date, DateTime? DeliveryDate)
        {
            switch (status)
            {
                case 1:
                    return ConvertToPersianDate(tempDate);
                case 2:
                    return ConvertToPersianDate(Date);
                case 3:
                    return ConvertToPersianDate(Date);
                case 4:
                    return ConvertToPersianDate(DeliveryDate);
                default:
                    return "--";
            }

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
                if (!string.IsNullOrEmpty(Request.QueryString["txtText"]))
                {
                    text = Request.QueryString["txtText"].ToString();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["Mode"]))
                {
                    mode = Request.QueryString["Mode"].ToString().Trim();
                }
                string username = Membership.GetUser().UserName;
                long? getLiability = (from site in EF.ULSTbl_SiteSetting
                                      where site.ID != 0
                                      select site.Liability).FirstOrDefault();
                var qGetInfo = (from item in EF.ULSTbl_Books
                                join ite in EF.ULSTbl_Categories
                                on item.CategoryId equals ite.ID
                                join it in EF.ULSTbl_Users
                                on item.UserNameAdder equals it.UserName
                                join res in EF.ULSTbl_Reserve
                                on username equals res.UserName
                                orderby res.ID descending
                                where res.BookID == item.ID && (string.IsNullOrEmpty(text) || item.BookTitle.Contains(text))
                                && (string.IsNullOrEmpty(mode) || mode == "0" || ((mode == "1" && res.StatusID == ReserveStatus.TempReservation) || (mode == "2" && res.StatusID == ReserveStatus.Reservation) || (mode == "3" && res.StatusID == ReserveStatus.Fine) || (mode == "4" && res.StatusID == ReserveStatus.Delivered)))
                                select new
                                {
                                    BookID = item.ID,
                                    BookTitle = item.BookTitle,
                                    PicName = item.PicName,
                                    Status = res.StatusID,
                                    CatName = ite.Title,
                                    NameFamily = it.FullName,
                                    ReserveStartDate = res.ReserveStartDate,
                                    ReserveEndDate = res.ReserveEndDate,
                                    DeliveryDate = res.DeliveryDate,
                                    Renewal = res.Renewal
                                }).AsEnumerable().Select(x => new
                                {
                                    x.BookID,
                                    x.BookTitle,
                                    x.PicName,
                                    ResTempStartDate = x.ReserveStartDate != null ? ConvertToPersianDate(x.ReserveStartDate) : "--",
                                    ResTempEndDate = x.ReserveEndDate != null ? ConvertToPersianDate(x.ReserveEndDate) : "--",
                                    BookFine = x.Status != 3 ? "--" : getLiability == null ? "خطا در محاسبه" : ((DateTime.Now - (DateTime)x.ReserveEndDate)).Days * getLiability + "ريال",
                                    StatusHtml = ReserveStatus.badgeStatus(x.Status),
                                    x.Renewal,
                                    x.CatName,
                                    x.NameFamily
                                }).ToList();


                if (qGetInfo == null)
                    e.Result = "<div class='alert alert-danger mb-2' role='alert'>اطلاعاتی درج نشده است</div>";
                else
                    e.Result = qGetInfo;
            }
        }
    }
    #endregion
}