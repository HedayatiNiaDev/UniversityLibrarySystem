using Classes;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem
{
    public partial class Search : System.Web.UI.Page
    {
        int numCards = 12;
        string bookName;
        string author;
        string translator;
        string publisher;
        string isbn;
        int category = 0;
        bool isSpecial;
        bool isAvailable;
        //LSI Config
        bool sortZA = true; //descending
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bookName = string.Empty;
                author = string.Empty;
                translator = string.Empty;
                publisher = string.Empty;
                isbn = string.Empty;
                bookName = GetFirstQueryValue(Request.QueryString, "BookName");
                author = GetFirstQueryValue(Request.QueryString, "Author");
                translator = GetFirstQueryValue(Request.QueryString, "Translator");
                publisher = GetFirstQueryValue(Request.QueryString, "Publisher");
                isbn = GetFirstQueryValue(Request.QueryString, "ISBN");
                if (!string.IsNullOrEmpty(GetFirstQueryValue(Request.QueryString, "Category")))
                {
                    category = int.Parse(GetFirstQueryValue(Request.QueryString, "Category"));
                }
                else
                {
                    category = 0;
                }
                isSpecial = GetFirstQueryValue(Request.QueryString, "isSpecial") == "on";
                isAvailable = GetFirstQueryValue(Request.QueryString, "isAvailable") == "on";
                if (string.IsNullOrEmpty(LastID.Text))
                    Books.Text = NewBook();

            }
            catch (Exception ex)
            {
                lblError.Text = "یک خطای غیرمنتظره رخ داده است: " + ex.Message;
            }
            if (!RegExVars())
            {
                lblError.Text = "خطا:مقادیر به درستی وارد نشده اند";
            }
            Page.Title = SiteConfig.mixTitle("جستو جو " + bookName);
        }

        private string GetFirstQueryValue(NameValueCollection queryString, string key)
        {
            string[] values = queryString.GetValues(key);
            if (values != null)
            {
                return values[0];
            }
            return null;
        }

        public bool RegExVars()
        {
            if (!string.IsNullOrEmpty(bookName) && !Regex.IsMatch(bookName, @"^.{2,100}$"))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(author) && !Regex.IsMatch(author, @"^.{2,50}$"))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(translator) && !Regex.IsMatch(translator, @"^.{2,50}$"))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(publisher) && !Regex.IsMatch(publisher, @"^.{2,50}$"))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(isbn) &&
                (!(isbn.Length == 10 || isbn.Length == 13 || isbn.Length == 17) ||
                !Regex.IsMatch(isbn, @"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$")))
            {
                return false;
            }


            if (category < 0)
            {
                return false;
            }
            return true;
        }
        public string NewBook()
        {
            if (!RegExVars())
            {
                NextPage.Visible = false;
                return "";
            }

            string html = "";
            long.TryParse(LastID.Text, out var lsID);
            using (var EF = new ULSDBEntities())
            {
                var queryNewBook = (from TableBook in EF.ULSTbl_Books
                                    join TableCategory in EF.ULSTbl_Categories
                                    on TableBook.CategoryId equals TableCategory.ID
                                    where (lsID == 0 || ((sortZA&& lsID > TableBook.ID)|| lsID < TableBook.ID))
                                    && (isAvailable == false || TableBook.Available > 0)
                                    && TableBook.Status == true
                                    && TableCategory.Status == true
                                    && (string.IsNullOrEmpty(bookName) || TableBook.BookTitle.Contains(bookName))
                                    && (string.IsNullOrEmpty(author) || TableBook.AuthorName.Contains(author))
                                    && (string.IsNullOrEmpty(publisher) || TableBook.PublisherName.Contains(publisher))
                                    && (string.IsNullOrEmpty(isbn) || TableBook.ISBN.Contains(isbn))
                                    && (!isSpecial || TableBook.IsSpecial == isSpecial)
                                    && (category == 0 || TableBook.CategoryId == category)
                                    orderby TableBook.ID descending
                                    select TableBook).Take(numCards + 1).ToList();

                NextPage.Visible = queryNewBook.Count == numCards + 1;
                if (NextPage.Visible) queryNewBook.RemoveAt(numCards);
                LastID.Text = queryNewBook.LastOrDefault()?.ID.ToString();
                foreach (var tableBook in queryNewBook)
                {
                    html += @"
                        <div class=""col-xl-3 col-lg-4 col-md-4 col-6"">
                            <div class='properties pb-30'>";
                    bool isSpec = false;
                    if (tableBook.IsSpecial != null)
                        if (tableBook.IsSpecial == true)
                            isSpec = true;
                    int Available = -1;
                    if (tableBook.Available != null)
                        Available = tableBook.Available == 0 ? 0 : 1;
                    html += Components.bookCard(tableBook.ID, tableBook.BookTitle, tableBook.PicName, tableBook.AuthorName, tableBook.PublisherName, Available, isSpec);

                    html += @"                            </div>
                        </div>
";
                }
            }
            return html;
        }

        public string GetCategories()
        {
            string html = "";
            var categories = SiteConfig.GetCategories(); // دریافت دسته‌بندی‌ها از کش یا پایگاه داده

            if (categories != null && categories.Any())
            {
                foreach (var item in categories)
                    html += "<option value='" + item.ID + "'>" + item.Title + "</option>";
            }

            return html;
        }

        protected void NextPage_Click(object sender, EventArgs e) => Books.Text += NewBook();
    }
}