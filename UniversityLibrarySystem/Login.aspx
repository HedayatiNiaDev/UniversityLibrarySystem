<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UniversityLibrarySystem.Login" %>

<%@ Register Assembly="BotDetect" Namespace="BotDetect.Web.UI" TagPrefix="BotDetect" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Classes.SiteConfig.mixTitle("ورود به حساب کاربری") %></title>
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
    <main class="login-bg">

        <form runat="server">
            <div class="login-form-area">
                <div class="login-form mb-1">
                    <div class="login-heading">
                        <a href="/">
                            <img src="img/logo/logo.png" alt="<%=Classes.SiteConfig.getSiteName() %>" />
                        </a>
                        <span>ورود</span>
                        <p>برای دسترسی به سامانه وارد شوید</p>
                        <asp:Label ID="ErrorMessage" runat="server" CssClass="text-danger" Visible="false" Font-Size="12"></asp:Label>
                    </div>
                    <div class="input-box">
                        <div class="single-input-fields">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">نام کاربری</asp:Label>
                            <asp:TextBox ID="UserName" MaxLength="50" runat="server" CssClass="form-control" placeholder="نام کاربری"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="UserName" ErrorMessage="نام کاربری الزامی است" Display="Dynamic" CssClass="text-danger" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UserName" ErrorMessage="تعداد کارکتر های مجاز بیشتر از حد مجاز است" ValidationExpression="^.{1,50}$" Display="Dynamic" CssClass="text-danger" ValidationGroup="LoginGroup"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revUsername" runat="server" ControlToValidate="UserName" ErrorMessage="نام کاربری باید فقط شامل اعداد باشد" ValidationExpression="^\d+$" Display="Dynamic" CssClass="text-danger" ValidationGroup="LoginGroup"></asp:RegularExpressionValidator>
                        </div>
                        <div class="single-input-fields">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">رمز عبور</asp:Label>
                            <asp:TextBox ID="Password" MaxLength="50" runat="server" TextMode="Password" CssClass="form-control" placeholder="رمز عبور"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="Password" ErrorMessage="رمز عبور الزامی است" Display="Dynamic" CssClass="text-danger" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="Password" ValidationExpression="^[A-Za-z\d@$!%*?&]{8,50}$"
                                ErrorMessage="رمز عبور باید حداقل ۸ کاراکتر باشد"
                                Display="Dynamic" CssClass="text-danger" ValidationGroup="LoginGroup"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Password" ErrorMessage="تعداد کارکتر های مجاز بیشتر از حد مجاز است" ValidationExpression="^.{1,50}$"
                                Display="Dynamic" CssClass="text-danger" ValidationGroup="LoginGroup"></asp:RegularExpressionValidator>

                        </div>
                        <div class="single-input-fields">
                            <asp:Label ID="CaptchaCodeTextBoxLabel" runat="server" AssociatedControlID="CaptchaCodeTextBox">کد امنیتی</asp:Label>
                            <BotDetect:WebFormsCaptcha ID="BotCaptcha" ImageSample runat="server" />
                            <br />
                            <asp:TextBox runat="server" ID="CaptchaCodeTextBox" ValidationGroup="LoginGroup" MaxLength="6" placeholder="کد امنیتی" CssClass="form-control w-50" />
                            <asp:RequiredFieldValidator ID="rfvFieldCaptcha" runat="server" ControlToValidate="CaptchaCodeTextBox" ErrorMessage="کد امنیتی الزامی است" Display="Dynamic" CssClass="text-danger" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="CaptchaCodeTextBox" ErrorMessage="تعداد کارکتر های مجاز بیشتر از حد مجاز است" ValidationExpression="^.{1,10}$"
                                Display="Dynamic" CssClass="text-danger" ValidationGroup="LoginGroup"></asp:RegularExpressionValidator>
                            <label id="rfvInvalidCode" visible="false" runat="server" class="text-danger">کد امنیتی وارد شده نادرست است</label>
                        </div>
                    </div>
                    <div class="login-footer">
                        <p><a href="/ForgetPassword">رمز خود را فراموش کرده اید؟</a></br>حساب کاربری ندارید؟ <a href="Register">ثبت نام کنید</a></p>
                        <asp:Button ID="LoginButton" runat="server" Text="ورود" CssClass="submit-btn3" ValidationGroup="LoginGroup" OnClick="LoginButton_Click" />
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
