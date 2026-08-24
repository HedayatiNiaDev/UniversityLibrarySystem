<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="UniversityLibrarySystem.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container svg {
            display: none;
        }

        .active-category {
            background-color: #f0f0f0;
            border-radius: 30px;
            padding: 7px;
        }

            .active-category svg {
                display: inline-block;
            }
    </style>
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
                        <%=GetLinksTitle() %>
                    </ul>
                </div>
            </div>
            <asp:ScriptManager runat="server" />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="listing-area pt-50 pb-50">
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-4 col-md-12 col-12">
                                    <div class="category-listing mb-50">
                                        <div class="single-listing">
                                            <div class="select-Categories pb-30">
                                                <div class="small-tittle mb-20">
                                                    <h4>دسته بندی ها</h4>
                                                </div>
                                                <a href="/Categories" class="container">
                                                    <svg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='currentColor' class='bi bi-chevron-left' viewBox='0 0 16 16'>
                                                        <path fill-rule='evenodd' d='M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0' />
                                                    </svg>همه دسته بندی ها
                                                </a>
                                                <%=LinksCategories() %>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-8 col-md-12 col-12">
                                    <div class="row justify-content-start">
                                        <asp:Label ID="lblSearch" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="best-selling p-0">
                                        <div class="row" id="placeholder-content">
                                            <asp:TextBox runat="server" ID="LastID" Text="" Visible="false" />
                                            <asp:Literal ID="Books" Text="" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row">
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

    </form>
    <script src="/js/jquery-3.6.0.min.js"></script>
    <script>

        // URL جدید را بدون پارامترها تنظیم کنید
        window.history.replaceState(null, null, window.location.href.split('?')[0]);
        document.addEventListener("DOMContentLoaded", function () {
            var links = document.querySelectorAll('.select-Categories a');
            var currentUrl = window.location.href.toLowerCase();

            links.forEach(function (link) {
                if (currentUrl === link.href.toLowerCase()) {
                    link.classList.add('active-category');
                }
            });
        });

    </script>
</asp:Content>
