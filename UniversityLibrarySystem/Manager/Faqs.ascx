<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Faqs.ascx.cs" Inherits="UniversityLibrarySystem.Manager.Faqs" %>
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
        <h4 class="py-3 breadcrumb-wrapper mb-4">سوالات متداول</h4>
        <div class="mb-0">
            <asp:Panel ID="PanelSearch" runat="server">
                <form action="Faqs">
                    <div class="row">
                        <div class="col-12" style="display: block ruby;">
                            <div class="col-6">
                                <input id="text2" name="txtText" type="text" class="form-control" placeholder="عنوان">
                            </div>
                            <div class="col-5">
                                <button type="submit" class="btn btn-success"><i class="bx bx-search-alt-2"></i>&nbsp;جستجو</button>
                            </div>
                        </div>
                    </div>
                </form>
            </asp:Panel>
            <form action="Faqs">
                <input id="Text1" name="Value" value="New" hidden="hidden" type="text" />
                <button id="btnAdd" runat="server" class="btn btn-primary my-2"><i class="bx bx-plus"></i>&nbsp;درج جدید</button>
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
                            <table class="table table-striped">
                                <thead>
                                    <tr>
                                        <th>درج کننده</th>
                                        <th>سوال</th>
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
                                                            <img src="../img/users/<%#Eval("AdderPic")%>" alt="Avatar" class="rounded-circle">
                                                        </li>
                                                        <a style="color:#000;" href="./Manager?Edit=<%# ID2hash(Eval("UserID"))%>"><%#Eval("NameFamily")%></a>
                                                    </ul>
                                                </td>
                                                <td><%#Eval("Question")%></td>
                                                <%# Status(Eval("ID"))%>
                                                <td><%#((DateTime)Eval("DateInsert")).ToShortDateString()%></td>
                                                <td>
                                                    <div class="dropdown">
                                                        <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                                            <i class="bx bx-dots-vertical-rounded"></i>
                                                        </button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="./Faqs?Edit=<%# ID2hash(Eval("ID"))%>"><i class="bx bx-edit-alt me-1"></i>مشاهده / ویرایش</a>
                                                            <a class="dropdown-item" href="./Faqs?Delete=<%# ID2hash(Eval("ID"))%>"><i class="bx bx-trash me-1"></i>حذف</a>
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
                                    <div class="col-xl-12 col-md-12 col-sm-12 mb-4 mb">
                                        <label class="form-label" for="custom-delimiter-mask">عنوان سوال</label>
                                        <div class="mb-3">
                                            <input type="text" class="form-control" id="txtQuestion" runat="server" validationgroup="Valid" placeholder="سوال" />
                                        </div>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtQuestion" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator2">این قسمت الزامی است</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-xl-12 col-md-12 col-sm-12 mb-4">
                                        <label class="form-label" for="creditCardMask">پاسخ</label>
                                        <div class="mb-3">
                                            <textarea class="form-control" id="txtAnswer" runat="server" validationgroup="Valid" rows="5" placeholder="پاسخ"></textarea>
                                        </div>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAnswer" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator1">این قسمت الزامی است</asp:RequiredFieldValidator>
                                        <!-- Mapp scripts -->
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
