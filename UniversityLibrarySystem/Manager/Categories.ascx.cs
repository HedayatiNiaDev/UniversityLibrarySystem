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
    public partial class Categories : System.Web.UI.UserControl
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
                    MultiViewMain.SetActiveView(ViewFileds);
                    btnAdd.Visible = false;
                }

                if (Request.QueryString["Edit"] != null)
                {
                    MultiViewMain.SetActiveView(ViewFileds);
                    btnAdd.Visible = false;
                }

                if (Request.QueryString["Delete"] != null)
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

                    using (var EF = new ULSDBEntities())
                    {
                        var qGetCategoryInfo = (from item in EF.ULSTbl_Categories
                                                where item.ID == id
                                                select item).FirstOrDefault();
                        if (qGetCategoryInfo != null)
                        {
                            btnResetPic.Visible = qGetCategoryInfo.PicName != "no-photo.png";
                            txtTitle.Value = qGetCategoryInfo.Title;

                            if (qGetCategoryInfo.PicName != "no-photo.png")
                                ImageNopic.ImageUrl = "../img/categories/" + qGetCategoryInfo.PicName;
                            else
                                ImageNopic.ImageUrl = "../img/Error/no-photo.png";

                            if (qGetCategoryInfo.Status == true)
                                chkStatus.Checked = true;
                            else
                                chkStatus.Checked = false;
                        }
                        else
                        {
                            LiteralMessage.Text = Components.alert("دسته بندی مورد نظر یافت نشد", Components.AlertStyle.danger);
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

                    using (var EF = new ULSDBEntities())
                    {
                        var qGetDeleteId = (from item in EF.ULSTbl_Categories
                                            where item.ID == id
                                            select item).FirstOrDefault();
                        var CanNotDelete = (from item in EF.ULSTbl_Reserve
                                            join books in EF.ULSTbl_Books
                                            on id equals books.CategoryId
                                            where item.BookID == books.ID && (item.StatusID == ReserveStatus.Reservation || item.StatusID == ReserveStatus.Fine)
                                            select item).FirstOrDefault();
                        if (CanNotDelete == null)
                        {
                            var qGetDeleteBookId = (from item in EF.ULSTbl_Books
                                                    where item.CategoryId == id
                                                    select item).ToList();
                            var qGetDeleteResID = (from item in EF.ULSTbl_Reserve
                                                   join books in EF.ULSTbl_Books
                                                   on item.BookID equals books.ID
                                                   where books.CategoryId == id
                                                   select item).ToList();
                            if (qGetDeleteId != null)
                            {
                                //------
                                foreach (var item in qGetDeleteResID)
                                {
                                    EF.ULSTbl_Reserve.Remove(item);
                                }
                                foreach (var item in qGetDeleteBookId)
                                {
                                    if (item.PicName != null && item.PicName != "no-photo.png")
                                    {
                                        string serverPath = Server.MapPath("~").ToString();
                                        serverPath += "/img/books/" + item.PicName;
                                        if (File.Exists(serverPath))
                                        {
                                            File.Delete(serverPath);
                                        }
                                    }
                                    EF.ULSTbl_Books.Remove(item);
                                }
                                if (qGetDeleteId.PicName != null && qGetDeleteId.PicName != "no-photo.png")
                                {
                                    string serverPath = Server.MapPath("~").ToString();
                                    serverPath += "/img/categories/" + qGetDeleteId.PicName;
                                    if (File.Exists(serverPath))
                                    {
                                        File.Delete(serverPath);
                                    }
                                }
                                EF.ULSTbl_Categories.Remove(qGetDeleteId);
                                EF.SaveChanges();
                            }
                            MultiViewMain.SetActiveView(ViewMain);
                            System.Web.HttpRuntime.Cache.Remove("Categories");
                            LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                        }
                        else
                        {
                            MultiViewMain.SetActiveView(ViewMain);
                            LiteralMessage.Text = Components.alert("یکی از کتب دسته بندی‌ مورد نظر در حالت رزرو قرار دارد", Components.AlertStyle.danger);
                        }
                    }
                }
            }
        }
        #endregion


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

                var qGetStatus = (from item in EF.ULSTbl_Categories
                                  where item.ID == ID
                                  select item).FirstOrDefault();

                if (qGetStatus.Status == true)
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

        #region [BooksCounter]
        public string BooksCounter(object id)
        {
            using (ULSDBEntities EF = new ULSDBEntities())
            {
                int ID = Convert.ToInt32(id);
                string text = "";

                var qGetStatus = (from item in EF.ULSTbl_Books
                                  where item.CategoryId == ID
                                  select item).Count();

                if (qGetStatus != null)
                {
                    text += @"<td>" + qGetStatus + "</td>";
                }
                else
                {
                    text += @"<td>خطا</td>";
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
                var qGetInfo = (from item in EF.ULSTbl_Categories
                                from it in EF.ULSTbl_Users
                                where item.UserNameAdder == it.UserName
                                orderby item.ID descending
                                select new
                                {
                                    UserID=it.ID,
                                    item.ID,
                                    item.Title,
                                    item.DateInsert,
                                    item.PicName,
                                    NameFamily = it.FullName,
                                    AdderPic = it.PicName
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
            System.Web.HttpRuntime.Cache.Remove("Categories");
            string UserId = Membership.GetUser().UserName.ToLower();

            using (var EF = new ULSDBEntities())
            {
                #region [NewsSave]
                if (Request.QueryString["Value"] != null)
                {
                    var checkCat = (from tCat in EF.ULSTbl_Categories
                                    where tCat.Title == txtTitle.Value
                                    select tCat).FirstOrDefault();
                    if (checkCat == null)
                    {
                    string PicName = "no-photo.png";

                    if (fileUploadPic.HasFile)//آیا تصویری را انتخاب نموده؟
                    {
                        string extension = Path.GetExtension(fileUploadPic.FileName);
                        PicName = Guid.NewGuid() + extension;
                        fileUploadPic.SaveAs(Server.MapPath("../img/Categories") + "//" + PicName);
                    }
                    //بر روی وصعیت فعال یا غیر فعال کلیک کرده
                    bool Status = true;

                    if (chkStatus.Checked)
                        Status = true;
                    else
                        Status = false;

                    //----------------
                    //درج و مقداردهی جدول با کنترل ها
                    ULSTbl_Categories TG = new ULSTbl_Categories()
                    {
                        UserNameAdder = UserId,
                        DateInsert = DateTime.Now,
                        PicName = PicName,
                        Status = Status,
                        Title = txtTitle.Value,
                    };

                    EF.ULSTbl_Categories.Add(TG);
                    EF.SaveChanges();

                    //--------
                    //به ترتیب پاک سازی - همگام سازی گرید ، نمایش گرید و نمایش پیغام درج موفق
                    Clean();
                    MultiViewMain.SetActiveView(ViewMain);
                    LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                    btnAdd.Visible = true;
                    }
                    else
                    {
                        Clean();
                        MultiViewMain.SetActiveView(ViewMain);
                        LiteralMessage.Text = Components.alert("عملیات با شکست مواجه شد", Components.AlertStyle.danger);
                        btnAdd.Visible = true;
                    }
                }
                #endregion

                #region [Edit]
                if (Request.QueryString["Edit"] != null)
                {
                    string HashId = Request.QueryString["Edit"];
                    int RowId = Hash2ID.HAsh2ID(HashId);

                    var Searchquery = (from item in EF.ULSTbl_Categories
                                       where item.ID == RowId
                                       select item).FirstOrDefault();//پیدا کردن سط مورد تقاضای کاربر از جدول
                    if (Searchquery != null)//پیدا شد
                    {
                        //مقادیر را از کنترل ها به فیلد های مرتبط اضافه کن
                        Searchquery.Title = txtTitle.Value;

                        if (chkStatus.Checked)
                            Searchquery.Status = true;
                        else
                            Searchquery.Status = false;

                        if (fileUploadPic.HasFile)//اگر تصویر جدیدی انتخاب نکرد پس تمایل به به روز رسانی تصویر ندارد
                        {
                            //اگر کرد پس قبلی را پاک کن
                            //جدید را بزار
                            if (Searchquery.PicName != "no-photo.jpg")
                            {
                                try
                                {
                                    string DelPath = Server.MapPath("../img/Categories") + "//" + Searchquery.PicName;
                                    System.IO.File.Delete(DelPath);
                                }
                                catch
                                {

                                }
                            }

                            string extension = Path.GetExtension(fileUploadPic.FileName);
                            string PicName = Guid.NewGuid() + extension;
                            fileUploadPic.SaveAs(Server.MapPath("~/img/Categories") + "//" + PicName);
                            Searchquery.PicName = PicName;
                        }

                        EF.SaveChanges();

                        //------------
                        //به ترتیب پاک سازی - همگام سازی گرید ، نمایش گرید و نمایش پیغام درج موفق
                        Clean();
                        MultiViewMain.SetActiveView(ViewMain);
                        LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                        btnAdd.Visible = true;
                    }
                }
                #endregion
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Categories");
        }
        #endregion

        #region[Clean]
        public void Clean()
        {
            txtTitle.Value = "";
            chkStatus.Checked = true;
            ImageNopic.ImageUrl = ResolveUrl("../img/Error/no-photo.png");
        }

        #endregion

        protected void btnResetPic_Click(object sender, EventArgs e)
        {
            try
            {
                using (var EF = new ULSDBEntities())
                {
                    if (Request.QueryString["Edit"]!=null)
                    {
                        string HashId = Request.QueryString["Edit"];
                        int RowId = Hash2ID.HAsh2ID(HashId);
                        var qGetPic = (from item in EF.ULSTbl_Categories
                                       where item.ID == RowId
                                       select item).FirstOrDefault();
                        if (qGetPic.PicName != "no-photo.png")
                        {
                            File.Delete(Server.MapPath("../img/categories") + "//" + qGetPic.PicName);
                            qGetPic.PicName = "no-photo.png";
                            EF.SaveChanges();
                        }
                        if (qGetPic != null)
                        {
                            ImageNopic.ImageUrl = "../img/categories/" + qGetPic.PicName;
                        }
                        LiteralMessage.Text = Components.alert("تصویر با موفقیت بازنشانی شد", Components.AlertStyle.success);
                        btnResetPic.Visible = false;
                    }
                    else
                    {
                        btnResetPic.Visible=false;
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
