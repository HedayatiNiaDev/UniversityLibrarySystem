using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Classes;

namespace UniversityLibrarySystem
{
    public partial class ServerSideChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["bc0b6b2d-1839-462c-837b-fe71ac128e78"] == "68868060-69a6-4bb0-8f3e-590ffa57070b")
            {
                Response.Write("StartProcess:" + DateTime.Now + "<br/>Command:Check");
                try
                {
                    using (var context = new ULSDBEntities())
                    {
                        // بهینه‌سازی: غیرفعال کردن قابلیت‌های غیرضروری برای بهبود عملکرد
                        context.Configuration.LazyLoadingEnabled = false;
                        context.Configuration.ProxyCreationEnabled = false;

                        var currentDate = DateTime.Now;

                        // بهینه‌سازی کوئری: استفاده از Include برای جلوگیری از SELECTهای اضافی
                        var getTempRes = (from tblres in context.ULSTbl_Reserve
                                          where tblres.StatusID == ReserveStatus.TempReservation
                                          && tblres.ReserveEndDate != null
                                          && tblres.ReserveEndDate < currentDate
                                          select new
                                          {
                                              Reservation = tblres,
                                              Book = (from b in context.ULSTbl_Books
                                                      where b.ID == tblres.BookID
                                                      select b).FirstOrDefault()
                                          }).ToList();

                        foreach (var item in getTempRes)
                        {
                            if (item.Book != null)
                            {
                                item.Book.Available += 1;
                            }
                            context.ULSTbl_Reserve.Remove(item.Reservation);
                        }

                        var getRes = context.ULSTbl_Reserve
                            .Where(r => r.StatusID == ReserveStatus.Reservation &&
                                       r.ReserveEndDate != null &&
                                       r.ReserveEndDate < currentDate)
                            .ToList();

                        foreach (var res in getRes)
                        {
                            res.StatusID = ReserveStatus.Fine;
                        }

                        // اضافه کردن تراکنش برای اطمینان از یکپارچگی داده‌ها
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                int changes = context.SaveChanges();
                                transaction.Commit();
                                Response.Write("<br/>Command Status:Done - " + changes + " records affected");
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // بهبود گزارش خطا با نمایش InnerException در صورت وجود
                    string errorMessage = "MessageError:" + ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += "<br/>InnerException:" + ex.InnerException.Message;
                    }
                    Response.Write(errorMessage + "<br/>StackTrace:" + ex.StackTrace);
                }
                Response.Write("<br/>EndProcess:" + DateTime.Now);
            }
            else if (Request.QueryString["bd0b6b2d-1839-462c-837b-fe71ac128e78"] == "67868060-69a6-4bd0-8f3e-590ffa57070b")
            {
                Response.Write("StartProcess:" + DateTime.Now + "<br/>Command:Change Status");
                try
                {
                    using (var context = new ULSDBEntities())
                    {
                        var TableSiteSetting = context.ULSTbl_SiteSetting.FirstOrDefault();

                        // اضافه کردن بررسی null برای جلوگیری از NullReferenceException
                        if (TableSiteSetting != null)
                        {
                            bool oldStatus = TableSiteSetting.Status==true;
                            TableSiteSetting.Status = !oldStatus;
                            HttpRuntime.Cache.Remove("SiteStatus");

                            int changes = context.SaveChanges();
                            Response.Write("<br/>Command Status:Done - Status changed from " +
                                         oldStatus + " to " + TableSiteSetting.Status);
                        }
                        else
                        {
                            Response.Write("<br/>Error: Site settings record not found");
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = "MessageError:" + ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += "<br/>InnerException:" + ex.InnerException.Message;
                    }
                    Response.Write(errorMessage + "<br/>StackTrace:" + ex.StackTrace);
                }
                Response.Write("<br/>EndProcess:" + DateTime.Now);
            }
            else
            {
                Response.StatusCode = 404;
                Response.Write("Invalid command");
                Response.End();
            }
        }


    }
}