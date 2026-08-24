using Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem.Manager
{
    public partial class Manager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiViewMain.SetActiveView(ViewMain);
                btnAdd.Visible = true;

                #region [Load]

                if (Request.QueryString["Value"] != null)
                {
                    btnDelete.Visible = false;
                    MultiViewMain.SetActiveView(ViewFileds);
                    btnAdd.Visible = false;
                }

                if (Request.QueryString["Edit"] != null)
                {
                    btnDelete.Visible = true;
                    MultiViewMain.SetActiveView(ViewFileds);
                    btnAdd.Visible = false;
                }

                if (Request.QueryString["Reset"] != null)
                {
                    MultiViewMain.SetActiveView(ViewMain);
                    btnAdd.Visible = true;
                }
                #endregion
                #region [Edit]
                if (Request.QueryString["Edit"] != null)
                {
                    string HashId = Request.QueryString["Edit"];
                    int id = Hash2ID.HAsh2ID(HashId);
                    string UserId = Membership.GetUser().UserName.ToLower();
                    using (ULSDBEntities EF = new ULSDBEntities())
                    {
                        var qGetCategoryInfo = (from item in EF.ULSTbl_Users
                                                where item.ID == id && item.RoleID == UserRole.Admin && item.UserName != "0"
                                                select item).FirstOrDefault();
                        if (qGetCategoryInfo != null)
                        {
                            if (qGetCategoryInfo.UserName == UserId)
                            {
                                Response.Redirect("../Manager/Profile");
                                return;
                            }
                            txtEmail.Value = qGetCategoryInfo.Email;
                            txtFullName.Value = qGetCategoryInfo.FullName;
                            txtMobile.Value = qGetCategoryInfo.Mobile;
                            txtPassword.Disabled = true;
                            RequiredFieldValidator4.Enabled = false;
                            txtUserName.Value = qGetCategoryInfo.UserName;
                            txtUserName.Disabled = true;
                            lblIsOnline.Text = Membership.GetUser(qGetCategoryInfo.UserName).IsOnline ? "کاربر مورد نظر در حال استفاده از سامانه است" : "آخرین تاریخ فعالیت کاربر:" + Membership.GetUser(qGetCategoryInfo.UserName).LastActivityDate;
                            lblIsOnline.CssClass += Membership.GetUser(qGetCategoryInfo.UserName).IsOnline ? " bg-label-primary" : " bg-label-dark";
                            if (qGetCategoryInfo.StatusID == UserStatus.Active)
                                chkStatus.Checked = true;
                            else
                                chkStatus.Checked = false;

                            if (qGetCategoryInfo.PicName != "no-photo.png")
                                ImageNopic.ImageUrl = "../img/users/" + qGetCategoryInfo.PicName;
                            else
                                ImageNopic.ImageUrl = "../img/users/no-photo.png";
                        }
                        else
                        {
                            MultiViewMain.SetActiveView(ViewMain);
                            btnAdd.Visible = true;
                            LiteralMessage.Text = Components.alert("کاربر مورد نظر یافت نشد", Components.AlertStyle.danger);
                        }


                    }
                }
                #endregion

                #region [Reset]
                if (Request.QueryString["Reset"] != null)
                {
                    string HashId = Request.QueryString["Reset"];
                    int id = Hash2ID.HAsh2ID(HashId);

                    using (ULSDBEntities EF = new ULSDBEntities())
                    {
                        var qGetDeleteId = (from item in EF.ULSTbl_Users
                                            where item.ID == id
                                            select item).FirstOrDefault();
                        if (qGetDeleteId != null)
                        {
                            var querya = (from item in EF.aspnet_Users
                                          join ite in EF.aspnet_Membership
                                          on item.UserId equals ite.UserId
                                          where item.UserName == qGetDeleteId.UserName
                                          select ite).FirstOrDefault();

                            querya.IsApproved = true;
                            querya.IsLockedOut = false;
                            querya.FailedPasswordAttemptCount = 0;
                            querya.FailedPasswordAnswerAttemptCount = 0;

                            EF.SaveChanges();

                            MultiViewMain.SetActiveView(ViewMain);
                            LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                        }
                        else
                        {
                            MultiViewMain.SetActiveView(ViewMain);
                            btnAdd.Visible = true;
                            LiteralMessage.Text = Components.alert("کاربر مورد نظر یافت نشد", Components.AlertStyle.danger);
                        }
                    }
                }
                #endregion

                PanelSearch.Visible = btnAdd.Visible;
            }
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

        #region [Status]
        public string Status(object id)
        {
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                int ID = Convert.ToInt32(id);
                string text = "";

                var qGetStatus = (from item in EF.ULSTbl_Users
                                  where item.ID == ID
                                  select item).FirstOrDefault();

                if (qGetStatus.StatusID == UserStatus.Active)
                {
                    text += @"<td><span class='badge bg-label-success me-1'>فعال</span></td>";
                }
                else
                {
                    text += @"<td><span class='badge bg-label-dark me-1'>غیرفعال</span></td>";
                }

                return text;
            }
        }
        #endregion

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
                string name = Request.QueryString["txtName"] != null ? Request.QueryString["txtName"].ToString() : null;

                var qGetInfo = (from item in EF.ULSTbl_Users
                                join ite in EF.ULSTbl_Users
                                on item.UserNameAdder equals ite.UserName
                                where item.RoleID == UserRole.Admin && (String.IsNullOrEmpty(name) || item.FullName.Contains(name))
                                orderby item.ID descending
                                select new
                                {
                                    UserID = ite.ID,
                                    item.ID,
                                    item.FullName,
                                    item.Mobile,
                                    Pic = item.PicName,
                                    AdderPic = ite.PicName,
                                    NameFamily = ite.FullName,
                                    DateInsert = item.DateInsert
                                }).ToList();

                if (qGetInfo == null)
                    e.Result = "<div class='alert alert-danger mb-2' role='alert'>اطلاعاتی درج نشده است</div>";
                else
                    e.Result = qGetInfo;
            }
        }
        #endregion

        #region [Button]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string UserId = Membership.GetUser().UserName.ToLower();

                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    #region [NewsSave]
                    if (Request.QueryString["Value"] != null)
                    {
                        string PicName = "no-photo.png";

                        var ChekRepid = (from item in EF.ULSTbl_Users
                                         where item.UserName.Trim() == txtUserName.Value.Trim()
                                         select item).FirstOrDefault();//جستجو برای اینکه قبلا همچنین نامی را این کاربر ثبت نکرده باشد

                        if (ChekRepid == null)//نکرده است
                        {
                            if (fileUploadPic.HasFile)//آیا تصویری را انتخاب نموده؟
                            {
                                string extension = Path.GetExtension(fileUploadPic.FileName);
                                PicName = Guid.NewGuid() + extension;
                                fileUploadPic.SaveAs(Server.MapPath("~/img/users") + "//" + PicName);
                            }
                            //بر روی وصعیت فعال یا غیر فعال کلیک کرده
                            bool Status = true;

                            if (chkStatus.Checked)
                                Status = true;
                            else
                                Status = false;

                            Random rand = new Random();

                            int virefy = rand.Next(100000, 999999);

                            //----------------
                            //درج و مقداردهی جدول با کنترل ها
                            ULSTbl_Users TG = new ULSTbl_Users()
                            {
                                UserNameAdder = UserId,
                                DateInsert = DateTime.Now,
                                PicName = PicName,
                                StatusID = Status ? UserStatus.Active : UserStatus.Deactive,
                                Email = txtEmail.Value,
                                FullName = txtFullName.Value,
                                Mobile = txtMobile.Value,
                                RoleID = UserRole.Admin,
                                UserName = txtUserName.Value,
                            };

                            EF.ULSTbl_Users.Add(TG);
                            EF.SaveChanges();

                            #region [CreateUser]
                            MembershipCreateStatus Us;
                            MembershipUser user = Membership.CreateUser(TG.UserName, txtPassword.Value, TG.Email, "What is nephie name?", "Arsham", true, out Us);
                            Roles.AddUserToRole(user.UserName, "Admin");
                            #endregion

                            //--------
                            //به ترتیب پاک سازی - همگام سازی گرید ، نمایش گرید و نمایش پیغام درج موفق
                            Clean();
                            MultiViewMain.SetActiveView(ViewMain);
                            LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                            btnAdd.Visible = true;
                        }
                        else//کرده است
                        {
                            //نمایش پیغام تکراری بودن-نمایش ویو مقادیر
                            LiteralMessage.Text = Components.alert("عملیات با شکست مواجعه شد", Components.AlertStyle.danger);
                            MultiViewMain.SetActiveView(ViewMain);
                            btnAdd.Visible = true;
                        }
                    }
                    #endregion

                    #region [Edit]
                    if (Request.QueryString["Edit"] != null)
                    {
                        string HashId = Request.QueryString["Edit"];
                        int RowId = Hash2ID.HAsh2ID(HashId);

                        var Searchquery = (from item in EF.ULSTbl_Users
                                           where item.ID == RowId
                                           select item).FirstOrDefault();//پیدا کردن سط مورد تقاضای کاربر از جدول
                        if (Searchquery != null)//پیدا شد
                        {
                            //مجددا چک کن که شخص با نام کاربری خود مقدار تکرای وارد نکند
                            var Cheekquery = (from item in EF.ULSTbl_Users
                                              where item.UserName == txtUserName.Value.Trim()
                                              && item.ID != RowId//به جز همین سطر-چون این سطر ممکن است تغییر نکند و همین سط را به عنوان تکرار انتخاب نماید
                                              select item).FirstOrDefault();

                            if (Cheekquery == null)//تکرای نبود
                            {
                                //مقادیر را از کنترل ها به فیلد های مرتبط اضافه کن
                                Searchquery.Email = txtEmail.Value;
                                Searchquery.FullName = txtFullName.Value;
                                Searchquery.Mobile = txtMobile.Value;

                                if (chkStatus.Checked)
                                    Searchquery.StatusID = UserStatus.Active;
                                else
                                {
                                    Searchquery.StatusID = UserStatus.Deactive;
                                }
                                if (fileUploadPic.HasFile)//اگر تصویر جدیدی انتخاب نکرد پس تمایل به به روز رسانی تصویر ندارد
                                {
                                    //اگر کرد پس قبلی را پاک کن
                                    //جدید را بزار
                                    if (Searchquery.PicName != "no-photo.jpg")
                                    {
                                        try
                                        {
                                            string DelPath = Server.MapPath("../img/users") + "//" + Searchquery.PicName;
                                            System.IO.File.Delete(DelPath);
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    string extension = Path.GetExtension(fileUploadPic.FileName);
                                    string PicName = Guid.NewGuid() + extension;
                                    fileUploadPic.SaveAs(Server.MapPath("~/img/users") + "//" + PicName);
                                    Searchquery.PicName = PicName;
                                }

                                EF.SaveChanges();
                                HttpRuntime.Cache.Remove($"PasswordChangedTime_{Searchquery.UserName}");
                                //------------
                                //به ترتیب پاک سازی - همگام سازی گرید ، نمایش گرید و نمایش پیغام درج موفق
                                Clean();
                                MultiViewMain.SetActiveView(ViewMain);
                                LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                                btnAdd.Visible = true;
                            }
                            else//تکراری بود
                            {
                                //نمایش پیغام تکراری بودن-نمایش ویو مقادیر
                                MultiViewMain.SetActiveView(ViewMain);
                                LiteralMessage.Text = Components.alert("عملیات با شکست مواجعه شد", Components.AlertStyle.danger);
                                btnAdd.Visible = true;
                                Clean();
                            }
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region[Clean]
        public void Clean()
        {
            txtFullName.Value = "";
            txtUserName.Value = "";
            txtPassword.Value = "";
            txtMobile.Value = "";
            txtEmail.Value = "";
            chkStatus.Checked = true;
            ImageNopic.ImageUrl = ResolveUrl("../img/users/no-photo.png");
        }
        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string UserId = Membership.GetUser().UserName.ToLower();

                using (ULSDBEntities EF = new ULSDBEntities())
                {

                    #region [Delete]
                    if (Request.QueryString["Edit"] != null)
                    {
                        string HashId = Request.QueryString["Edit"];
                        int RowId = Hash2ID.HAsh2ID(HashId);

                        var Searchquery = (from item in EF.ULSTbl_Users
                                           where item.ID == RowId
                                           select item).FirstOrDefault();//پیدا کردن سط مورد تقاضای کاربر از جدول
                        if (Searchquery != null)//پیدا شد
                        {
                            if (Searchquery.PicName != "no-photo.png" && Searchquery.PicName != "user.png" && Searchquery.PicName != "zero.png")
                            {
                                try
                                {
                                    string DelPath = Server.MapPath("../img/users") + "//" + Searchquery.PicName;
                                    System.IO.File.Delete(DelPath);
                                }
                                catch
                                {

                                }
                            }
                            var TablePermission = (from permission in EF.PAPTbl_PermissionUsers
                                                   where permission.UserName == Searchquery.UserName
                                                   select permission).ToList();
                            var TableCat = (from cat in EF.ULSTbl_Categories
                                            where cat.UserNameAdder == Searchquery.UserName
                                            select cat).ToList();
                            foreach (var item in TableCat)
                            {
                                item.UserNameAdder = "0";
                            }
                            var TableBooks = (from book in EF.ULSTbl_Books
                                              where book.UserNameAdder == Searchquery.UserName
                                              select book).ToList();
                            foreach (var item in TableBooks)
                            {
                                item.UserNameAdder = "0";
                            }
                            Membership.DeleteUser(Searchquery.UserName, true);
                            EF.PAPTbl_PermissionUsers.RemoveRange(TablePermission);
                            EF.PAPMyDevice.RemoveRange((from MyDevice in EF.PAPMyDevice where MyDevice.Username == Searchquery.UserName select MyDevice).ToList());
                            EF.ULSTbl_Users.Remove(Searchquery);
                            EF.SaveChanges();
                            Clean();
                            MultiViewMain.SetActiveView(ViewMain);
                            LiteralMessage.Text = Components.alert("حساب کاربری مورد نظر با موفقیت حذف شد", Components.AlertStyle.success);
                            btnAdd.Visible = true;
                        }
                        else
                        {
                            MultiViewMain.SetActiveView(ViewMain);
                            LiteralMessage.Text = Components.alert("عملیات با شکست مواجعه شد", Components.AlertStyle.danger);
                            btnAdd.Visible = true;
                            Clean();
                        }
                    }
                    #endregion
                }
            }
        }
    }
}