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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteConfig.siteStatus())
            {
                Response.StatusCode = 503;
                Response.End();
            }
            Page.Title = SiteConfig.mixTitle("پنل داشنجو");
            try
            {
                if (Membership.GetUser() == null)
                {
                    Response.Redirect("/");
                }
            }
            catch { }
            try
            {
                if (RouteData.Values["Page"] != null)
                {
                    UserControl ctr = Page.LoadControl(RouteData.Values["Page"].ToString() + ".ascx") as UserControl;
                    ctr.ID = RouteData.Values["Page"].ToString();
                    PanelLoad.Controls.Add(ctr);
                }
                else
                {
                    Response.Redirect("./Books", false);
                    Context.ApplicationInstance.CompleteRequest(); // end response
                }
            }
            catch (HttpCompileException)
            {
                try
                {
                    UserControl ctr = Page.LoadControl("../PAPAssets/Pages/CompileError.ascx") as UserControl;
                    ctr.ID = RouteData.Values["Page"].ToString();
                    PanelLoad.Controls.Add(ctr);
                }
                catch
                { }
            }
            catch (HttpException)
            {
                try
                {
                    UserControl ctr = Page.LoadControl("../PAPAssets/Pages/NotFoundPage.ascx") as UserControl;
                    ctr.ID = RouteData.Values["Page"].ToString();
                    PanelLoad.Controls.Add(ctr);
                }
                catch
                { }
            }
            catch
            {

            }
        }

        public string Date() => DateTime.Now.ToLongDateString();

        public string Profile()
        {
            try
            {
                using (var EF = new ULSDBEntities())
                {
                    string UserId = Membership.GetUser().UserName.ToLower();

                    var qGetUser = (from item in EF.ULSTbl_Users
                                    where item.UserName == UserId
                                    select item).FirstOrDefault();

                    string text = "";

                    text += @"<li class='nav-item navbar-dropdown dropdown-user dropdown'>
                                    <a class='nav-link dropdown-toggle hide-arrow' href='javascript:void(0);' data-bs-toggle='dropdown'>
                                        <div id='user-avatar' class='avatar avatar-online'>
                                            <img src='../img/users/" + qGetUser.PicName + @"' alt class='rounded-circle'>
                                        </div>
                                    </a>
                                    <ul class='dropdown-menu dropdown-menu-end'>
                                        <li>
                                            <a class='dropdown-item' href='./Profile'>
                                                <div class='d-flex'>
                                                    <div class='flex-shrink-0 me-3'>
                                                        <div id='user-avatar-menu' class='avatar avatar-online mt-1'>
                                                            <img src='../img/users/" + qGetUser.PicName + @"' alt class='rounded-circle'>
                                                        </div>
                                                    </div>
                                                    <div class='flex-grow-1'>
                                                        <span class='fw-semibold d-block'>" + qGetUser.FullName + @"</span>";

                    text += @"<small> دانشجو محترم </small>";
                    text += @"</div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <div class='dropdown-divider'></div>
                                        </li>
                                        <li>
                                            <a class='dropdown-item' href='../'>
                                                <i class='bx bx-home me-2'></i>
                                                <span class='align-middle'>خانه</span>
                                            </a>
                                        </li>
                                        <li>
                                            <div class='dropdown-divider'></div>
                                        </li>
                                        <li>
                                            <a class='dropdown-item' href='./Profile'>
                                                <i class='bx bx-user me-2'></i>
                                                <span class='align-middle'>پروفایل من</span>
                                            </a>
                                        </li>
                                        <li>
                                            <div class='dropdown-divider'></div>
                                        </li>
                                        <li>
                                            <a class='dropdown-item' href='./LogOut'>
                                                <i class='bx bx-power-off me-2'></i>
                                                <span class='align-middle'>خروج</span>
                                            </a>
                                        </li>
                                    </ul>
                                </li>";

                    return text;

                }
            }
            catch
            {
                return "";
            }
        }

        public static string checkFinance()
        {
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                string username = Membership.GetUser().UserName;
                var query = (from res in EF.ULSTbl_Reserve
                             where res.UserName == username && res.StatusID == ReserveStatus.Fine
                             select res).Count();
                if (query > 0)
                {
                    return @"<div class=""alert alert-danger"" role=""alert"">
  شما در حال جریمه هستید! لطفاً اقدامات لازم را جهت تسویه حساب انجام دهید. <a id=""btnGoFine"" class=""btn btn-danger"" href=""./Books?Mode=3"">مشاهده جریمه ها</a>
</div>";
                }
            }
            return "";
        }

        //public static bool CheckUserPermision(string UserName, string PageEnglishName)
        //{
        //    using (var EF = new ULSDBEntities())
        //    {
        //        var CheckPage = (from PermissionPages in EF.PAPTbl_PermissionPages
        //                         where PermissionPages.PageName.ToLower() == PageEnglishName.ToLower()
        //                         select new { PermissionPages.ID }).FirstOrDefault();
        //        if (CheckPage == null)
        //            return true;

        //        var Check = (from PermissionUsers in EF.PAPTbl_PermissionUsers
        //                     join PermissionPages in EF.PAPTbl_PermissionPages
        //                     on PermissionUsers.PageId equals PermissionPages.ID
        //                     where PermissionUsers.UserName.ToLower() == UserName.ToLower()
        //                     && PermissionPages.PageName.ToLower() == PageEnglishName.ToLower()
        //                     && PermissionPages.IsPermission == true
        //                     select PermissionUsers).FirstOrDefault();

        //        if (Check != null)
        //            return true;

        //        return false;
        //    }
        //}
    }
}