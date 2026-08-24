using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace UniversityLibrarySystem
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteConfig.siteStatus())
            {
                Response.StatusCode = 503;
                Response.End();
            }
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Dash");
            }
            Session.Clear();

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            rfvInvalidCode.Visible = false;
            if (Page.IsValid)
            {
                if (BotCaptcha.Validate(CaptchaCodeTextBox.Text))
                {
                    string fullName = FullName.Text;
                    string username = Username.Text.Trim();
                    string mobile = Mobile.Text.Trim();
                    string password = Password.Text;
                    string email = Email.Text;

                    try
                    {
                        using (var EF = new ULSDBEntities())
                        {
                            ErrorMessage.Visible = false;
                            var qGetExist = (from TableUser in EF.ULSTbl_Users
                                             where TableUser.UserName == username
                                             select TableUser).FirstOrDefault();
                            if (qGetExist == null)
                            {

                                Random random = new Random();
                                string verify = random.Next(10000000, 99999999).ToString();

                                var TableUser = new ULSTbl_Users
                                {
                                    UserName = username,
                                    UserNameAdder = username,
                                    Liability = 0,
                                    RoleID = UserRole.User,
                                    DateInsert = DateTime.Now,
                                    StatusID = UserStatus.Active,
                                    Mobile = mobile,
                                    FullName = fullName,
                                    Email = email,
                                    PicName = "user.png",
                                };
                                EF.ULSTbl_Users.Add(TableUser);
                                EF.SaveChanges();
                                MembershipCreateStatus UserCreator;
                                MembershipUser user = Membership.CreateUser(username, password, "User@User.com", "What is nephie name?", "Arsham", true, out UserCreator);
                                Roles.AddUserToRole(user.UserName, "User");
                                FullName.Text = "";
                                Username.Text = "";
                                Mobile.Text = "";
                                Password.Text = "";
                                Email.Text = "";
                                SuccessMessage.Visible = true;
                            }
                            else
                            {
                                ErrorMessage.Text = "این نام کاربری قبلا ثبت نام کرده است در صورتی که کلمه عبور خود را فراموش کرده اید اقدام به بازیابی آن کنید";
                                ErrorMessage.Visible = true;
                            }
                        }
                    }
                    catch
                    {
                        ErrorMessage.Text = "ثبت نام شما با موفقیت انجام نشده است!";
                        ErrorMessage.Visible = true;
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