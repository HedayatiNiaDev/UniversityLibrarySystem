using Classes;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;


namespace UniversityLibrarySystem
{

    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Dash");
            }
            Session.Clear();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            rfvInvalidCode.Visible = false;
            if (Page.IsValid)
            {
                if (BotCaptcha.Validate(CaptchaCodeTextBox.Text))
                {
                    string username = UserName.Text;
                    string password = Password.Text;
                    if (Membership.ValidateUser(username, password))
                    {

                        using (var libraryDBEntities = new ULSDBEntities())
                        {
                            var queryGetUser = (from TableUser in libraryDBEntities.ULSTbl_Users
                                                where TableUser.UserName == username
                                                select new
                                                {
                                                    TableUser.StatusID,
                                                    TableUser.Mobile
                                                }).FirstOrDefault();
                            if (queryGetUser != null)
                            {
                                if (queryGetUser.StatusID == UserStatus.Active)
                                {
                                    FormsAuthentication.SetAuthCookie(username, true);
                                    PAPMyDevice device = new PAPMyDevice
                                    {
                                        Username = username,
                                        Name = "<h4 class='m-0 p-0'>" + Classes.SiteConfig.GetOperatingSystem(Request.UserAgent) + "</h4>"+Request.Browser.Browser + " (v" + Request.Browser.Version+") IP:"+ Request.UserHostAddress,
                                        LogCode = null,
                                        DateTime = DateTime.Now
                                    };
                                    libraryDBEntities.PAPMyDevice.Add(device);
                                    libraryDBEntities.SaveChanges();

                                        // مدیریت لاگ‌های قدیمی‌تر
                                        var userDevices = libraryDBEntities.PAPMyDevice.Where(d => d.Username == username)
                                                                      .OrderByDescending(d => d.Id)
                                                                      .Skip(3)
                                                                      .ToList();
                                        if (userDevices.Any())
                                        {
                                        libraryDBEntities.PAPMyDevice.RemoveRange(userDevices);
                                        libraryDBEntities.SaveChanges();
                                        }
                                    
                                    Response.Redirect("~/Dash");
                                    return;
                                }
                                else if (queryGetUser.StatusID == UserStatus.Deactive)
                                {
                                    ErrorMessage.Text = "حساب کاربری شما غیر فعال می باشد، لطفاً با مدیر سیستم تماس بگیرید.";
                                    ErrorMessage.Visible = true;
                                }
                                else
                                {
                                    ErrorMessage.Text = "حساب کاربری شما تأیید نشده است، لطفاً با مدیر سیستم تماس بگیرید.";
                                    ErrorMessage.Visible = true;
                                    return;
                                }
                            }
                            else
                            {
                                ErrorMessage.Text = "نام کاربری که وارد کردید یافت نشد.";
                                ErrorMessage.Visible = true;
                                return;
                            }
                        }
                    }
                    else
                    {
                        MembershipUser user = Membership.GetUser(username);
                        if (user != null)
                        {
                            if (user.IsLockedOut)
                            {
                                ErrorMessage.Text = "حساب کاربری شما قفل شده است، لطفاً با مدیر سیستم تماس بگیرید.";
                                ErrorMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                ErrorMessage.Text = "نام کاربری یا رمز عبور نادرست است";
                                ErrorMessage.Visible = true;
                                return;
                            }
                        }
                        else
                        {
                            ErrorMessage.Text = "نام کاربری که وارد کردید یافت نشد.";
                            ErrorMessage.Visible = true;
                            return;
                        }
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