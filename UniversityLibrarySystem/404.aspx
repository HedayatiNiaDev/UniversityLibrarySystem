<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="UniversityLibrarySystem._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/x-icon" href="img/logo/favicon.png">
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/style.css">
</head>
<body>
    <main class="login-bg">
        <div class="login-form-area">
            <div class="login-form mb-1 text-center">
                <h1 class="display-1">404</h1>
                <p class="lead">صفحه مورد نظر یافت نشد!</p>
                <p>متاسفانه صفحه‌ای که به دنبال آن هستید وجود ندارد یا منتقل شده است.</p>
                <a id="btnBack" class="btn header-btn">بازگشت</a>
            </div>
        </div>
    </main>
    <script>
        var btnBack = document.getElementById("btnBack");
        if (history.length < 2) {
            btnBack.textContent = "بازگشت به صفحه اصلی";
        }
        btnBack.addEventListener('click', function () {
            if (history.length > 1) {
                history.go(-1);
            }
            else {
                window.location.replace(window.location.origin)
            }
        });
    </script>
</body>
</html>
