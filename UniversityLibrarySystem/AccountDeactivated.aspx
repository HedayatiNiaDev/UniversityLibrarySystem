<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountDeactivated.aspx.cs" Inherits="UniversityLibrarySystem.AccountDeactivated" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/x-icon" href="img/logo/favicon.png">
    <title><%=Classes.SiteConfig.mixTitle("حساب کاربری شما غیرفعال شده است")%></title>
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/style.css">
</head>
<body>
    <main class="login-bg">
        <div class="login-form-area">
            <div class="login-form mb-1 text-center">
                <img src="img/logo/logo.png" class="mb-1" alt="<%=Classes.SiteConfig.getSiteName()%>" />
                <p class="lead">حساب کاربری شما غیرفعال شده است.</p>
                <p>متاسفانه حساب کاربری شما غیرفعال گردیده است. لطفاً با مدیر سیستم تماس بگیرید.</p>
                <a href="/" class="btn header-btn">بازگشت به صفحه اصلی</a>
            </div>
        </div>
    </main>
</body>
</html>
