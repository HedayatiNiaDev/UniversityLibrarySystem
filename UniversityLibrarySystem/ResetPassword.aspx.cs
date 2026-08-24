using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace UniversityLibrarySystem
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Dash");
            }
            if (Session["UserNameForResetPassword"] == null)
            {
                Response.Redirect("ForgetPassword");
            }

        }

        protected void ResetPasswordButton_Click(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            if (Page.IsValid)
            {
                if (Session["UserNameForResetPassword"] != null)
                {
                    try
                    {
                        using (var uls = new ULSDBEntities())
                        {
                            string username = Session["UserNameForResetPassword"].ToString();

                            if (username != null || username != "")
                            {
                                var queryGetUser = (from User in uls.ULSTbl_Users
                                                    where User.UserName == username
                                                    select new
                                                    {
                                                        User.UserName,
                                                        User.Mobile
                                                    }).FirstOrDefault();
                                if (queryGetUser != null)
                                {
                                    string Password = NewPassword.Text;
                                    if (Password != null || Password != "")
                                    {
                                        MembershipUser userReset = Membership.GetUser(queryGetUser.UserName);
                                        string resetPwd = userReset.ResetPassword();
                                        userReset.ChangePassword(resetPwd, Password);
                                        try
                                        {
                                            using (var connection = new ULSDBEntities())
                                            {
                                                // یافتن کاربر مورد نظر
                                                var user = connection.ULSTbl_Users
                                                    .FirstOrDefault(u => u.UserName == username);

                                                if (user != null)
                                                {
                                                    // به‌روزرسانی فیلد chpass با تاریخ و زمان فعلی
                                                    user.chpass = DateTime.Now; // یا DateTime.Now اگر زمان محلی مد نظر است
                                                    string cacheKey = $"PasswordChangedTime_{username}";
                                                    if (HttpRuntime.Cache[cacheKey] != null)
                                                    {
                                                        HttpRuntime.Cache.Remove(cacheKey);
                                                    }
                                                    Response.Redirect("ForgetPassword?reset=1");
                                                    // ذخیره تغییرات در دیتابیس
                                                    connection.SaveChanges();
                                                }
                                            }
                                        }
                                        catch{}
                                    }
                                }
                                else
                                {
                                    ErrorMessage.Text = "مشکلی پیش آمده است";
                                    ErrorMessage.Visible = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = "خطا:" + ex.Message;
                        ErrorMessage.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("ForgetPassword");
                }
            }
        }
    }
}