using Classes;
using System;
using System.Linq;

namespace UniversityLibrarySystem.Manager
{
    public partial class SystemSettings : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "تنظیمات سامانه";
            try
            {
                using (var EF = new ULSDBEntities())
                {
                    var query = (from table in EF.ULSTbl_SiteSetting
                                 where table.ID != 0
                                 select table).FirstOrDefault();

                    if (query != null)
                    {
                        txtSiteName.Text = query.SiteName;
                        txtTelephone.Text = query.Telephone;
                        txtEmail.Text = query.Email;
                        txtAddress.Text = query.Address;
                        txtMapLink.Text = query.MapLink;
                        txtShortAboutUs.Text = query.ShortAboutUs;
                        txtAboutUs.Text = query.AboutUs;
                        txtReserveDay.Text = query.ReserveDay.ToString();
                        txtMaxUserReserve.Text = query.MaxUserReserve.ToString();
                        txtReserveAgain.Text = query.ReserveAgain.ToString();
                        txtLiability.Text = query.Liability.ToString();
                        chkSiteStatus.Checked = query.Status == true;
                        chkres.Checked = query.CanRes == true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = Components.alert("خطا:" + ex.Message, Components.AlertStyle.danger);
                lblMessage.Visible = true;
                return;
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
                        var query = (from table in EF.ULSTbl_SiteSetting
                                     where table.ID != 0
                                     select table).FirstOrDefault();
                        System.Web.HttpRuntime.Cache.Remove("SiteName");
                        System.Web.HttpRuntime.Cache.Remove("SiteStatus");
                        System.Web.HttpRuntime.Cache.Remove("CanRes");
                        System.Web.HttpRuntime.Cache.Remove("ShortAboutUs");
                        System.Web.HttpRuntime.Cache.Remove("GetContactUSPage");
                        if (query != null)
                        {
                            query.SiteName = txtSiteName.Text;
                            query.Telephone = txtTelephone.Text;
                            query.Email = txtEmail.Text;
                            query.Address = txtAddress.Text;
                            query.MapLink = txtMapLink.Text;
                            query.ShortAboutUs = txtShortAboutUs.Text;
                            query.AboutUs = txtAboutUs.Text;
                            query.ReserveDay = int.Parse(txtReserveDay.Text);
                            query.MaxUserReserve = int.Parse(txtMaxUserReserve.Text);
                            query.ReserveAgain = int.Parse(txtReserveAgain.Text); // Ensure the correct value is being assigned
                            query.Liability = long.Parse(txtLiability.Text);
                            query.Status = chkSiteStatus.Checked;
                            query.CanRes = chkres.Checked;

                            EF.SaveChanges();
                        }
                        else
                        {
                            var newSetting = new ULSTbl_SiteSetting
                            {
                                SiteName = txtSiteName.Text,
                                Telephone = txtTelephone.Text,
                                Email = txtEmail.Text,
                                Address = txtAddress.Text,
                                MapLink = txtMapLink.Text,
                                ShortAboutUs = txtShortAboutUs.Text,
                                AboutUs = txtAboutUs.Text,
                                ReserveDay = int.Parse(txtReserveDay.Text),
                                MaxUserReserve = int.Parse(txtMaxUserReserve.Text),
                                ReserveAgain = int.Parse(txtReserveAgain.Text),
                                Liability = long.Parse(txtLiability.Text),
                                Status = chkSiteStatus.Checked
                            };
                            EF.ULSTbl_SiteSetting.Add(newSetting);
                            EF.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = Components.alert("خطا:" + ex.Message, Components.AlertStyle.danger);
                    lblMessage.Visible = true;
                    return;
                }
                lblMessage.Text = Components.alert("تنظیمات شما با موفقیت ذخیره شد", Components.AlertStyle.success);
                lblMessage.Visible = true;
            }
        }
    }
}