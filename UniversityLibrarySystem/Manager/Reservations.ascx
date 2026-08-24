<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reservations.ascx.cs" Inherits="UniversityLibrarySystem.Manager.Reservations" %>
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
        <h4 class="py-3 breadcrumb-wrapper mb-4" id="titlePage">مدیریت رزرو ها
        </h4>
        <div class="mb-0">
            <asp:Panel ID="PanelSearch" runat="server" CssClass="mb-2" Visible="true">
                <form action="Reservations">
                    <div class="row">
                        <div class="col-12" style="display: block ruby;">
                            <div class="col-lg-6 col-md-12 col-12 m-1">
                                <input id="text2" name="txtText" type="text" class="form-control" placeholder="عنوان">
                            </div>
                            <div class="col-lg-5 col-md-12 col-12 m-1">
                                <button id="btnSubmit" type="submit" class="btn btn-success"><i class="bx bx-search-alt-2"></i>&nbsp;جستجو</button>
                            </div>
                            <div class="col-lg-6 col-md-12 col-12 m-1">
                                <input id="textUID" name="uid" type="text" class="form-control" placeholder="نام کاربری">
                            </div>
                            <div class="col-lg-5 col-md-12 col-12 m-1">
                                <select class="form-select" id="drpMode" name="Mode" aria-label="Default select example">
                                    <option value="0">همه موارد</option>
                                    <option value="1"><%=Classes.ReserveStatus.ReserveStatusToText(1) %></option>
                                    <option value="2"><%=Classes.ReserveStatus.ReserveStatusToText(2) %></option>
                                    <option value="3"><%=Classes.ReserveStatus.ReserveStatusToText(3) %></option>
                                    <option value="4"><%=Classes.ReserveStatus.ReserveStatusToText(4) %></option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>
                </form>
                <a id="btnAdd" runat="server" class="btn btn-primary my-2" href="./Reservations?Value=New"><i class="bx bx-plus"></i>&nbsp;درج جدید</a>
            </asp:Panel>
        </div>
        <form runat="server">
            <asp:MultiView ID="multiView" ActiveViewIndex="0" runat="server">
                <asp:View runat="server">


                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <!-- Striped Rows -->
                    <div class="card">
                        <div class="table-responsive text-nowrap">
                            <table class="table table-striped">
                                <thead>
                                    <tr>
                                        <th>نام دانشجو</th>
                                        <th>نام کاربری دانشجو</th>
                                        <th>وضعیت دانشجو</th>
                                        <th>تصویر کتاب</th>
                                        <th>عنوان</th>
                                        <th>دسته بندی</th>
                                        <th>وضعیت رزرو</th>
                                        <th>تمدید</th>
                                        <th>عمل ها</th>
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
                                                        <a style="color: #000;" href="./Students?Edit=<%#Eval("UserNameLink")%>"><%#Eval("NameFamily")%></a>
                                                    </ul>
                                                </td>
                                                <td><%#Eval("UserName")%></td>
                                                <%#Eval("UserStatus")%>
                                                <td>
                                                    <div class="avatar mr-1 avatar-xl">
                                                        <img class="example-image" onerror="this.src='../img/Error/no-photo.png'"
                                                            src='../img/Books/<%#Eval("PicName")%>' alt="" /></a>
                                                    </div>
                                                </td>
                                                <td><%#Eval("BookTitle")%></td>
                                                <td><%#Eval("CatName")%></td>
                                                <td><%#Eval("StatusHtml") %></td>
                                                <td><%#Eval("Renewal") %></td>
                                                <td>
                                                    <%#Eval("ButtonsHtml")%>
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
                    <script>
                        function setUrl() {
                            const urlParams = new URLSearchParams(window.location.search);
                            var txtText = urlParams.get("txtText")
                            if (txtText) {
                                document.getElementById("text2").value = txtText;
                            }
                            var txtU = urlParams.get("uid")
                            if (txtU) {
                                document.getElementById("textUID").value = txtU;
                            }
                            const mode = urlParams.get("Mode");
                            console.log(mode);
                            if (mode !== null) {
                                const dropdown = document.getElementById('drpMode');
                                dropdown.value = mode;
                                if (mode != 0)
                                    document.getElementById("titlePage").innerText += " (" + dropdown.options[mode].text + ")";
                            }
                        }
                        function clearUrlParams() {
                            var currentUrl = window.location.href;
                            var baseUrl = currentUrl.split('?')[0];
                            window.history.replaceState(null, null, baseUrl);
                        }
                        setUrl();
                        clearUrlParams();
                    </script>
                </asp:View>
                <asp:View runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <!-- Site Name -->
                                <style>
                                    .big-label {
                                        font-size: 16px; /* اندازه فونت را به دلخواه تنظیم کنید */
                                        font-weight: bold;
                                    }

                                    .small-label {
                                        font-size: 16px; /* اندازه فونت را به دلخواه تنظیم کنید */
                                    }
                                </style>
                                <div class="col-12">
                                    <asp:Literal ID="literalView" runat="server"></asp:Literal>
                                </div>
                                <div class="col-12">
                                    <asp:Image ID="ImageNopic" runat="server" ImageUrl="../img/Error/no-photo.png"
                                        Style="max-width: 256px" />
                                </div>
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="lblBookName" Text="نام کتاب:" CssClass="form-label big-label" runat="server" />
                                    <asp:TextBox ID="txtBookName" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="lblStudentName" Text="نام دانشجو:" CssClass="form-label big-label" runat="server" />
                                    <asp:TextBox ID="txtStudentName" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="lblStudentUserName" Text="نام کاربری دانشجو:" CssClass="form-label big-label" runat="server" />
                                    <asp:TextBox ID="txtStudentUserName" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="lblStudentStatus" Text="وضعیت دانشجو:" CssClass="form-label big-label" runat="server" />
                                    <asp:TextBox ID="txtStudentStatus" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    <asp:Label ID="lblStartDate" Text="تاریخ شروع:" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtStartDate" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="lblEndDate" Text="تاریخ پایان:" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtEndDate" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-6" id="DivDeliveryDate" runat="server">
                                    <asp:Label ID="lblDeliveryDate" Text="تاریخ تحویل:" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtDeliveryDate" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="lblStatus" Text="وضعیت:" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtStatus" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-6" id="DivRenewal" runat="server">
                                    <asp:Label ID="lblRenewal" Text="تعداد تمدید:" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtRenewal" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="lblISBN" Text="شابک:" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtISBN" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6" id="DivOrderCode" runat="server">
                                    <asp:Label ID="lblOrderCode" Text="توضیحات:" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtOrderCode" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                </div>

                                <div class="pt-4">
                                    <asp:MultiView runat="server" ID="multiButton">
                                        <asp:View runat="server" ID="tempres">

                                            <asp:TextBox ID="txtCustomCode" runat="server" placeholder="توضیحات" class="form-control w-50 mb-1"></asp:TextBox>
                                            <asp:Button ID="btnRes" runat="server" CssClass="btn btn-success" Text="امانت کتاب" OnClick="btnRes_Click" />
                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="حذف رزرو موقت" OnClick="btnDelete_Click" />
                                        </asp:View>
                                        <asp:View runat="server" ID="res">
                                            <asp:Button ID="btnDel" Text="تحویل" OnClick="btnDel_Click" CssClass="btn btn-success" runat="server" />
                                            <asp:Button ID="btnRen" Text="تمدید" OnClick="btnRen_Click" CssClass="btn btn-primary" runat="server" />
                                        </asp:View>
                                        <asp:View runat="server" ID="fine">
                                            <asp:Label ID="lblFine" Text="مبلغ بدهی:" CssClass="form-label big-label" runat="server" /><br />
                                            <asp:Button ID="btnPaid" Text="پرداخت و تحویل" OnClick="btnDel_Click" CssClass="btn btn-success" runat="server" />
                                        </asp:View>
                                    </asp:MultiView>
                                    <a href="./Reservations" class="btn btn-label-secondary">بازگشت</a>
                                </div>
                            </div>
                        </div>
                    </div>


                </asp:View>
                <asp:View ID="New" runat="server">
                    <div class="card">
                        <div class="card-body">
                            <div class="row p-1">
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="Label1" Text="نام کاربری:" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtUsernameNew" CssClass="form-control" runat="server" ValidationGroup="New"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server"
                                        ControlToValidate="txtUsernameNew"
                                        ErrorMessage="نام کاربری الزامی است"
                                        CssClass="badge bg-label-danger"
                                        Display="Dynamic"
                                        ValidationGroup="New" />
                                </div>
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="Label2" Text="شابک:" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtISBNNew" CssClass="form-control" runat="server" ValidationGroup="New"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvIsbn" runat="server"
                                        ControlToValidate="txtISBNNew"
                                        ErrorMessage="شابک الزامی است"
                                        CssClass="badge bg-label-danger"
                                        Display="Dynamic"
                                        ValidationGroup="New" />
                                    <asp:RegularExpressionValidator ID="revIsbn" runat="server"
                                        ControlToValidate="txtISBNNew"
                                        ErrorMessage="فرمت شابک نامعتبر است"
                                        ValidationExpression="^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$"
                                        CssClass="badge bg-label-danger"
                                        Display="Dynamic"
                                        ValidationGroup="New" />
                                </div>
                                <div class="col-12 col-md-6">
                                    <asp:Label ID="Label3" Text="توضیحات" CssClass="form-label small-label" runat="server" />
                                    <asp:TextBox ID="txtDetail" CssClass="form-control" runat="server" ValidationGroup="New"></asp:TextBox>
                                </div>
                                <hr class="mt-3" />
                                <div class="my-3">
                                    <asp:Button ID="btnNew" runat="server" CssClass="btn btn-primary me-1" OnClick="btnNew_Click" Text="ثبت" ValidationGroup="New" />
                                    <a href="./Reservations" class="btn btn-label-secondary mx-1">بازگشت</a>
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
