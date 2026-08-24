<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Books.ascx.cs" Inherits="UniversityLibrarySystem.Manager.Books" %>
<style>
    .activepage {
        line-height: 2;
        text-align: center;
        min-width: calc( 2.0075rem + 0px );
        border-color: #00cfdd;
        background-color: #00cfdd;
        color: #fff;
        box-shadow: 0 0.125rem 0.25rem rgba(147, 158, 170, 0.4);
        border-top-right-radius: 0.25rem;
        border-bottom-right-radius: 0.25rem;
        border-top-left-radius: 0.25rem;
        border-bottom-left-radius: 0.25rem;
    }

    .noactivepage {
        border-top-right-radius: 0.25rem;
        border-bottom-right-radius: 0.25rem;
        border-top-left-radius: 0.25rem;
        border-bottom-left-radius: 0.25rem;
    }
</style>
<script>
    function itpro(Number) {
        Number += '';
        Number = Number.replace(',', ''); Number = Number.replace(',', ''); Number = Number.replace(',', '');
        Number = Number.replace(',', ''); Number = Number.replace(',', ''); Number = Number.replace(',', '');
        x = Number.split('.');
        y = x[0];
        z = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(y))
            y = y.replace(rgx, '$1' + ',' + '$2');
        return y + z;
    }
</script>
<script type="text/javascript">
    function previewFile() {

        var preview = document.querySelector('#<%=ImageNopic.ClientID %>');
        var file = document.querySelector('#<%=fileUploadPic.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            preview.src = reader.result;
        }

        if (file) {
            reader.readAsDataURL(file);

        } else {
            preview.src = "../img/Error/no-photo.png";
        }
    }
</script>
<div class="content-wrapper">
    <!-- Content -->

    <div class="container-xxl flex-grow-1 container-p-y">
        <h4 class="py-3 breadcrumb-wrapper mb-4">مدیریت کتاب ها
        </h4>
        <div class="mb-0">
            <asp:Panel ID="PanelSearch" runat="server">
                <asp:Literal ID="literalForSearch" runat="server"></asp:Literal>
                <form action="Books">
                    <div class="row">
                        <div class="col-12" style="display: block ruby;">
                            <div class="col-5 py-1">
                                <input id="text2" name="txtText" type="text" class="form-control" placeholder="عنوان">
                            </div>
                            <div class="col-5 py-1">
                                <input id="text3" name="txtISBNForSearch" type="text" class="form-control" placeholder="شابک">
                            </div>
                            <div class="col-5 py-1">
                                <button type="submit" class="btn btn-success"><i class="bx bx-search-alt-2"></i>&nbsp;جستجو</button>
                            </div>

                        </div>
                    </div>
                </form>
            </asp:Panel>
            <form action="Books">
                <input id="Text1" name="Value" value="New" hidden="hidden" type="text" />
                <button id="btnAdd" runat="server" class="btn btn-primary my-2"><i class="bx bx-plus"></i>&nbsp;درج جدید</button>
                <br />
                <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>
            </form>
        </div>
        <form runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:MultiView ID="MultiViewMain" runat="server">
                <asp:View ID="ViewMain" runat="server">
                    <!-- Striped Rows -->
                    <div class="card">
                        <div class="table-responsive text-nowrap">
                            <table class="table table-striped">
                                <thead>
                                    <tr>
                                        <th>درج کننده</th>
                                        <th>تصویر کتاب</th>
                                        <th>عنوان</th>
                                        <th>دسته بندی</th>
                                        <th>تعداد موجود</th>
                                        <th>وضعیت</th>
                                        <th>تاریخ درج</th>
                                        <th>عمل‌ها</th>
                                    </tr>
                                </thead>
                                <tbody class="table-border-bottom-0">
                                    <asp:ListView ID="ProuductDefualt" runat="server" DataSourceID="LinqDataSourceNews"
                                        ItemPlaceholderID="brandplacea42">
                                        <LayoutTemplate>
                                            <asp:PlaceHolder ID="brandplacea42" runat="server"></asp:PlaceHolder>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <ul class="list-unstyled users-list m-0 avatar-group d-flex align-items-center">
                                                        <li data-bs-toggle="tooltip" data-popup="tooltip-custom" data-bs-placement="top" class="avatar avatar-xs pull-up" title="" data-bs-original-title="<%#Eval("NameFamily")%>">
                                                            <img src="../img/users/<%#Eval("AdderPic")%>" style="min-width: 26px;" alt="Avatar" class="rounded-circle">
                                                        </li>
                                                        <a style="color: #000;" href="./Manager?Edit=<%# ID2hash(Eval("UserID"))%>"><%#Eval("NameFamily")%></a>
                                                    </ul>
                                                </td>
                                                <td>
                                                    <div class="avatar mr-1 avatar-xl">
                                                        <a class="example-image-link" href='../img/Book/<%#Eval("PicName")%>'
                                                            data-lightbox='MainCategory<%#Eval("ID")%>' data-title='تصویر کتاب با کد <%#Eval("ID") %>'>
                                                            <img class="example-image" onerror="this.src='../img/Error/no-photo.png'"
                                                                src='../img/Books/<%#Eval("PicName")%>' alt="" /></a>
                                                    </div>
                                                </td>
                                                <td><%#Eval("BookTitle")%></td>
                                                <td><%#Eval("CatName")%></td>
                                                <td><%#Eval("Available")%></td>
                                                <%# Status(Eval("ID"))%>
                                                <td><%#((DateTime)Eval("DateInsert")).ToShortDateString()%></td>
                                                <td>
                                                    <div class="dropdown">
                                                        <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                                            <i class="bx bx-dots-vertical-rounded"></i>
                                                        </button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="./Books?Edit=<%# ID2hash(Eval("ID"))%>"><i class="bx bx-edit-alt me-1"></i>مشاهده / ویرایش</a>
                                                            <a class="dropdown-item" href="./Books?Delete=<%# ID2hash(Eval("ID"))%>"><i class="bx bx-trash me-1"></i>حذف</a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <asp:LinqDataSource ID="LinqDataSourceNews" runat="server" OnSelecting="LinqDataSourceNews_Selecting">
                                    </asp:LinqDataSource>
                                </tbody>
                            </table>
                        </div>
                        <div class="col-lg-4" style="padding: 1rem;">
                            <nav aria-label="Page navigation">
                                <asp:DataPager ID="DataPager1" runat="server" class="pagination pagination-info" style="display: flex;" PagedControlID="ProuductDefualt" PageSize="45">
                                    <Fields>
                                        <asp:NumericPagerField CurrentPageLabelCssClass="activepage" NextPageText=""
                                            PreviousPageText="" NumericButtonCssClass="page-link noactivepage" ButtonType="Button" />
                                    </Fields>
                                </asp:DataPager>
                            </nav>
                        </div>
                    </div>
                    <!--/ Striped Rows -->
                    <script>
                        function setUrl() {
                            const urlParams = new URLSearchParams(window.location.search);
                            var txtText = urlParams.get("txtText")
                            var txtISBNForSearch = urlParams.get("txtISBNForSearch")
                            if (txtText) {
                                document.getElementById("text2").value = txtText;
                            }
                            if (txtISBNForSearch) {
                                document.getElementById("text3").value = txtISBNForSearch;
                            }
                        }
                        setUrl();
                        function clearUrlParams() {
                            var currentUrl = window.location.href;
                            var baseUrl = currentUrl.split('?')[0];
                            window.history.replaceState(null, null, baseUrl);
                        }
                        clearUrlParams();
                    </script>
                </asp:View>
                <asp:View ID="ViewFileds" runat="server">
                    <div class="col-12">
                        <div class="card mb-4">
                            <div class="card-body">
                                <div class="row">
                                    <div class="mb-3 col-md-12">
                                        <label for="fileUploadPic" class="form-label">تصویر</label><br />
                                        <asp:Image ID="ImageNopic" runat="server" ImageUrl="../img/Error/no-photo.png"
                                            Style="max-width: 256px" />
                                        <br />
                                        <asp:Button ID="btnResetPic" CssClass="btn btn-danger my-2" OnClick="btnResetPic_Click" runat="server" Text="بازشناسی تصویر کتاب" />
                                        <div class="col-6">
                                            <asp:FileUpload ID="fileUploadPic" accept="image/*" onChange="previewFile()" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <!-- Mapp scripts -->
                                    <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                                        <label class="form-label" for="phone-number-mask">دسته اصلی</label>
                                        <div>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList CssClass="form-select" ID="drpCategory" name="drpBlogGroup"
                                                        runat="server" DataSourceID="LinqDataSourcedrpCategory" DataTextField="Title"
                                                        DataValueField="ID" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:LinqDataSource ID="LinqDataSourcedrpCategory" runat="server" OnSelecting="LinqDataSourcedrpCategory_Selecting">
                                                    </asp:LinqDataSource>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                                        <label class="form-label" for="txtAuthor">نام و نام خانوادگی نویسنده</label>
                                        <div class="col-lg-10 col-9">
                                            <input type="text" id="txtAuthor" runat="server" class="form-control" placeholder="نام و نام خانوادگی نویسنده">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtAuthor"
                                                ErrorMessage="نام نویسنده الزامی است"
                                                CssClass="badge bg-label-danger"
                                                Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="revAuthor" runat="server"
                                                ControlToValidate="txtAuthor"
                                                ErrorMessage="نام نویسنده باید بین ۲ تا ۵۰ حرف باشد"
                                                ValidationExpression="^.{2,50}$"
                                                CssClass="badge bg-label-danger"
                                                Display="Dynamic" />
                                        </div>
                                    </div>

                                    <div class="col-xl-6 col-md-6 col-sm-12">
                                        <div class="form-group row align-items-center">
                                            <label class="form-label" for="txtTedad">تعداد کتاب موجود</label>
                                            <div class="col-lg-10 col-9">
                                                <input type="text" id="txtTedad" runat="server" class="form-control" value="1" placeholder="تعداد کتاب موجود">

                                                <asp:RangeValidator runat="server" ID="RangeValidator2" ControlToValidate="txtTedad" MinimumValue="1" MaximumValue="999999999" Type="Integer" ErrorMessage="لطفاً مقداری بین 1 و 999999999 وارد کنید." CssClass="badge bg-label-danger"></asp:RangeValidator>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-xl-6 col-md-6 col-sm-12">
                                        <div class="form-group row align-items-center">
                                            <label class="form-label" for="txtTitle">عنوان کتاب</label>
                                            <div class="col-lg-10 col-9">
                                                <input type="text" id="txtTitle" maxlength="100" runat="server" class="form-control" placeholder="عنوان کتاب">
                                                <asp:RequiredFieldValidator ID="rfvTitle" runat="server"
                                                    ControlToValidate="txtTitle"
                                                    ErrorMessage="عنوان کتاب الزامی است"
                                                    CssClass="badge bg-label-danger"
                                                    Display="Dynamic" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ControlToValidate="txtTitle"
                                                    ErrorMessage="عنوان کتاب باید بین 2 تا 100 حرف باشد"
                                                    ValidationExpression="^.{2,100}$"
                                                    CssClass="badge bg-label-danger"
                                                    Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group row align-items-center">
                                            <label class="form-label" for="txtTranslatorName">نام مترجم</label>

                                            <div class="col-lg-10 col-9">
                                                <input type="text" id="txtTranslatorName" runat="server" class="form-control" placeholder="نام مترجم">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                    ControlToValidate="txtTranslatorName"
                                                    ErrorMessage="نام مترجم باید بین 2 تا 50 حرف باشد"
                                                    ValidationExpression="^.{2,50}$"
                                                    CssClass="badge bg-label-danger"
                                                    Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group row align-items-center">
                                            <label class="form-label" for="txtPublisher">ناشر</label>
                                            <div class="col-lg-10 col-9">
                                                <input type="text" id="txtPublisher" runat="server" class="form-control" placeholder="ناشر">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtPublisher"
                                                    ErrorMessage="نام ناشر الزامی است"
                                                    CssClass="badge bg-label-danger"
                                                    Display="Dynamic" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                    ControlToValidate="txtPublisher"
                                                    ErrorMessage="نام ناشر باید بین 2 تا 50 حرف باشد"
                                                    ValidationExpression="^.{2,50}$"
                                                    CssClass="badge bg-label-danger"
                                                    Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group row align-items-center">
                                            <label class="form-label" for="txtISBN">شابک</label>

                                            <div class="col-lg-10 col-9">
                                                <input type="text" id="txtIsbn" runat="server" class="form-control" placeholder="شابک">
                                                <asp:RequiredFieldValidator ID="rfvIsbn" runat="server"
                                                    ControlToValidate="txtIsbn"
                                                    ErrorMessage="شابک الزامی است"
                                                    CssClass="badge bg-label-danger"
                                                    Display="Dynamic" />
                                                <asp:RegularExpressionValidator ID="revIsbn" runat="server"
                                                    ControlToValidate="txtIsbn"
                                                    ErrorMessage="فرمت شابک نامعتبر است"
                                                    ValidationExpression="^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$"
                                                    CssClass="badge bg-label-danger"
                                                    Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-12 col-md-6 col-sm-12 mb-4 mb">
                                        <label class="form-label" for="custom-delimiter-mask">وضعیت</label>
                                        <div class="form-check form-switch mb-2">
                                            <input class="form-check-input" runat="server" type="checkbox" id="chkStatus" checked>
                                            <label class="form-check-label" for="chkStatus">وضعیت</label>
                                        </div>
                                    </div>
                                    <div class="col-xl-12 col-md-6 col-sm-12 mb-4 mb">
                                        <label class="form-label" for="custom-delimiter-mask">ویژه</label>
                                        <div class="form-check form-switch mb-2">
                                            <input class="form-check-input" runat="server" type="checkbox" id="chkSpecial" checked>
                                            <label class="form-check-label" for="chkSpecial">ویژه</label>
                                        </div>
                                        <!-- Custom Delimiters -->
                                        <hr class="my-4 mx-n4">
                                        <div class="pt-4">
                                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary me-sm-3 me-1" runat="server" Text="ثبت" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" CssClass="btn btn-label-secondary" runat="server" Text="بازگشت" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </form>
    </div>
    <!-- / Content -->
    <div class="content-backdrop fade"></div>
</div>
