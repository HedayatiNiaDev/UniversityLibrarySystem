using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem.Manager
{
    public partial class Dashboard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        private void BindData()
        {
            // بروزرسانی داده‌ها
            allAccountCounter();
            PersonalCounter();
            allActiveUserCounter();
            allDeactiveCounter();
            allBookCounter();
            TempReserve();
            Reserve();
            Fine();
            InLib();

            // بروزرسانی لیست‌ویو
            ProuductDefualt.DataBind();
        }
        #region Account
        public string allAccountCounter()
        {
            try
            {
                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    var query = (from TableUser in EF.ULSTbl_Users
                                 where TableUser.UserName != "0"
                                 select TableUser.ID).Count();
                    return query.ToString();
                }
            }
            catch
            {
                return "0";
            }
        }
        public string PersonalCounter()
        {
            try
            {
                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    var query = (from TableUser in EF.ULSTbl_Users
                                 where TableUser.StatusID != UserStatus.NeedVerify && TableUser.RoleID == UserRole.Admin && TableUser.UserName != "0"
                                 select TableUser.ID).Count();
                    return query.ToString();
                }
            }
            catch
            {
                return "0";
            }
        }
        public string allActiveUserCounter()
        {
            try
            {
                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    var query = (from TableUser in EF.ULSTbl_Users
                                 where TableUser.StatusID > UserStatus.Deactive && TableUser.RoleID == UserRole.User && TableUser.UserName != "0"
                                 select TableUser.ID).Count();
                    return query.ToString();
                }
            }
            catch
            {
                return "0";
            }
        }
        public string allDeactiveCounter()
        {
            try
            {
                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    var query = (from TableUser in EF.ULSTbl_Users
                                 where TableUser.StatusID <= UserStatus.Deactive && TableUser.UserName != "0"
                                 select TableUser.ID).Count();
                    return query.ToString();
                }
            }
            catch
            {
                return "0";
            }
        }
        #endregion
        #region HashID

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

        #region Book
        public string allBookCounter()
        {
            try
            {
                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    long sum = 0;
                    var query = (from TableBook in EF.ULSTbl_Books
                                 join TableCategories in EF.ULSTbl_Categories
                                 on TableBook.CategoryId equals TableCategories.ID
                                 where TableCategories.Status == true && TableBook.Status == true
                                 select TableBook).ToList();
                    foreach (var item in query)
                    {
                        sum += (int)item.Available;
                    }
                    return sum.ToString();
                }
            }
            catch
            {
                return "0";
            }
        }
        public string TempReserve()
        {
            string html = "0";
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                var query = (from TableBook in EF.ULSTbl_Books
                             join TableCategories in EF.ULSTbl_Categories
                             on TableBook.CategoryId equals TableCategories.ID
                             where TableCategories.Status == true && TableBook.Status == true
                             join TableReserve in EF.ULSTbl_Reserve
                             on TableBook.ID equals TableReserve.BookID
                             where TableReserve.StatusID == ReserveStatus.TempReservation
                             select TableBook.ID).Count();
                html = query.ToString();
            }
            return html;
        }
        public string Reserve()
        {
            string html = "0";
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                var query = (from TableBook in EF.ULSTbl_Books
                             join TableCategories in EF.ULSTbl_Categories
                             on TableBook.CategoryId equals TableCategories.ID
                             where TableCategories.Status == true && TableBook.Status == true
                             join TableReserve in EF.ULSTbl_Reserve
                             on TableBook.ID equals TableReserve.BookID
                             where TableReserve.StatusID == ReserveStatus.Reservation
                             select TableBook.ID).Count();

                html = query.ToString();
            }
            return html;
        }
        public string Fine()
        {
            string html = "0";
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                var query = (from TableBook in EF.ULSTbl_Books
                             join TableCategories in EF.ULSTbl_Categories
                             on TableBook.CategoryId equals TableCategories.ID
                             where TableCategories.Status == true && TableBook.Status == true
                             join TableReserve in EF.ULSTbl_Reserve
                             on TableBook.ID equals TableReserve.BookID
                             where TableReserve.StatusID == ReserveStatus.Fine
                             select TableBook.ID).Count();

                html = query.ToString();
            }
            return html;
        }
        public string InLib()
        {
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                long sum = 0;
                var query = (from TableBook in EF.ULSTbl_Books
                             join TableCategories in EF.ULSTbl_Categories
                             on TableBook.CategoryId equals TableCategories.ID
                             where TableCategories.Status == true && TableBook.Status == true
                             join TableReserve in EF.ULSTbl_Reserve
                             on TableBook.ID equals TableReserve.BookID
                             where TableReserve.StatusID != ReserveStatus.Delivered && TableBook.Available != null
                             select TableBook).ToList();
                foreach (var item in query)
                {
                    sum += (int)item.Available;
                }
                return sum.ToString();
            }
        }
        #endregion
        #region MostReservedBooks
        protected void LinqDataSourceNews_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    var queryGetMostReservedBooks = (from reservation in EF.ULSTbl_Reserve
                                                     group reservation by reservation.BookID into grouped
                                                     join TableBook in EF.ULSTbl_Books
                                                     on grouped.Key equals TableBook.ID
                                                     join TableCategory in EF.ULSTbl_Categories
                                                     on TableBook.CategoryId equals TableCategory.ID
                                                     join TableUser in EF.ULSTbl_Users
                                                     on TableBook.UserNameAdder equals TableUser.UserName
                                                     orderby grouped.Count() descending
                                                     select new
                                                     {
                                                         UserID = TableUser.ID,
                                                         NameFamily = TableUser.FullName,
                                                         AdderPic = TableUser.PicName,
                                                         BookID = TableBook.ID,
                                                         BookTitle = TableBook.BookTitle,
                                                         CatName = TableCategory.Title,
                                                         Available = TableBook.Available,
                                                         StatusHtml = TableBook.Status == true ? @"<span class='badge bg-label-success me-1'>فعال</span>" : "<span class='badge bg-label-dark me-1'>غیرفعال</span>",
                                                         PicName = TableBook.PicName,
                                                     }).Take(5).ToList();

                    e.Result = queryGetMostReservedBooks;
                }
            }
            catch (Exception ex)
            {
                e.Result = $"<div class='alert alert-danger mb-2' role='alert'>خطا: {ex.Message}</div>";
            }
        }
        #endregion

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            BindData();
        }
    }
}