<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UniversityLibrarySystem.Manager.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="fa" class="light-style layout-navbar-fixed layout-menu-fixed" dir="rtl" data-assets-path="../PAPAssets/" data-template="vertical-menu-template">
<head runat="server">
    <%=PAP.InClude.Top() %>
</head>
<body>
    <!-- Layout wrapper -->
    <div class="layout-wrapper layout-content-navbar">
        <div class="layout-container">
            <!-- Menu -->

            <aside id="layout-menu" class="layout-menu menu-vertical menu bg-menu-theme">
                <div class="app-brand demo">
                    <a href="./" class="app-brand-link gap-2 mb-2">
                        <img style="margin: 0.35rem; width: 80%" src="../img/logo/favicon.png">
                    </a>

                    <a href="javascript:void(0);" class="layout-menu-toggle menu-link text-large ms-auto">
                        <i class="bx menu-toggle-icon d-none d-xl-block fs-4 align-middle"></i>
                        <i class="bx bx-x d-block d-xl-none bx-sm align-middle"></i>
                    </a>
                </div>

                <div class="menu-divider mt-0"></div>

                <div class="menu-inner-shadow"></div>
                <ul class="menu-inner py-1">
                    <!-- Dashboards -->
                    <li class="menu-item">
                        <a href="../Home" class="menu-link">
                            <i class="menu-icon tf-icons bx bxs-home"></i>
                            <div>خانه</div>
                        </a>
                    </li>

                    <li class="menu-item">
                        <a href="./Dashboard" class="menu-link">
                            <i class="menu-icon tf-icons bx bxs-dashboard"></i>
                            <div>داشبورد</div>
                        </a>
                    </li>

                    <li class="menu-header small text-uppercase"><span class="menu-header-text">مدیریت دسته بندی ها</span></li>
                    <li class="menu-item">
                        <a href="./Categories" class="menu-link">
                            <i class="menu-icon tf-icons bx bxs-category"></i>
                            <div>مدیریت دسته بندی ها</div>
                        </a>
                    </li>
                    <li class="menu-header small text-uppercase"><span class="menu-header-text">مدیریت کتب</span></li>
                    <li class="menu-item">
                        <a href="./Books" class="menu-link">
                            <i class="menu-icon tf-icons bx bxs-book"></i>
                            <div>مدیریت کتاب ها</div>
                        </a>
                    </li>
                    <li class="menu-item">
                        <a href="./Reservations" class="menu-link">
                            <i class="menu-icon tf-icons bx bxs-book"></i>
                            <div>مدیریت رزرو ها</div>
                        </a>
                    </li>
                    <%--                    <li class="menu-header small text-uppercase"><span class="menu-header-text">امور مالی</span></li>
                    <li class="menu-item">
                        <a href="./Finance" class="menu-link">
                            <i class="menu-icon tf-icons bx bxs-wallet"></i>
                            <div>امور مالی</div>
                        </a>
                    </li>--%>
                    <li class="menu-header small text-uppercase"><span class="menu-header-text">مدیریت دانشجویان</span></li>
                    <li class="menu-item">
                        <a href="./Students" class="menu-link">
                            <i class="menu-icon tf-icons bx bxs-user-circle"></i>
                            <div>مدیریت دانشجویان</div>
                        </a>
                    </li>
                    <!-- Personnel Management -->
                    <li class="menu-header small text-uppercase"><span class="menu-header-text">مدیریت مدیران</span></li>
                    <li class="menu-item">
                        <a href="./Manager" class="menu-link">
                            <i class="menu-icon tf-icons bx bxs-user-circle"></i>
                            <div>مدیریت مدیران</div>
                        </a>
                    </li>
                    <li class="menu-item">
                        <a href="./Permission" class="menu-link">
                            <i class="menu-icon tf-icons bx bx-lock-open"></i>
                            <div>سطح دسترسی مدیران</div>
                        </a>
                    </li>

                    <!-- Settings -->
                    <li class="menu-header small text-uppercase"><span class="menu-header-text">تنظیمات</span></li>
                    <li class="menu-item">
                        <a href="./SystemSettings" class="menu-link">
                            <i class="menu-icon tf-icons bx bx-cog"></i>
                            <div>تنظیمات سامانه</div>
                        </a>
                    </li>
                    <li class="menu-item">
                        <a href="./Faqs" class="menu-link">
                            <i class="menu-icon tf-icons bx bx-question-mark"></i>
                            <div>سوالات متداول</div>
                        </a>
                    </li>
                    <li class="menu-header small text-uppercase"><span class="menu-header-text">ناحیه کاربری</span></li>
                    <li class="menu-item">
                        <a href="./Profile" class="menu-link">
                            <i class="menu-icon tf-icons bx bx-user"></i>
                            <div>پروفایل من</div>
                        </a>
                    </li>
                    <li class="menu-item">
                        <a href="./LoginReport" class="menu-link">
                            <i class="menu-icon tf-icons bx bx-log-in"></i>
                            <div>گزارش ورود</div>
                        </a>
                    </li>
                    <li class="menu-item">
                        <a href="./ChangePassword" class="menu-link">
                            <i class="menu-icon tf-icons bx bx-lock"></i>
                            <div>تغییر کلمه عبور</div>
                        </a>
                    </li>

                    <!-- Logout -->
                    <li class="menu-item">
                        <a href="./LogOut" class="menu-link">
                            <i class="menu-icon tf-icons bx bx-log-out"></i>
                            <div>خروج از حساب کاربری</div>
                        </a>
                    </li>
                </ul>

            </aside>
            <!-- / Menu -->

            <!-- Layout container -->
            <div class="layout-page">
                <!-- Navbar -->

                <nav class="layout-navbar navbar navbar-expand-xl align-items-center bg-navbar-theme" id="layout-navbar">
                    <div class="container-fluid">
                        <div class="layout-menu-toggle navbar-nav align-items-xl-center me-3 me-xl-0 d-xl-none">
                            <a class="nav-item nav-link px-0 me-xl-4" href="javascript:void(0)">
                                <i class="bx bx-menu bx-sm"></i>
                            </a>
                        </div>

                        <div class="navbar-nav-right d-flex align-items-center" id="navbar-collapse">
                            <div class="navbar-nav align-items-center">
                                <p class="mb-1" style="font-size: medium;"><%=Date() %></p>
                            </div>

                            <ul class="navbar-nav flex-row align-items-center ms-auto">
                                <%--<a id="toggle-dark-mode"><i id="icon-toggle-dark-mode" class="bx bx-sm"></i></a>--%>
                                <!-- User -->
                                <li class="nav-item dropdown-shortcuts navbar-dropdown dropdown me-2 me-xl-0">
                                    <a class="nav-link dropdown-toggle hide-arrow" href="javascript:void(0);" data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
                                        <i class="bx bx-grid-alt bx-sm"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-end py-0">
                                        <div class="dropdown-menu-header border-bottom">
                                            <div class="dropdown-header d-flex align-items-center py-3">
                                                <h5 class="text-body mb-0 me-auto secondary-font">میانبرها</h5>
                                            </div>
                                        </div>
                                        <div class="dropdown-shortcuts-list scrollable-container ps ps__rtl">
                                            <div class="row row-bordered overflow-visible g-0">
                                                <div class="dropdown-shortcuts-item col">
                                                    <span class="dropdown-shortcuts-icon bg-label-secondary rounded-circle mb-2">
                                                        <i class="bx bx-book fs-4"></i>
                                                    </span>
                                                    <a href="./Reservations" class="stretched-link">مدیریت رزرو ها</a>
                                                </div>
                                                <div class="dropdown-shortcuts-item col">
                                                    <span class="dropdown-shortcuts-icon bg-label-secondary rounded-circle mb-2">
                                                        <i class="bx bx-user fs-4"></i>
                                                    </span>
                                                    <a href="./Students" class="stretched-link">مدیریت دانشجویان</a>
                                                </div>
                                            </div>
                                            <div class="row row-bordered overflow-visible g-0">
                                                <div class="dropdown-shortcuts-item col">
                                                    <span class="dropdown-shortcuts-icon bg-label-secondary rounded-circle mb-2">
                                                        <i class="bx bx-category fs-4"></i>
                                                    </span>
                                                    <a href="./Categories" class="stretched-link">مدیریت دسته بندی ها</a>
                                                </div>
                                                <div class="dropdown-shortcuts-item col">
                                                    <span class="dropdown-shortcuts-icon bg-label-secondary rounded-circle mb-2">
                                                        <i class="bx bx-book fs-4"></i>
                                                    </span>
                                                    <a href="./books" class="stretched-link">مدیریت کتاب ها</a>
                                                </div>
                                            </div>
                                            <div class="row row-bordered overflow-visible g-0">
                                                <div class="dropdown-shortcuts-item col">
                                                    <span class="dropdown-shortcuts-icon bg-label-secondary rounded-circle mb-2">
                                                        <i class="bx bx-lock fs-4"></i>
                                                    </span>
                                                    <a href="./Permission" class="stretched-link">سطح دسترسی مدیران</a>
                                                </div>
                                                <div class="dropdown-shortcuts-item col">
                                                    <span class="dropdown-shortcuts-icon bg-label-secondary rounded-circle mb-2">
                                                        <i class="bx bx-user fs-4"></i>
                                                    </span>
                                                    <a href="./Manager" class="stretched-link">مدیریت مدیران</a>
                                                </div>
                                            </div>
                                            <div class="row row-bordered overflow-visible g-0">
                                                <div class="dropdown-shortcuts-item col">
                                                    <span class="dropdown-shortcuts-icon bg-label-secondary rounded-circle mb-2">
                                                        <i class="bx bx-help-circle fs-4"></i>
                                                    </span>
                                                    <a href="./faqs" class="stretched-link">سوالات متداول</a>
                                                </div>
                                                <div class="dropdown-shortcuts-item col">
                                                    <span class="dropdown-shortcuts-icon bg-label-secondary rounded-circle mb-2">
                                                        <i class="bx bx-cog fs-4"></i>
                                                    </span>
                                                    <a href="./SystemSettings" class="stretched-link">تنظیمات سامانه</a>
                                                </div>
                                            </div>
                                            <div class="ps__rail-x" style="left: 0px; bottom: 0px;">
                                                <div class="ps__thumb-x" tabindex="0" style="left: 0px; width: 0px;"></div>
                                            </div>
                                            <div class="ps__rail-y" style="top: 0px; right: -13px;">
                                                <div class="ps__thumb-y" tabindex="0" style="top: 0px; height: 0px;"></div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <%=Profile() %>

                                <!--/ User -->
                            </ul>

                        </div>
                    </div>
                </nav>

                <!-- / Navbar -->

                <!-- Content wrapper -->
                <%=SiteStatus() %>
                <div class="content-wrapper">
                    <asp:Panel ID="PanelLoad" runat="server"></asp:Panel>
                </div>
                <!-- Content wrapper -->
            </div>
            <!-- / Layout page -->
        </div>

        <!-- Overlay -->
        <div class="layout-overlay layout-menu-toggle"></div>

        <!-- Drag Target Area To SlideIn Menu On Small Screens -->
        <div class="drag-target"></div>
    </div>
    <!-- / Layout wrapper -->
    <script>
        function updateUserStatus() {
            const userAvatar = document.getElementById('user-avatar');
            const userAvatarMenu = document.getElementById('user-avatar-menu');
            const noInternetConnection = document.getElementById('nointernetconnection');
            if (navigator.onLine) {
                userAvatar.classList.remove('avatar-offline');
                userAvatar.classList.add('avatar-online');
                userAvatarMenu.classList.remove('avatar-offline');
                userAvatarMenu.classList.add('avatar-online');
            } else {
                userAvatar.classList.remove('avatar-online');
                userAvatar.classList.add('avatar-offline');
                userAvatarMenu.classList.remove('avatar-online');
                userAvatarMenu.classList.add('avatar-offline');
            }
        }

        updateUserStatus();

        // تنظیم رویداد برای تغییر وضعیت
        window.addEventListener('online', updateUserStatus);
        window.addEventListener('offline', updateUserStatus);
    </script>
    <%=PAP.InClude.Bottom() %>
</body>
</html>
