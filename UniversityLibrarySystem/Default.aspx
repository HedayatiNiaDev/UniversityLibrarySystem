<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UniversityLibrarySystem.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #SiteName {
            text-align: center;
            margin: 25px 0;
            font-size: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form method="get" action="search">
        <div style="margin: 200px 0 200px 0">
            <h2 id="SiteName"><%=Classes.SiteConfig.getSiteName() %></h2>

            <div class="d-flex justify-content-center align-items-center px-2">

                <div class="row border-radius-30 form-control-border m-0 p-0 search-bar-custom">
                    <div class="col-9 col-md-10 col-lg-11 px-0 m-0">
                        <input type="text" class="form-control search-input border-transparent" placeholder="نام کتاب را وارد نمایید" name="BookName" id="txtBookName">
                    </div>
                    <div class="col-3 col-md-2 col-lg-1 px-2 m-0">
                        <button type="submit" class="search-btn w-100">
                            <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                                <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0" />
                            </svg>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <form runat="server">
        <div class="categories-slider">
            <div class="container">
                <div class="row justify-content-right">
                    <div class="col-xl-7 col-lg-8">
                        <div class="section-tittle">
                            <h2>دسته بندی ها</h2>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-12">
                        <div class="categories-active">

                            <%=CategoriesBookHScroll() %>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="best-selling section-bg">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-xl-7 col-lg-8">
                        <div class="section-tittle text-center mb-55">
                            <h2>کتاب های ویژه</h2>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xl-12">
                        <div class="selling-active">
                            <%=SpecialBook() %>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <section class="our-client section-padding best-selling">
            <div class="container">
                <div class="section-tittle  mb-40">
                    <h2>جدیدترین کتبــ ها</h2>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <%=NewBook() %>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-12">
                    <div class="more-btn text-center mt-15">
                        <a href="/Categories" class="border-btn border-btn2 more-btn2">مشاهده بیشتر</a>
                    </div>
                </div>
            </div>
            </div>
        </section>
    </form>
</asp:Content>
