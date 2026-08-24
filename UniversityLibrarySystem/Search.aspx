<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="UniversityLibrarySystem.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <main>
            <div class="breadcrumb-items">
                <div class="row">
                    <ul class="breadcrumb">
                        <li>
                            <a href="/" class="breadcrumb-link">صفحه اصلی</a>
                        </li>
                        <li class="chevron"><span class="fa fa-chevron-left"></span></li>
                        <li>
                            <span class="breadcrumb-active">جستوجو</span>
                        </li>
                    </ul>
                </div>
            </div>
            <asp:ScriptManager ID="scriptManager" runat="server" />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="listing-area pt-50 pb-50">
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-4 col-md-12 col-12">
                                    <form method="get" action="/Search" id="searchForm" class="needs-validation" novalidate>
                                        <div class="category-listing mb-50">
                                            <div class="single-listing">
                                                <asp:Label Text="" ID="lblError" CssClass="text-danger" runat="server" />

                                                <!-- Book Name Input -->
                                                <div class="mb-3">
                                                    <div class="row border-radius-30 form-control-border m-0 p-0">
                                                        <div class="col-10 px-0 m-0">
                                                            <input type="text"
                                                                class="form-control search-input border-transparent"
                                                                placeholder="نام کتاب را وارد نمایید"
                                                                name="BookName"
                                                                id="txtName"
                                                                pattern="^.{2,100}$"
                                                                title="نام کتاب باید بین ۲ تا ۱۰۰ کاراکتر باشد">
                                                        </div>
                                                        <div class="col-2 px-0 m-0">
                                                            <button type="submit" onclick="sendQueryString()" class="search-btn w-100">
                                                                <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                                                                    <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0" />
                                                                </svg>
                                                            </button>
                                                        </div>
                                                    </div>
                                                    <div class="invalid-feedback text-danger">
                                                        لطفا نام کتاب معتبر وارد کنید
                                                    </div>
                                                </div>

                                                <div class="small-tittle">
                                                    <h4>فیلتر ها</h4>
                                                </div>

                                                <!-- Category Select -->
                                                <div class="select-job-items2 mb-30">
                                                    <div class="col-xl-12">
                                                        <select name="Category" id="categorySelect" class="form-control">
                                                            <option value="0">فیلتر براساس دسته بندی</option>
                                                            <%=GetCategories() %>
                                                        </select>
                                                    </div>
                                                </div>

                                                <!-- Author Input -->
                                                <div class="row mb-3">
                                                    <div class="col-12">
                                                        <input type="text"
                                                            class="form-control border-radius-30"
                                                            placeholder="نام نویسنده"
                                                            name="Author"
                                                            id="txtAuthor"
                                                            pattern="^.{2,50}$"
                                                            title="نام نویسنده باید بین ۲ تا ۵۰ حرف باشد"
                                                            maxlength="50">
                                                        <div class="invalid-feedback text-danger">
                                                            لطفا نام نویسنده معتبر وارد کنید
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- Translator Input -->
                                                <div class="row mb-3">
                                                    <div class="col-12">
                                                        <input type="text"
                                                            class="form-control border-radius-30"
                                                            placeholder="نام مترجم"
                                                            name="Translator"
                                                            id="txtTranslator"
                                                            pattern="^.{2,50}$"
                                                            title="نام مترجم باید بین ۲ تا ۵۰ حرف باشد"
                                                            maxlength="50">
                                                        <div class="invalid-feedback text-danger">
                                                            لطفا نام مترجم معتبر وارد کنید
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- Publisher Input -->
                                                <div class="row mb-3">
                                                    <div class="col-12">
                                                        <input type="text"
                                                            class="form-control border-radius-30"
                                                            placeholder="نام انتشارات"
                                                            name="Publisher"
                                                            id="txtPublisher"
                                                            pattern="^.{2,50}$"
                                                            title="نام انتشارات باید بین ۲ تا ۵۰ حرف باشد"
                                                            maxlength="50">
                                                        <div class="invalid-feedback text-danger">
                                                            لطفا نام انتشارات معتبر وارد کنید
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- ISBN Input -->
                                                <div class="row mb-3">
                                                    <div class="col-12">
                                                        <input type="text"
                                                            class="form-control border-radius-30"
                                                            placeholder="شابک"
                                                            name="ISBN"
                                                            id="txtISBN"
                                                            pattern="^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$"
                                                            title="شابک باید ۱۰ یا ۱۳ رقم باشد یا شامل خط تیره باشد">
                                                        <div class="invalid-feedback text-danger">
                                                            لطفا شابک معتبر وارد کنید (10 یا 13 رقم یا شامل خط تیره)
                                                        </div>
                                                    </div>
                                                </div>


                                                <!-- Checkboxes -->
                                                <div class="select-Categories pt-45 pb-10">
                                                    <label class="container">
                                                        کتاب ویژه
                                                <input type="checkbox" name="isSpecial" id="isSpecial">
                                                        <span class="checkmark"></span>
                                                    </label>
                                                </div>
                                                <div class="select-Categories pt-45 pb-10">
                                                    <label class="container">
                                                        فقط کتاب های موجود
                                                <input type="checkbox" name="isAvailable" id="isAvailable">
                                                        <span class="checkmark"></span>
                                                    </label>
                                                </div>

                                                <button type="submit" onclick="sendQueryString()" class="btn header-btn mx-1 w-100">اعمال فیلتر</button>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                                <div class="col-lg-8 col-md-12 col-12">
                                    <div class="row justify-content-start">
                                        <asp:Label ID="lblSearch" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="best-selling p-0">
                                        <div class="row" id="placeholder-content">
                                            <asp:TextBox ID="LastID" runat="server" Visible="false" Text=""></asp:TextBox>
                                            <asp:Literal ID="Books" Text="" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div class="more-btn text-center mt-15">
                                                <asp:LinkButton ID="NextPage" Text="مشاهده کتب بیشتر" runat="server" OnClick="NextPage_Click" class="border-btn border-btn2 more-btn2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </main>

        <script src="/js/jquery-3.6.0.min.js"></script>
        <script>
            function sendQueryString() {
                var txtName = document.getElementById("txtName").value;
                var categorySelect = document.getElementById("categorySelect").value;
                var txtAuthor = document.getElementById("txtAuthor").value;
                var txtTranslator = document.getElementById("txtTranslator").value;
                var txtPublisher = document.getElementById("txtPublisher").value;
                var txtISBN = document.getElementById("txtISBN").value;
                var isSpecial = document.getElementById("isSpecial").checked ? "on" : "off";
                var isAvailable = document.getElementById("isAvailable").checked ? "on" : "off";

                var url = 'Search?BookName=' + encodeURIComponent(txtName) +
                    '&Category=' + encodeURIComponent(categorySelect) +
                    '&Author=' + encodeURIComponent(txtAuthor) +
                    '&Translator=' + encodeURIComponent(txtTranslator) +
                    '&Publisher=' + encodeURIComponent(txtPublisher) +
                    '&ISBN=' + encodeURIComponent(txtISBN) +
                    '&isSpecial=' + encodeURIComponent(isSpecial) +
                    '&isAvailable=' + encodeURIComponent(isAvailable);

                window.location.href = url;
            }

        </script>
        <script>
            document.addEventListener('DOMContentLoaded', function () {
                const urlParams = new URLSearchParams(window.location.search);

                // Populate form fields from URL parameters
                const fields = {
                    'BookName': 'txtName',
                    'Author': 'txtAuthor',
                    'Translator': 'txtTranslator',
                    'Publisher': 'txtPublisher',
                    'ISBN': 'txtISBN'
                };

                for (const [param, elementId] of Object.entries(fields)) {
                    const value = urlParams.get(param);
                if (value) {
                    document.getElementById(elementId).value = value;
                }
            }

                // Handle Category select
                const category = urlParams.get('Category');
            if (category) {
                const categorySelect = document.getElementById('categorySelect');
        
                // Try to find exact match first
                let found = Array.from(categorySelect.options).some(option => option.value === category);
        
                // If no exact match, try partial match
                if (!found) {
                    Array.from(categorySelect.options).some(option => {
                        if (option.text.includes(category)) {
                            categorySelect.value = option.value;
                            return true;
                        }
                        return false;
                    });
                } else {
                    categorySelect.value = category;
                }

                // Trigger change event for nice-select if it exists
                if (window.$ && $.fn.niceSelect) {
                    $(categorySelect).niceSelect('update');
                    const selectedCategoryText = $(categorySelect).find('option:selected').text();
                    $('.nice-select.category .current').text(selectedCategoryText);
                }
            }

            // Handle checkboxes
            const checkboxFields = [
                { param: 'isSpecial', id: 'isSpecial' },
                { param: 'isAvailable', id: 'isAvailable' }
            ];

            checkboxFields.forEach(field => {
                const value = urlParams.get(field.param);
                const checkbox = document.getElementById(field.id);
        
                if (value === 'on') {
                    checkbox.checked = true;
                } else if (value === 'off') {
                    checkbox.checked = false;
                }
            });

            // Form Validation
            const forms = document.querySelectorAll('.needs-validation');
            Array.from(forms).forEach(form => {
                form.addEventListener('submit', event => {
                    if (!form.checkValidity()) {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                    form.classList.add('was-validated');
                }, false);
            });

            // ISBN Input Validation
            const isbnInput = document.getElementById('txtISBN');
            isbnInput.addEventListener('input', function () {
                const isbn = this.value.trim();
                const regex = /^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]*$/;
        
                if (isbn === '') {
                    this.setCustomValidity('');
                } else if (!regex.test(isbn)) {
                    this.setCustomValidity("شابک باید ۱۰ یا ۱۳ رقم باشد یا شامل خط تیره باشد");
                } else {
                    this.setCustomValidity('');
                }
                this.reportValidity();
            });

            // Real-time Input Validation
            document.querySelectorAll('input[type="text"]').forEach(input => {
                input.addEventListener('input', function () {
                    this.classList.toggle('is-valid', this.checkValidity());
                    this.classList.toggle('is-invalid', !this.checkValidity());
                });
            });
            });


        </script>
        <script>
            // گرفتن query string از URL
            let queryString = window.location.search;

            // تبدیل query string به شیء
            let urlParams = new URLSearchParams(queryString);

            // ایجاد یک شیء جدید برای نگهداری مقادیر غیرخالی
            let newParams = new URLSearchParams();

            // فیلتر کردن مقادیر غیرخالی
            urlParams.forEach((value, key) => {
                if (value !== '' && value !== '0' && value !== 'off') {
                    newParams.append(key, value);
                }
            });

            // به‌روزرسانی query string در URL
            let newUrl = window.location.origin + window.location.pathname + '?' + newParams.toString();
            window.history.replaceState(null, null, newUrl);

        </script>
    </form>
</asp:Content>
