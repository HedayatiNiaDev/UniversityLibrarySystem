<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Books.ascx.cs" Inherits="UniversityLibrarySystem.Student.Books" %>
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
        <h4 class="py-3 breadcrumb-wrapper mb-4" id="titlePage">مدیریت کتاب ها
        </h4>
        <div class="mb-0">
            <asp:Panel ID="PanelSearch" Visible="true" runat="server">
                <form action="Books">
                    <div class="row">
                        <div class="col-12" style="display: block ruby;">
                            <div class="col-md-6 m-1 col-12">
                                <input id="txtText" name="txtText" type="text" class="form-control" placeholder="عنوان">
                            </div>
                            <div class="col-md-5 m-1 col-12">
                                <button type="submit" class="btn btn-success"><i class="bx bx-search-alt-2"></i>&nbsp;جستجو</button>
                            </div>
                            <div class="col-lg-5 m-1 col-12">
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
                </form>
            </asp:Panel>
            <form action="Books">
                <input id="Text1" name="Value" value="New" hidden="hidden" type="text" />
                <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>
            </form>
        </div>
        <form runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <!-- Striped Rows -->
            <div class="card">
                <div class="table-responsive text-nowrap">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th>تصویر کتاب</th>
                                <th>عنوان</th>
                                <th>دسته بندی</th>
                                <th>وضعیت رزرو</th>
                                <th>تاریخ شروع</th>
                                <th>تاریخ پایان</th>
                                <th>مبلغ قابل پرداخت</th>
                                <th>مشاهده کتاب</th>
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
                                            <div class="avatar mr-1 avatar-xl">
                                                <img class="example-image" onerror="this.src='../img/Error/no-photo.png'"
                                                    src='../img/Books/<%#Eval("PicName")%>' alt="" /></a>
                                            </div>
                                        </td>
                                        <td><%#Eval("BookTitle")%></td>
                                        <td><%#Eval("CatName")%></td>
                                        <td><%#Eval("StatusHtml") %></td>
                                        <td><%#Eval("ResTempStartDate")%></td>
                                        <td><%#Eval("ResTempEndDate")%></td>
                                        <td><%#Eval("BookFine")%></td>
                                        <td>
                                            <a class="btn btn-primary" href="../BookDetail-<%#Eval("BookID")%>-<%#Uri.EscapeDataString(Eval("BookTitle").ToString())%>"></i>مشاهده</a>
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
        </form>
    </div>
    <!-- / Content -->

    <div class="content-backdrop fade"></div>
    <script>
        function setUrl() {
            const urlParams = new URLSearchParams(window.location.search);
            var txtText = urlParams.get("txtText")
            if (txtText) {
                document.getElementById("txtText").value = txtText;
            }
            const mode = urlParams.get("Mode");
            if (mode !== null) {
                const dropdown = document.getElementById('drpMode');
                dropdown.value = mode;
                if (mode != 0)
                    document.getElementById("titlePage").innerText += " (" + dropdown.options[mode].text + ")";
                if (mode == 3) {
                    document.getElementById('btnGoFine').style.display = 'none';
                }
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
</div>
