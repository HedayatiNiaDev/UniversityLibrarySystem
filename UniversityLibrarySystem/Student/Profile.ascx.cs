using Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem.Student
{
    public partial class Profile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (var EF = new ULSDBEntities())
                {
                    string UserId = Membership.GetUser().UserName.ToLower();

                    var qGetUser = (from item in EF.ULSTbl_Users
                                    where item.UserName == UserId
                                    select item).FirstOrDefault();

                    if (qGetUser != null)
                    {
                        txtFullName.Value = qGetUser.FullName;
                        txtMobile.Value = qGetUser.Mobile;
                        txtEmail.Value = qGetUser.Email;
                        ImageNopic.ImageUrl = "../img/users/" + qGetUser.PicName;
                        btnResetProfile.Visible = qGetUser.PicName != "no-photo.png" && qGetUser.PicName != "user.png";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = Components.alert("خطا:" + ex.Message, Components.AlertStyle.danger);
                lblMessage.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    using (var EF = new ULSDBEntities())
                    {
                        string UserId = Membership.GetUser().UserName.ToLower();

                        var qGetUser = (from item in EF.ULSTbl_Users
                                        where item.UserName == UserId
                                        select item).FirstOrDefault();
                        string oldPhoto = qGetUser.PicName;

                        if (fileUploadPic.HasFile)//آیا تصویری را انتخاب نموده؟
                        {
                            string extension = Path.GetExtension(fileUploadPic.FileName);
                            string PicName = Guid.NewGuid().ToString() + DateTime.Now.ToString("-yyyyM-MddHH-mmss") + extension;
                            fileUploadPic.SaveAs(Server.MapPath("../img/users") + "//" + PicName);
                            if (qGetUser.PicName != "no-photo.png" && qGetUser.PicName != "user.png")
                            {
                                try
                                {
                                    File.Delete(Server.MapPath("../img/users") + "//" + oldPhoto);
                                }
                                catch
                                {

                                    throw;
                                }
                            }
                            btnResetProfile.Visible = true;
                            qGetUser.PicName = PicName;
                        }

                        qGetUser.FullName = txtFullName.Value;
                        qGetUser.Mobile = txtMobile.Value;
                        qGetUser.Email = txtEmail.Value;

                        EF.SaveChanges();
                        if (qGetUser != null)
                        {
                            txtFullName.Value = qGetUser.FullName;
                            txtMobile.Value = qGetUser.Mobile;
                            txtEmail.Value = qGetUser.Email;
                            ImageNopic.ImageUrl = "../img/users/" + qGetUser.PicName;
                        }
                        lblMessage.Text = Components.alert("پروفایل شما با موفقیت بروزرسانی شد", Components.AlertStyle.success);
                        lblMessage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = Components.alert("خطا:" + ex.Message, Components.AlertStyle.danger);
                    lblMessage.Visible = true;
                }
            }
        }

        protected void btnResetProfile_Click(object sender, EventArgs e)
        {
            try
            {
                using (var EF = new ULSDBEntities())
                {
                    string UserId = Membership.GetUser().UserName.ToLower();

                    var qGetUser = (from item in EF.ULSTbl_Users
                                    where item.UserName == UserId
                                    select item).FirstOrDefault();
                    if (qGetUser.PicName != "no-photo.png" && qGetUser.PicName != "user.png")
                    {
                        File.Delete(Server.MapPath("../img/users") + "//" + qGetUser.PicName);
                        qGetUser.PicName = "no-photo.png";
                        EF.SaveChanges();
                    }
                    if (qGetUser != null)
                    {
                        ImageNopic.ImageUrl = "../img/users/" + qGetUser.PicName;
                    }
                    lblMessage.Text = Components.alert("تصویر پروفایل شما با موفقیت بازنشانی شد", Components.AlertStyle.success);
                    lblMessage.Visible = true;
                    btnResetProfile.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = Components.alert("خطا:" + ex.Message, Components.AlertStyle.danger);
                lblMessage.Visible = true;
            }
        }
    }
}