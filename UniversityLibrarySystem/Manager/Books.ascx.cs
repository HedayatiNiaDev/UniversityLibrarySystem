using Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem.Manager
{
    public partial class Books : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            MultiViewMain.SetActiveView(ViewMain);
            btnAdd.Visible = true;
            PanelSearch.Visible = true;
            txtIsbn.Disabled = false;

            #region [Load]

            if (Request.QueryString["Value"] != null)
            {
                using (var EF = new ULSDBEntities())
                {
                    var query = (from cat in EF.ULSTbl_Categories
                                 select cat).FirstOrDefault();
                    if (query != null)
                    {
                        MultiViewMain.SetActiveView(ViewFileds);
                        btnAdd.Visible = false;
                        PanelSearch.Visible = false;
                    }
                    else
                    {
                        LiteralMessage.Text = Components.alert("دسته بندی فعالی وجود ندارد", Components.AlertStyle.danger);
                    }
                }

                //int productid = BaseClass.Coder.ProductCoder();

                //Session["ProductId"] = null;

                //Session["ProductId"] = productid;
            }

            if (Request.QueryString["Edit"] != null)
            {
                MultiViewMain.SetActiveView(ViewFileds);
                txtIsbn.Disabled = true;
                btnAdd.Visible = false;
                PanelSearch.Visible = false;
            }

            if (Request.QueryString["Delete"] != null)
            {
                MultiViewMain.SetActiveView(ViewMain);
                btnAdd.Visible = true;
                PanelSearch.Visible = true;
            }
            #endregion

            #region [Edit]
            if (Request.QueryString["Edit"] != null)
            {
                string HashId = Request.QueryString["Edit"];
                int id = Hash2ID.HAsh2ID(HashId);

                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    var qGetCategoryInfo = (from item in EF.ULSTbl_Books
                                            from ite in EF.ULSTbl_Categories
                                            where item.ID == id && ite.ID == item.CategoryId
                                            select new
                                            {
                                                item.AuthorName,
                                                item.CategoryId,
                                                item.TranslatorName,
                                                item.Count,
                                                item.PublisherName,
                                                item.BookTitle,
                                                item.ISBN,
                                                item.IsSpecial,
                                                item.PicName,
                                                item.Status,
                                                CatStatus = ite.Status,
                                                CatName = ite.Title
                                            }).FirstOrDefault();

                    if (qGetCategoryInfo != null)
                    {
                        if (qGetCategoryInfo.CatStatus != true)
                            LiteralMessage.Text = Components.alert("دسته بندی " + qGetCategoryInfo.CatName + " غیر فعال می باشد", Components.AlertStyle.warning);
                        drpCategory.SelectedValue = qGetCategoryInfo.CategoryId.ToString();

                        txtAuthor.Value = qGetCategoryInfo.AuthorName;
                        txtTedad.Value = qGetCategoryInfo.Count.ToString();
                        txtTitle.Value = qGetCategoryInfo.BookTitle;
                        txtTranslatorName.Value = qGetCategoryInfo.TranslatorName;
                        txtIsbn.Value = qGetCategoryInfo.ISBN;
                        txtPublisher.Value = qGetCategoryInfo.PublisherName;


                        if (qGetCategoryInfo.Status == true)
                            chkStatus.Checked = true;
                        else
                            chkStatus.Checked = false;

                        if (qGetCategoryInfo.IsSpecial == true)
                            chkSpecial.Checked = true;
                        else
                            chkSpecial.Checked = false;

                        if (qGetCategoryInfo.PicName != "no-photo.png")
                            ImageNopic.ImageUrl = "../img/books/" + qGetCategoryInfo.PicName;
                        else
                            ImageNopic.ImageUrl = "../img/Error/no-photo.png";

                    }
                    else
                    {
                        LiteralMessage.Text = Components.alert("کتاب مورد نظر یافت نشد", Components.AlertStyle.danger);
                        MultiViewMain.SetActiveView(ViewMain);
                    }
                }
            }
            #endregion

            #region [Delete]
            if (Request.QueryString["Delete"] != null)
            {
                string HashId = Request.QueryString["Delete"];
                int id = Hash2ID.HAsh2ID(HashId);

                using (ULSDBEntities EF = new ULSDBEntities())
                {
                    var qGetDeleteId = (from item in EF.ULSTbl_Books
                                        where item.ID == id
                                        select item).FirstOrDefault();
                    var CanNotDelete = (from item in EF.ULSTbl_Reserve
                                        where item.BookID == id && (item.StatusID == ReserveStatus.Reservation || item.StatusID == ReserveStatus.Fine)
                                        select item).FirstOrDefault();
                    if (CanNotDelete == null)
                    {
                        var qGetDeleteResID = (from item in EF.ULSTbl_Reserve
                                               where item.BookID == id
                                               select item).ToList();
                        if (qGetDeleteId != null)
                        {
                            if (qGetDeleteId.PicName != null && qGetDeleteId.PicName != "no-photo.png")
                            {
                                string serverPath = Server.MapPath("~").ToString();
                                serverPath += "/img/books/" + qGetDeleteId.PicName;
                                if (File.Exists(serverPath))
                                {
                                    File.Delete(serverPath);
                                }
                            }
                            //------
                            EF.ULSTbl_Books.Remove(qGetDeleteId);

                            foreach (var item in qGetDeleteResID)
                            {
                                EF.ULSTbl_Reserve.Remove(item);
                            }

                            EF.SaveChanges();
                            MultiViewMain.SetActiveView(ViewMain);
                            LiteralMessage.Text = Components.alert("عملیات با موفقیت انجام شد", Components.AlertStyle.success);
                        }
                        else
                        {
                            MultiViewMain.SetActiveView(ViewMain);
                            LiteralMessage.Text = Components.alert("کتاب مورد نظر یافت نشد", Components.AlertStyle.danger);
                        }
                    }
                    else
                    {
                        MultiViewMain.SetActiveView(ViewMain);
                        LiteralMessage.Text = Components.alert("کتاب مورد نظر هم‌اکنون در حالت رزرو قرار دارد", Components.AlertStyle.danger);
                    }

                }
            }
            #endregion

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

                var qGetStatus = (from item in EF.ULSTbl_Books
                                  from ite in EF.ULSTbl_Categories
                                  where item.ID == ID && ite.Status == true && item.Status == true
                                  select new { item.Status }).FirstOrDefault();

                if (qGetStatus != null)
                {
                    text += @"<td><span class='badge bg-label-primary me-1'>فعال</span></td>";
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
                string text = Request.QueryString["txtText"] != null ? Request.QueryString["txtText"].ToString() : null;
                string isbn = Request.QueryString["txtISBNForSearch"] != null ? Request.QueryString["txtISBNForSearch"].ToString() : null;
                literalForSearch.Text = "";
                if (!string.IsNullOrEmpty(isbn) &&
                    (!(isbn.Length == 10 || isbn.Length == 13 || isbn.Length == 17) ||
                    !Regex.IsMatch(isbn, @"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$")))
                {
                    isbn = null;
                    text = null;
                    literalForSearch.Text = Classes.Components.alert("لطفا شابک معتبر وارد کنید (10 یا 13 رقم یا شامل خط تیره)", Classes.Components.AlertStyle.danger);
                }
                var qGetInfo = (from item in EF.ULSTbl_Books
                                join ite in EF.ULSTbl_Categories
                                on item.CategoryId equals ite.ID
                                join it in EF.ULSTbl_Users
                                on item.UserNameAdder equals it.UserName
                                where (String.IsNullOrEmpty(text) || item.BookTitle.Contains(text))
                                && (String.IsNullOrEmpty(isbn) || item.ISBN.Contains(isbn))
                                orderby item.ID descending
                                select new
                                {
                                    UserID = it.ID,
                                    item.ID,
                                    item.BookTitle,
                                    item.Available,
                                    item.DateInsert,
                                    item.PicName,
                                    AdderPic = it.PicName,
                                    CatName = ite.Title,
                                    NameFamily = it.FullName,
                                    Pic = it.PicName
                                }).ToList();

                if (qGetInfo == null)
                    e.Result = "<div class='alert alert-danger mb-2' role='alert'>اطلاعاتی درج نشده است</div>";
                else
                    e.Result = qGetInfo;
            }
        }

        protected void LinqDataSourcedrpCategory_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                e.Result = (from item in EF.ULSTbl_Categories
                            join tblUser in EF.ULSTbl_Users
                            on item.UserNameAdder equals tblUser.UserName
                            select item).ToList();
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

                        var ChekRepid = (from item in EF.ULSTbl_Books
                                         where item.ISBN.Replace("-", "") == txtIsbn.Value.Replace("-", "")
                                         select item).FirstOrDefault();//جستجو برای اینکه قبلا همچنین نامی را این کاربر ثبت نکرده باشد

                        if (ChekRepid == null)//نکرده است
                        {
                            try
                            {
                                if (txtTitle == null)
                                {
                                    LiteralMessage.Text = Components.alert("داده های وارد شده معتبر نیستند", Components.AlertStyle.danger);
                                    return;
                                }
                                //بر روی وصعیت فعال یا غیر فعال کلیک کرده
                                bool Status = true;

                                if (chkStatus.Checked)
                                    Status = true;
                                else
                                    Status = false;

                                bool Special = true;

                                if (chkSpecial.Checked)
                                    Special = true;
                                else
                                    Special = false;

                                if (fileUploadPic.HasFile)//آیا تصویری را انتخاب نموده؟
                                {
                                    string extension = Path.GetExtension(fileUploadPic.FileName);
                                    PicName = Guid.NewGuid() + extension;
                                    fileUploadPic.SaveAs(Server.MapPath("~/img/books") + "//" + PicName);
                                }

                                //----------------
                                //درج و مقداردهی جدول با کنترل ها
                                ULSTbl_Books TG = new ULSTbl_Books()
                                {
                                    CategoryId = int.Parse(drpCategory.SelectedValue),
                                    PicName = PicName,
                                    DateInsert = DateTime.Now,
                                    Status = Status,
                                    UserNameAdder = UserId,
                                    IsSpecial = Special,
                                    Count = int.Parse(txtTedad.Value),
                                    Available = int.Parse(txtTedad.Value),
                                    BookTitle = txtTitle.Value,
                                    AuthorName = txtAuthor.Value,
                                    ISBN = txtIsbn.Value,
                                    PublisherName = txtPublisher.Value,
                                    TranslatorName = txtTranslatorName.Value,
                                };

                                EF.ULSTbl_Books.Add(TG);
                                EF.SaveChanges();

                                //--------
                                //به ترتیب پاک سازی - همگام سازی گرید ، نمایش گرید و نمایش پیغام درج موفق
                                Clean();
                                MultiViewMain.SetActiveView(ViewMain);
                                LiteralMessage.Text = Components.alert("عملیات با موفقیت انجام شد", Components.AlertStyle.success);
                                btnAdd.Visible = true;
                                PanelSearch.Visible = true;
                            }
                            catch (System.FormatException)
                            {
                                LiteralMessage.Text = Components.alert("داده های وارد شده معتبر نیستند", Components.AlertStyle.danger);
                            }
                            catch (Exception ex)
                            {
                                LiteralMessage.Text = Components.alert(ex.Message, Components.AlertStyle.danger);
                            }
                        }
                        else//کرده است
                        {
                            //نمایش پیغام تکراری بودن-نمایش ویو مقادیر
                            LiteralMessage.Text = Components.alert("این کتاب را قبلا ثبت کرده اید", Components.AlertStyle.danger);
                            MultiViewMain.SetActiveView(ViewFileds);
                            btnAdd.Visible = true;
                            PanelSearch.Visible = true;
                        }
                    }
                    #endregion

                    #region [Edit]
                    if (Request.QueryString["Edit"] != null)
                    {
                        string HashId = Request.QueryString["Edit"];
                        int RowId = Hash2ID.HAsh2ID(HashId);

                        var Searchquery = (from item in EF.ULSTbl_Books
                                           where item.ID == RowId
                                           select item).FirstOrDefault();//پیدا کردن سط مورد تقاضای کاربر از جدول
                        if (Searchquery != null)//پیدا شد
                        {
                            var getres = (from res in EF.ULSTbl_Reserve
                                          where res.StatusID < ReserveStatus.Delivered && res.BookID == RowId
                                          select res).Count();
                            int available = int.Parse(txtTedad.Value) - getres;
                            if (available >= 0)
                            {
                                Searchquery.CategoryId = int.Parse(drpCategory.SelectedValue);
                                if (getres!=0)
                                {
                                    Searchquery.Count = int.Parse(txtTedad.Value);
                                }
                                Searchquery.Available = available;
                                Searchquery.BookTitle = txtTitle.Value;
                                Searchquery.AuthorName = txtAuthor.Value;
                                Searchquery.PublisherName = txtPublisher.Value;
                                Searchquery.TranslatorName = txtTranslatorName.Value;

                                if (fileUploadPic.HasFile)//اگر تصویر جدیدی انتخاب نکرد پس تمایل به به روز رسانی تصویر ندارد
                                {
                                    if (Searchquery.PicName != "no-photo.jpg")
                                    {
                                        try
                                        {
                                            string DelPath = Server.MapPath("../img/books") + "//" + Searchquery.PicName;
                                            System.IO.File.Delete(DelPath);
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    string extension = Path.GetExtension(fileUploadPic.FileName);
                                    string PicName = Guid.NewGuid() + extension;
                                    fileUploadPic.SaveAs(Server.MapPath("~/img/books") + "//" + PicName);
                                    Searchquery.PicName = PicName;
                                }


                                if (chkStatus.Checked)
                                    Searchquery.Status = true;
                                else
                                    Searchquery.Status = false;

                                if (chkSpecial.Checked)
                                    Searchquery.IsSpecial = true;
                                else
                                    Searchquery.IsSpecial = false;

                                EF.SaveChanges();

                                //------------
                                //به ترتیب پاک سازی - همگام سازی گرید ، نمایش گرید و نمایش پیغام درج موفق
                                Clean();
                                MultiViewMain.SetActiveView(ViewMain);
                                LiteralMessage.Text = Components.alert("عملیات با موفقیت انجام شد", Components.AlertStyle.success);
                                btnAdd.Visible = true;
                            }
                            else//تکراری بود
                            {
                                //نمایش پیغام تکراری بودن-نمایش ویو مقادیر
                                MultiViewMain.SetActiveView(ViewFileds);
                                LiteralMessage.Text = Components.alert("کاهش موجودی تنها به میزان کتاب‌های موجود در کتابخانه امکان‌پذیر است", Components.AlertStyle.danger);
                                btnAdd.Visible = true;
                                Clean();
                            }
                        }
                    }
                    #endregion
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("./books");
        }
        #endregion

        #region[Clean]
        public void Clean()
        {
            txtAuthor.Value = "";
            txtTedad.Value = "";
            txtTitle.Value = "";
            txtTranslatorName.Value = "";
            txtIsbn.Value = "";
            txtPublisher.Value = "";
            chkStatus.Checked = true;
            chkSpecial.Checked = false;
            ImageNopic.ImageUrl = ResolveUrl("../img/Error/no-photo.png");
        }
        #endregion

        protected void btnResetPic_Click(object sender, EventArgs e)
        {
            try
            {
                using (var EF = new ULSDBEntities())
                {
                    if (Request.QueryString["Edit"] != null)
                    {
                        string HashId = Request.QueryString["Edit"];
                        int RowId = Hash2ID.HAsh2ID(HashId);
                        var qGetPic = (from item in EF.ULSTbl_Books
                                       where item.ID == RowId
                                       select item).FirstOrDefault();
                        if (qGetPic.PicName != "no-photo.png")
                        {
                            File.Delete(Server.MapPath("../img/books") + "//" + qGetPic.PicName);
                            qGetPic.PicName = "no-photo.png";
                            EF.SaveChanges();
                        }
                        if (qGetPic != null)
                        {
                            ImageNopic.ImageUrl = "../img/books/" + qGetPic.PicName;
                        }
                        LiteralMessage.Text = Components.alert("تصویر با موفقیت بازنشانی شد", Components.AlertStyle.success);
                        btnResetPic.Visible = false;
                    }
                    else
                    {
                        btnResetPic.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LiteralMessage.Text = Components.alert("خطا:" + ex.Message, Components.AlertStyle.danger);
            }

        }
    }
}