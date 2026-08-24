<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginReport.ascx.cs" Inherits="UniversityLibrarySystem.Manager.LoginReport" %>
<form runat="server">
    <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
    <div class="row m-2">
        <h4>گزارش ورود</h4>
        <div>
            <a href="ChangePassword" class="btn btn-primary me-1">تغییر کلمه عبور</a>
            <asp:Button ID="btnDeleteAllDevice" runat="server" Text="خروج از تمام دستگاه‌ها" CssClass="btn btn-danger mx-1" OnClick="btnDeleteAllDevice_Click" />
            <div class="table-responsive card text-nowrap my-3">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>اطلاعات دستگاه</th>
                            <th>تاریخ اولین ورود</th>
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
                                    <td><%#Eval("BrowserName")%></td>
                                    <td dir="ltr"><%#Eval("DateTime")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:LinqDataSource ID="LinqDataSourceNews" runat="server" OnSelecting="LinqDataSourceNews_Selecting">
                        </asp:LinqDataSource>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <script>window.history.replaceState(null, null, window.location.href.split('?')[0]);</script>
</form>
