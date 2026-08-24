using Classes;
using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem.Manager
{
    public partial class Faqs : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiViewMain.SetActiveView(ViewMain);
                btnAdd.Visible = true;
                PanelSearch.Visible = true;

                #region [Load]

                if (Request.QueryString["Value"] != null)
                {
                    MultiViewMain.SetActiveView(ViewFileds);
                    btnAdd.Visible = false;
                    PanelSearch.Visible = false;
                }

                if (Request.QueryString["Edit"] != null)
                {
                    MultiViewMain.SetActiveView(ViewFileds);
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

                    using (var EF = new ULSDBEntities())
                    {
                        var qGetCategoryInfo = (from item in EF.ULSTbl_Faq
                                                where item.ID == id
                                                select item).FirstOrDefault();
                        if (qGetCategoryInfo != null)
                        {
                            txtQuestion.Value = qGetCategoryInfo.Question;
                            txtAnswer.Value = qGetCategoryInfo.Answer;

                            if (qGetCategoryInfo.Status == true)
                                chkStatus.Checked = true;
                            else
                                chkStatus.Checked = false;
                        }
                        else
                        {
                            btnAdd.Visible = true;
                            PanelSearch.Visible = true;
                            LiteralMessage.Text = Components.alert("خطا:داده ای یافت نشد", Components.AlertStyle.danger);
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
                        var qGetDeleteId = (from item in EF.ULSTbl_Faq
                                            where item.ID == id
                                            select item).FirstOrDefault();

                        if (qGetDeleteId != null)
                        {
                            EF.ULSTbl_Faq.Remove(qGetDeleteId);
                            EF.SaveChanges();

                            MultiViewMain.SetActiveView(ViewMain);
                            LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                            System.Web.HttpRuntime.Cache.Remove("FaqPageCache");
                        }
                        else
                        {
                            btnAdd.Visible = true;
                            PanelSearch.Visible = true;
                            LiteralMessage.Text = Components.alert("خطا:داده ای یافت نشد", Components.AlertStyle.danger);
                            MultiViewMain.SetActiveView(ViewMain);
                        }
                    }
                }
                #endregion
            }
        }

        #region [Status]
        public string Status(object id)
        {
            using (var EF = new ULSDBEntities())
            {
                int ID = Convert.ToInt32(id);
                string text = "";

                var qGetStatus = (from item in EF.ULSTbl_Faq
                                  where item.ID == ID
                                  select item).FirstOrDefault();

                if (qGetStatus.Status == true)
                    text += @"<td><span class='badge bg-label-success me-1'>فعال</span></td>";
                else
                    text += @"<td><span class='badge bg-label-danger me-1'>غیر فعال</span></td>";

                return text;
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

        #region [LoadTabel]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinqDataSourceNews_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            using (var EF = new ULSDBEntities())
            {
                string text = null;
                if (Request.QueryString["txtText"] != null)
                {
                    text = Request.QueryString["txtText"].ToString();
                }
                var qGetInfo = (from item in EF.ULSTbl_Faq
                                join ite in EF.ULSTbl_Users
                                on item.UserNameAdder equals ite.UserName
                                where (String.IsNullOrEmpty(text) || item.Question.Contains(text))
                                orderby item.ID
                                select new
                                {
                                    UserID = ite.ID,
                                    item.ID,
                                    item.Question,
                                    item.DateInsert,
                                    NameFamily = ite.FullName,
                                    AdderPic = ite.PicName
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

                using (var EF = new ULSDBEntities())
                {
                    #region [NewsSave]
                    if (Request.QueryString["Value"] != null)
                    {
                        bool Status;

                        if (chkStatus.Checked == true)
                            Status = true;
                        else
                            Status = false;
                        //----------------
                        //درج و مقداردهی جدول با کنترل ها
                        ULSTbl_Faq TG = new ULSTbl_Faq()
                        {
                            Answer = txtAnswer.Value,
                            DateInsert = DateTime.Now,
                            Question = txtQuestion.Value,
                            UserNameAdder = UserId,
                            Status = Status
                        };

                        EF.ULSTbl_Faq.Add(TG);
                        EF.SaveChanges();
                        //--------
                        //به ترتیب پاک سازی - همگام سازی گرید ، نمایش گرید و نمایش پیغام درج موفق
                        Clean();
                        MultiViewMain.SetActiveView(ViewMain);
                        LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                        btnAdd.Visible = true;
                        PanelSearch.Visible = true;
                    }
                    #endregion

                    #region [Edit]
                    if (Request.QueryString["Edit"] != null)
                    {
                        string HashId = Request.QueryString["Edit"];
                        int RowId = Hash2ID.HAsh2ID(HashId);

                        var Searchquery = (from item in EF.ULSTbl_Faq
                                           where item.ID == RowId
                                           select item).FirstOrDefault();//پیدا کردن سط مورد تقاضای کاربر از جدول
                        if (Searchquery != null)//پیدا شد
                        {
                            Searchquery.Answer = txtAnswer.Value;
                            Searchquery.Question = txtQuestion.Value;

                            if (chkStatus.Checked == true)
                                Searchquery.Status = true;
                            else
                                Searchquery.Status = false;

                            EF.SaveChanges();

                            //------------
                            //به ترتیب پاک سازی - همگام سازی گرید ، نمایش گرید و نمایش پیغام درج موفق
                            Clean();
                            MultiViewMain.SetActiveView(ViewMain);
                            LiteralMessage.Text = Components.alert("درخواست شما با موفقیت ثبت شد", Components.AlertStyle.success);
                            btnAdd.Visible = true;
                            PanelSearch.Visible = true;
                        }
                    }
                    #endregion
                }
                System.Web.HttpRuntime.Cache.Remove("FaqsPageCache");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clean();

            Response.Redirect("~/Admin/Faq");
        }
        #endregion

        #region[Method]

        public void Clean()
        {
            //Tbl_MostanadattxtReportDate.Value = "";
            txtAnswer.Value = "";
            txtQuestion.Value = "";
            chkStatus.Checked = true;
        }
        #endregion
    }
}