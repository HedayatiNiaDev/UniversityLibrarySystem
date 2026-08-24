<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="UniversityLibrarySystem.Manager.Categories" %>
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
<script src="../PAPAssets/js/lightbox-plus-jquery.min.js"></script>
<link href="../PAPAssets/css/lightbox.min.css" rel="stylesheet" />
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
        <h4 class="py-3 breadcrumb-wrapper mb-4">مدیریت دسته بندی ها
        </h4>
        <div class="mb-0 py-1">
            <form action="Categories">
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
                    <script>
                        function clearUrlParams() {
                            var currentUrl = window.location.href;
                            var baseUrl = currentUrl.split('?')[0];
                            window.history.replaceState(null, null, baseUrl);
                        }
                        clearUrlParams();
                    </script>
                    <!-- Striped Rows -->
                    <div class="card">
                        <div class="table-responsive text-nowrap">
                            <table class="table table-striped text-nowrap">
                                <thead>
                                    <tr>
                                        <th>درج کننده</th>
                                        <th>تصویر</th>
                                        <th>عنوان</th>
                                        <th>وضعیت</th>
                                        <th>تعداد کتاب ها</th>
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
                                                        <a style="color:#000;" href="./Manager?Edit=<%# ID2hash(Eval("UserID"))%>"><%#Eval("NameFamily")%></a>
                                                    </ul>
                                                </td>
                                                <td>
                                                    <div class="avatar avatar-xl">
                                                        <a class="example-image-link" href='../img/Categories/<%#Eval("PicName")%>'
                                                            data-lightbox='MainCategory<%#Eval("ID")%>' data-title='تصویر دسته بندی <%#Eval("Title")%> با کد <%#Eval("ID") %>'>
                                                            <img class="example-image" onerror="this.src='../img/Error/no-photo.png'"
                                                                src='../img/Categories/<%#Eval("PicName")%>' alt="<%#Eval("Title")%>" /></a>
                                                    </div>
                                                </td>
                                                <td><%#Eval("Title")%></td>
                                                <%# Status(Eval("ID"))%>
                                                <%# BooksCounter(Eval("ID")) %>
                                                <td><%#((DateTime)Eval("DateInsert")).ToShortDateString()%></td>
                                                <td>
                                                    <div class="dropdown">
                                                        <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                                            <i class="bx bx-dots-vertical-rounded"></i>
                                                        </button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="./Categories?Edit=<%# ID2hash(Eval("ID"))%>"><i class="bx bx-edit-alt me-1"></i>مشاهده / ویرایش</a>
                                                            <a class="dropdown-item" href="./Categories?Delete=<%# ID2hash(Eval("ID"))%>"><i class="bx bx-trash me-1"></i>حذف</a>
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
                        <hr />
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
                </asp:View>
                <asp:View ID="ViewFileds" runat="server">
                    <div class="col-12">
                        <div class="card mb-4">
                            <div class="card-body">
                                <div class="row">
                                    <!-- Mapp scripts -->
                                    <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                                        <label class="form-label" for="creditCardMask">عنوان</label>
                                        <div class="input-group input-group-merge">
                                            <input type="text" runat="server" id="txtTitle" validationgroup="Valid" name="creditCardMask" class="form-control credit-card-mask text-start" dir="ltr" maxlength="50" placeholder="عنوان">
                                            <span class="input-group-text cursor-pointer p-1" id="creditCardMask2"><span class="card-type"></span></span>
                                        </div>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator3">این قسمت الزامی است</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                            ControlToValidate="txtTitle" ValidationGroup="Valid"
                                            ErrorMessage="نام دسته بندی باید بین 2 تا 100 حرف باشد"
                                            ValidationExpression="^.{2,100}$"
                                            CssClass="badge bg-label-danger"
                                            Display="Dynamic" />
                                        <!-- Mapp scripts -->
                                    </div>
                                    <!-- Credit Card -->
                                    <div class="mb-3 col-md-12">
                                        <label for="txtAddress" class="form-label">تصویر</label>
                                        <div class="col-6">
                                            <asp:FileUpload ID="fileUploadPic" accept="image/*" onChange="previewFile()" runat="server" class="form-control" />
                                        </div>
                                        <br />
                                        <hr />
                                        <asp:Image ID="ImageNopic" runat="server" Height="225px" ImageUrl="../img/Error/no-photo.png"
                                            Width="225px" />
                                        <br />
                                        <asp:Button ID="btnResetPic" CssClass="btn btn-danger my-2 mx-1" Visible="false" runat="server" Text="بازشناسی تصویر دسته بندی" OnClick="btnResetPic_Click"/>
                                    </div>
                                    <div class="col-xl-4 col-md-6 col-sm-12 mb-4 mb">
                                        <label class="form-label" for="custom-delimiter-mask">وضعیت</label>
                                        <div class="form-check form-switch mb-2">
                                            <input class="form-check-input" runat="server" type="checkbox" id="chkStatus" checked>
                                            <label class="form-check-label" for="chkStatus">وضعیت</label>
                                        </div>
                                    </div>
                                    <!-- Custom Delimiters -->
                                    <hr class="my-4 mx-n4">
                                    <div class="pt-4">
                                        <asp:Button ID="btnSubmit" ValidationGroup="Valid" CssClass="btn btn-primary me-sm-3 me-1" runat="server" Text="ثبت" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-label-secondary" runat="server" Text="بازگشت" OnClick="btnCancel_Click" />
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
