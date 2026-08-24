<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UniversityLibrarySystem.Student.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="fa" class="light-style layout-navbar-fixed layout-menu-fixed" dir="rtl" data-theme="theme-default-dark" data-assets-path="../PAPAssets/" data-template="vertical-menu-template">
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
                    <li class="menu-header small text-uppercase"><span class="menu-header-text">جستوجو کتاب</span></li>
                    <li class="menu-item">
                        <a href="../Search" class="menu-link">
                            <i class="menu-icon tf-icons bx bx-search-alt-2"></i>
                            <div>جستوجو کتاب</div>
                        </a>
                    </li>
                    <!-- Book Management -->
                    <li class="menu-header small text-uppercase"><span class="menu-header-text">مدیریت کتاب ها</span></li>
                    <li class="menu-item">
                        <a href="javascript:void(0);" class="menu-link menu-toggle">
                            <i class="menu-icon tf-icons bx bx-book-alt"></i>
                            <div>مدیریت کتاب ها</div>
                        </a>
                        <ul class="menu-sub">
                            <li class="menu-item">
                                <a href="./Books" class="menu-link">
                                    <div>مشاهده همه</div>
                                </a>
                            </li>
                            <li class="menu-item">
                                <a href="./Books?Mode=1" class="menu-link">
                                    <div><%=Classes.ReserveStatus.ReserveStatusToText(1) %></div>
                                </a>
                            </li>
                            <li class="menu-item">
                                <a href="./Books?Mode=2" class="menu-link">
                                    <div><%=Classes.ReserveStatus.ReserveStatusToText(2) %></div>
                                </a>
                            </li>
                            <li class="menu-item">
                                <a href="./Books?Mode=3" class="menu-link">
                                    <div><%=Classes.ReserveStatus.ReserveStatusToText(3) %></div>
                                </a>
                            </li>
                            <li class="menu-item">
                                <a href="./Books?Mode=4" class="menu-link">
                                    <div><%=Classes.ReserveStatus.ReserveStatusToText(4) %></div>
                                </a>
                            </li>
                        </ul>
                    </li>

                    <!-- Settings -->
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
                                <!-- User -->
                                <%=Profile() %>
                                <!--/ User -->
                            </ul>
                        </div>
                    </div>
                </nav>

                <!-- / Navbar -->

                <!-- Content wrapper -->
                <%=checkFinance() %>
                <div class="content-wrapper">

                    <asp:Panel ID="PanelLoad" CssClass="m-0" runat="server"></asp:Panel>
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
    <%=PAP.InClude.Bottom() %>
    <script>
        function updateUserStatus() {
            const userAvatar = document.getElementById('user-avatar');
            const userAvatarMenu = document.getElementById('user-avatar-menu');
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

        // بروزرسانی وضعیت در ابتدا
        updateUserStatus();

        // تنظیم رویداد برای تغییر وضعیت
        window.addEventListener('online', updateUserStatus);
        window.addEventListener('offline', updateUserStatus);
    </script>
</body>
</html>
