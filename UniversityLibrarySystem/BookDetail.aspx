<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="UniversityLibrarySystem.BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <asp:MultiView ID="MultiViewMain" ActiveViewIndex="0" runat="server">
            <asp:View runat="server">
                <div class="breadcrumb-items">
                    <div class="row">
                        <ul class="breadcrumb">
                            <li>
                                <a href="/" class="breadcrumb-link">صفحه اصلی</a>
                            </li>
                            <li class="chevron"><span class="fa fa-chevron-left"></span></li>
                            <li>
                                <%=BottomNav() %>
                        </ul>
                    </div>
                </div>

                <div class="book-details">
                    <div class="container">
                        <div class="row">
                            <div class="col-xl-12">
                                <div class="row">
                                    <div class="col-xl-12">
                                        <asp:Literal ID="LiteralMSG" runat="server"></asp:Literal>
                                        <%=BookShortDetail() %>
                                        <asp:MultiView ID="MultiView1" runat="server">
                                            <asp:View ID="NeedLogin" runat="server">
                                                <a href="/Login" class="btn btn-success">ورود به سیستم</a>
                                            </asp:View>
                                            <asp:View ID="DeleteReserveView" runat="server">
                                                <asp:Button ID="btnDelReserve" runat="server" CssClass="btn btn-danger" OnClick="btnDelReserve_Click" Text="حذف رزرو" />
                                            </asp:View>
                                            <asp:View ID="AdminView" runat="server">
                                                <a href="/Manager/Books?Edit=<%=ID2hash() %>" class="btn btn-success">ویرایش کتاب</a>
                                            </asp:View>
                                            <asp:View ID="UnavailableView" runat="server">
                                                <p class="text-danger">کتاب مورد نظر موجود نمی باشد</p>
                                            </asp:View>
                                            <asp:View ID="ReserveView" runat="server">
                                                <asp:Button ID="btnReserve" Text="رزرو کتاب" CssClass="btn btn-success" OnClick="btnReserve_Click" runat="server"></asp:Button>
                                            </asp:View>
                                            <asp:View ID="ReservedView" runat="server">
                                                <a href="/Dash" class="btn btn-success">مشاهده رزروها</a>
                                            </asp:View>
                                        </asp:MultiView>
                                        <asp:Label ID="lblMessage" runat="server" Text="" ViewStateMode="Disabled"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%=BookLongDetail() %>


                <section class="related-books section-bg">
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-xl-7 col-lg-8">
                                <div class="section-tittle text-center mb-55">
                                    <h2>کتاب های مرتبط</h2>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xl-12">
                                    <div class="related-books-box">
                                        <%=NewBook() %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <script>
                    function clearUrlParams() {
                        window.history.replaceState(null, null, window.location.href.split('?')[0]);
                    }

                    clearUrlParams();
                </script>
            </asp:View>
            <asp:View runat="server">
                <style>
                    .lead {
                        font-size: 18pt;
                    }

                    main {
                        text-align: center;
                    }
                </style>
                <main class="m-5 text-center">
                    <h1 class="display-1">404</h1>
                    <p class="lead">کتاب مورد نظر یافت نشد!</p>
                    <p>متاسفانه کتابی که به دنبال آن هستید یافت نشد یا غیرفعال شده است.</p>
                    <a onclick="history.go(-1)" class="btn header-btn">بازگشت</a>
                </main>
            </asp:View>
        </asp:MultiView>
    </form>
</asp:Content>
