using Classes;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace UniversityLibrarySystem.Manager
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            rfvInvalidCode.Visible = false;
            lblInvalidCurrentPassword.Visible = false;

            if (Membership.GetUser(Membership.GetUser().UserName).IsLockedOut)
            {
                LiteralMessage.Text = Components.alert("خطا:" + "حساب کاربری شما قفل شده است، لطفاً با مدیر سیستم تماس بگیرید.", Components.AlertStyle.danger);
                LiteralMessage.Visible = true;
                string cacheKey = $"PasswordChangedTime_{Membership.GetUser().UserName}";
                if (HttpRuntime.Cache[cacheKey] != null)
                {
                    HttpRuntime.Cache.Remove(cacheKey);
                }
                FormsAuthentication.SignOut();
                return;
            }
            if (Page.IsValid)
            {
                if (BotCaptcha.Validate(CaptchaCodeTextBox.Text))
                {
                    try
                    {
                        string UserId = Membership.GetUser().UserName.ToLower();
                        string pass = CurrentPassword.Value;
                        if (Membership.ValidateUser(UserId, pass))
                        {
                            MembershipUser usr = Membership.GetUser(UserId);
                            string resetPwd = usr.ResetPassword();
                            usr.ChangePassword(resetPwd, txtPassword.Value);
                            try
                            {
                                using (var connection = new ULSDBEntities())
                                {
                                    // یافتن کاربر مورد نظر
                                    var user = connection.ULSTbl_Users
                                        .FirstOrDefault(u => u.UserName == UserId);

                                    if (user != null)
                                    {
                                        // به‌روزرسانی فیلد chpass با تاریخ و زمان فعلی
                                        user.chpass = DateTime.Now; // یا DateTime.Now اگر زمان محلی مد نظر است

                                        // ذخیره تغییرات در دیتابیس
                                        connection.SaveChanges();
                                    }
                                }
                            }
                            catch { }
                            LiteralMessage.Text = Components.alert("رمز عبور شما با موفقیت بروزرسانی شد، لطفاً برای ادامه، مجدداً وارد حساب کاربری خود شوید.", Components.AlertStyle.success);
                            string cacheKey = $"PasswordChangedTime_{Membership.GetUser().UserName}";

                            if (HttpRuntime.Cache[cacheKey] != null)
                            {
                                HttpRuntime.Cache.Remove(cacheKey);
                            }
                            FormsAuthentication.SignOut();
                        }
                        else
                        {
                            lblInvalidCurrentPassword.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LiteralMessage.Text = Components.alert("خطا:" + ex.Message, Components.AlertStyle.danger);
                    }
                }
                else
                {
                    rfvInvalidCode.Visible = true;
                }
            }
        }
    }
}