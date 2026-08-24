<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager.ascx.cs" Inherits="UniversityLibrarySystem.Manager.Manager" %>
<style>
    #<%=ImageNopic.ClientID %> {
        border-radius: 0.25rem;
    }
</style>

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
            preview.src = "../img/users/no-photo.png";
        }
    }
</script>
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
<div class="content-wrapper">
    <!-- Content -->

    <div class="container-xxl flex-grow-1 container-p-y">
        <h4 class="py-3 breadcrumb-wrapper mb-4">مدیریت مدیران
        </h4>
        <div class="mb-0 py-1">
            <asp:Panel ID="PanelSearch" runat="server">
                <asp:Literal ID="literalForSearch" runat="server"></asp:Literal>
                <form action="Manager">
                    <div class="row">
                        <div class="col-12" style="display: block ruby;">
                            <div class="col-5 py-1">
                                <input id="txtName" name="txtName" type="text" class="form-control" placeholder="نام و نام خانوادگی">
                            </div>
                            <div class="col-5 py-1">
                                <button type="submit" class="btn btn-success"><i class="bx bx-search-alt-2"></i>&nbsp;جستجو</button>
                            </div>

                        </div>
                    </div>
                </form>
            </asp:Panel>
            <form action="Manager">
                <input id="Text1" name="Value" value="New" hidden="hidden" type="text" />
                <button id="btnAdd" runat="server" class="btn btn-primary my-2"><i class="bx bx-plus"></i>&nbsp;درج جدید</button>
                <br />
                <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>
            </form>
        </div>
        <form runat="server">
            <asp:MultiView ID="MultiViewMain" runat="server">
                <asp:View ID="ViewMain" runat="server">
                    <!-- Striped Rows -->
                    <div class="card">
                        <div class="table-responsive text-nowrap">
                            <table class="table table-striped">
                                <thead>
                                    <tr>
                                        <th>درج کننده</th>
                                        <th>تصویر پروفایل</th>
                                        <th>نام و نام خانوادگی</th>
                                        <th>موبایل</th>
                                        <th>وضعیت</th>
                                        <th>تاریخ درج</th>
                                        <th>عمل‌ها</th>
                                    </tr>
                                </thead>
                                <tbody class="table-border-bottom-0 text-nowrap">
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
                                                    <div class="avatar avatar-xl">
                                                        <a class="example-image-link" href='../img/users/<%#Eval("Pic")%>'
                                                            data-lightbox='MainCategory<%#Eval("ID")%>' data-title='تصویر مشاور <%#Eval("FullName")%> با کد <%#Eval("ID") %>'>
                                                            <img class="example-image rounded-circle" onerror="this.src='../img/users/no-photo.png'"
                                                                src='../img/users/<%#Eval("Pic")%>' alt="<%#Eval("FullName")%>" /></a>
                                                    </div>
                                                </td>
                                                <td><%#Eval("FullName")%></td>
                                                <td><%#Eval("Mobile")%></td>
                                                <%#Status(Eval("ID"))%>
                                                <td><%#((DateTime)Eval("DateInsert")).ToShortDateString()%></td>
                                                <td>
                                                    <div class="dropdown">
                                                        <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                                            <i class="bx bx-dots-vertical-rounded"></i>
                                                        </button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="./Manager?Edit=<%# ID2hash(Eval("ID"))%>"><i class="bx bx-edit-alt me-1"></i>ویرایش</a>
                                                            <a class="dropdown-item" href="./Manager?Reset=<%# ID2hash(Eval("ID"))%>"><i class="bx bx-lock-open-alt me-1"></i>لغو قفل حساب کاربری</a>
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
                                <asp:DataPager ID="DataPager1" runat="server" class="pagination pagination-info" style="display: flex;" PagedControlID="ProuductDefualt" PageSize="12">
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
                            var txtText = urlParams.get("txtName")
                            if (txtText) {
                                document.getElementById("txtName").value = txtText;
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
                                <asp:Label ID="lblIsOnline" CssClass="badge" runat="server" Text="Label"></asp:Label>
                                <div class="row">
                                    <asp:Image ID="ImageNopic" runat="server" Height="225px" ImageUrl="../img/users/no-photo.png"
                                        Width="225px" />
                                    <br />
                                    <hr />
                                    <div class="col-xl-12 col-md-12 col-sm-12 mb-4">
                                        <label class="form-label" for="delimiter-mask">تصویر</label>
                                        <div class="col-12">
                                            <asp:FileUpload ID="fileUploadPic" accept="image/*" onChange="previewFile()" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <!-- Credit Card -->
                                    <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                                        <label class="form-label" for="creditCardMask">نام و نام خانوادگی</label>
                                        <div class="input-group input-group-merge">
                                            <input type="text" runat="server" id="txtFullName" validationgroup="Valid" name="creditCardMask" class="form-control credit-card-mask text-start" dir="ltr" placeholder="نام">
                                            <span class="input-group-text cursor-pointer p-1" id="creditCardMask2"><span class="card-type"></span></span>
                                        </div>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFullName" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator2">این قسمت الزامی است</asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                                        <label class="form-label" for="date-mask">نام کاربری</label>
                                        <input type="text" id="txtUserName" runat="server" validationgroup="Valid" class="form-control date-mask text-start" dir="ltr" placeholder="نام کاربری">
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserName" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="UserNameRequired">این قسمت الزامی است</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rgeMobileNumber" runat="server" ValidationGroup="LoginMain"
                                            ControlToValidate="txtUserName" SetFocusOnError="True" CssClass="badge bg-label-danger" Display="Dynamic" ErrorMessage="نام کاربری باید فقط شامل اعداد باشد" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>

                                    </div>
                                    <!-- Time -->
                                    <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                                        <label class="form-label" for="time-mask">کلمه عبور</label>
                                        <input type="password" id="txtPassword" runat="server" validationgroup="Valid" class="form-control time-mask text-start" dir="ltr" placeholder="کلمه عبور">
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator4" Display="Dynamic">این قسمت الزامی است</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword" ValidationExpression="^[A-Za-z\d@$!%*?&]{8,}$" ErrorMessage="رمز عبور باید حداقل ۸ کاراکتر باشد و می‌تواند شامل حروف لاتین یا اعداد باشد"
                                            Display="Dynamic" CssClass="badge bg-label-danger" ValidationGroup="Valid"></asp:RegularExpressionValidator>
                                    </div>
                                    <!-- Numeral Formatting -->
                                    <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                                        <label class="form-label" for="numeral-mask">شماره موبایل</label>
                                        <input type="text" id="txtMobile" validationgroup="Valid" runat="server" class="form-control numeral-mask text-start" dir="ltr" placeholder="شماره موبایل">
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator1">این قسمت الزامی است</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Valid"
                                            ControlToValidate="txtMobile" SetFocusOnError="True" CssClass="badge bg-label-danger" Display="Dynamic" ErrorMessage="شماره موبایل به درستی وارد نشده است"
                                            ValidationExpression="09(0(\d)|1(\d)|2(\d)|3(\d)|(9(\d)))\d{7}$"></asp:RegularExpressionValidator>
                                    </div>
                                    <!-- Blocks -->
                                    <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                                        <label class="form-label" for="block-mask">ایمیل</label>
                                        <input type="text" id="txtEmail" runat="server" validationgroup="Valid" class="form-control block-mask text-start" dir="ltr" placeholder="ایمیل">
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator5" Display="Dynamic">این قسمت الزامی است</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="Valid"
                                            ControlToValidate="txtEmail" SetFocusOnError="True" CssClass="badge bg-label-danger" Display="Dynamic" ErrorMessage="ایمیل به درستی وارد نشده است"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
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
                                        <asp:Button ID="btnDelete" ValidationGroup="Valid" CssClass="btn btn-danger me-sm-3 me-1" runat="server" Text="حذف حساب" OnClick="btnDelete_Click" />
                                        <a href="./Manager" class="btn btn-label-secondary">بازگشت</a>
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
