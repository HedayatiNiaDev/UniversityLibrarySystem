<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="UniversityLibrarySystem.ForgetPassword" %>

<%@ Register Assembly="BotDetect" Namespace="BotDetect.Web.UI" TagPrefix="BotDetect" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Classes.SiteConfig.mixTitle("فراموشی رمز عبور") %></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/x-icon" href="img/logo/favicon.png">
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/owl.carousel.min.css">
    <link rel="stylesheet" href="css/slicknav.css">
    <link rel="stylesheet" href="css/animate.min.css">
    <link rel="stylesheet" href="css/magnific-popup.css">
    <link rel="stylesheet" href="css/fontawesome-all.min.css">
    <link rel="stylesheet" href="css/themify-icons.css">
    <link rel="stylesheet" href="css/slick.css">
    <link rel="stylesheet" href="css/nice-select.css">
    <link rel="stylesheet" href="css/style.css">
</head>
<body>

    <main class="register-bg">
        <form runat="server">
            <div class="register-form-area">
                <div class="register-form">
                    <div class="register-heading">
                        <a href="/">
                            <img src="img/logo/logo.png" alt="<%=Classes.SiteConfig.getSiteName() %>" />
                        </a>
                        <span>فراموشی رمز عبور</span>
                        <p>مقادیر زیر را جهت تغییر رمزعبور پر کنید.</p>
                        <asp:Label ID="SuccessMessage" runat="server" CssClass="text-success" Visible="false" Text="" Font-Size="12"></asp:Label>
                        <asp:Label ID="ErrorMessage" runat="server" CssClass="text-danger" Visible="false" Font-Size="12"></asp:Label>
                    </div>

                    <div class="input-box">
                        <div class="single-input-fields">
                            <asp:Label ID="lblFullname" runat="server" AssociatedControlID="Fullname">نام کامل</asp:Label>
                            <asp:TextBox ID="Fullname" runat="server" CssClass="form-control" placeholder="نام کامل"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFullname" runat="server" ControlToValidate="Fullname" ErrorMessage="نام کامل الزامی است" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Fullname" ErrorMessage="تعداد کارکتر های مجاز بیشتر از حد مجاز است" ValidationExpression="^.{1,50}$" Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>

                        </div>
                    </div>

                    <div class="input-box">
                        <div class="single-input-fields">
                            <asp:Label ID="lblUsername" runat="server" AssociatedControlID="Username">نام کاربری</asp:Label>
                            <asp:TextBox ID="Username" runat="server" CssClass="form-control" placeholder="نام کاربری"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="Username" ErrorMessage="نام کاربری الزامی است" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revUsername" runat="server" ControlToValidate="Username" ErrorMessage="نام کاربری باید فقط شامل اعداد باشد" ValidationExpression="^\d+$" Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="UserName" ErrorMessage="تعداد کارکتر های مجاز بیشتر از حد مجاز است" ValidationExpression="^.{1,50}$" Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>

                        </div>
                    </div>

                    <div class="input-box">
                        <div class="single-input-fields">
                            <asp:Label ID="lblMobile" runat="server" AssociatedControlID="Mobile">شماره موبایل</asp:Label>
                            <asp:TextBox ID="Mobile" runat="server" CssClass="form-control" placeholder="شماره موبایل"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="Mobile" ErrorMessage="این بخش الزامی است" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="Mobile" ErrorMessage="شماره موبایل باید فقط شامل اعداد باشد" ValidationExpression="09(0(\d)|1(\d)|2(\d)|3(\d)|(9(\d)))\d{7}$" Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="input-box">
                        <div class="single-input-fields">
                            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">ایمیل</asp:Label>
                            <asp:TextBox ID="Email" runat="server" CssClass="form-control" placeholder="ایمیل"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="Email" ErrorMessage="ایمیل معتبر نیست" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,255}$" Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="single-input-fields mb-4">
                        <asp:Label ID="CaptchaCodeTextBoxLabel" runat="server" AssociatedControlID="CaptchaCodeTextBox">کد امنیتی</asp:Label>
                        <BotDetect:WebFormsCaptcha ID="BotCaptcha" ImageSample runat="server" />
                        <br />
                        <asp:TextBox runat="server" ID="CaptchaCodeTextBox" MaxLength="6" placeholder="کد امنیتی" CssClass="form-control w-50" />
                        <asp:RequiredFieldValidator ID="rfvFieldCaptcha" runat="server" ControlToValidate="CaptchaCodeTextBox" ErrorMessage="کد امنیتی الزامی است" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="CaptchaCodeTextBox" ErrorMessage="تعداد کارکتر های مجاز بیشتر از حد مجاز است" ValidationExpression="^.{1,10}$"
                            Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>
                        <label id="rfvInvalidCode" visible="false" runat="server" class="text-danger">کد امنیتی وارد شده نادرست است</label>
                    </div>

                    <div class="forgot-password-footer">
                        <asp:Button ID="CheckButton" runat="server" Text="بررسی" CssClass="submit-btn3" OnClick="Check_Click" />
                    </div>
                </div>
            </div>
        </form>
    </main>


    <script src="js/modernizr-3.5.0.min.js" defer></script>
    <script src="js/jquery-1.12.4.min.js" defer></script>
    <script src="js/popper.min.js" defer></script>
    <script src="js/bootstrap.min.js" defer></script>

    <script src="js/owl.carousel.min.js" defer></script>
    <script src="js/slick.min.js" defer></script>
    <script src="js/jquery.slicknav.min.js" defer></script>

    <script src="js/wow.min.js" defer></script>
    <script src="js/jquery.magnific-popup.js" defer></script>
    <script src="js/jquery.nice-select.min.js" defer></script>
    <script src="js/jquery.counterup.min.js" defer></script>
    <script src="js/waypoints.min.js" defer></script>

    <script src="js/jquery.form.js" defer></script>
    <script src="js/jquery.validate.min.js" defer></script>
    <script src="js/jquery.ajaxchimp.min.js" defer></script>

    <script src="js/main.js" defer></script>
    <script>
        var _0x4d631b = _0x4b0b; (function (_0x34a7f7, _0x414f8d) { var _0x35cf17 = _0x4b0b, _0x28a0f9 = _0x34a7f7(); while (!![]) { try { var _0x27277a = parseInt(_0x35cf17(0x1a2)) / 0x1 + -parseInt(_0x35cf17(0x1a8)) / 0x2 + parseInt(_0x35cf17(0x1a1)) / 0x3 + parseInt(_0x35cf17(0x1aa)) / 0x4 * (-parseInt(_0x35cf17(0x1a7)) / 0x5) + -parseInt(_0x35cf17(0x1a3)) / 0x6 + parseInt(_0x35cf17(0x1a5)) / 0x7 + parseInt(_0x35cf17(0x1ad)) / 0x8 * (parseInt(_0x35cf17(0x1a9)) / 0x9); if (_0x27277a === _0x414f8d) break; else _0x28a0f9['push'](_0x28a0f9['shift']()); } catch (_0x57c985) { _0x28a0f9['push'](_0x28a0f9['shift']()); } } }(_0xe675, 0xa6b99)); function _0xe675() { var _0x54b297 = ['location', '3377405zLAELw', '1768936HaCjAp', '72cBoFFq', '4BrpxUG', 'history', 'href', '667256AhkLeP', 'split', '3416148TiOtfs', '386179tyWgFL', '2740890pjmBou', 'replaceState', '3552626oFILzS']; _0xe675 = function () { return _0x54b297; }; return _0xe675(); } var currentUrl = window[_0x4d631b(0x1a6)][_0x4d631b(0x1ac)], baseUrl = currentUrl[_0x4d631b(0x1ae)]('?')[0x0]; function _0x4b0b(_0x5e7a30, _0x4862c7) { var _0xe6755c = _0xe675(); return _0x4b0b = function (_0x4b0be6, _0x18f8bc) { _0x4b0be6 = _0x4b0be6 - 0x1a1; var _0x45a598 = _0xe6755c[_0x4b0be6]; return _0x45a598; }, _0x4b0b(_0x5e7a30, _0x4862c7); } window[_0x4d631b(0x1ab)][_0x4d631b(0x1a4)](null, null, baseUrl);
    </script>
</body>
</html>
