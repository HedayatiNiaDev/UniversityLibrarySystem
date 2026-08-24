<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="UniversityLibrarySystem.ResetPassword" %>

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
                        <span>بازیابی رمز عبور</span>
                        <p>رمز عبور جدید خود را تنظیم کنید.</p>
                        <asp:Label ID="ErrorMessage" runat="server" CssClass="text-danger" Visible="false" Font-Size="12"></asp:Label>
                    </div>
                    <div class="input-box">

                        <div class="single-input-fields">
                            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">رمز عبور جدید</asp:Label>
                            <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="رمز عبور جدید"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="NewPassword" ErrorMessage="رمز عبور جدید الزامی است" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ControlToValidate="NewPassword" ValidationExpression="^[A-Za-z\d@$!%*?&]{8,}$"
                                ErrorMessage="رمز عبور باید حداقل ۸ کاراکتر باشد و می‌تواند شامل حروف لاتین یا اعداد باشد"
                                Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="NewPassword" ErrorMessage="تعداد کارکتر های مجاز بیشتر از حد مجاز است" ValidationExpression="^.{1,50}$"
                                Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="TextBox1" Visible="false" Text="120" runat="server"></asp:TextBox>
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <div class="forgot-password-footer">
                        <asp:Button ID="ResetPasswordButton" runat="server" Text="تغییر رمز عبور" CssClass="submit-btn3" OnClick="ResetPasswordButton_Click" />
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

</body>
</html>
