<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerCommand.aspx.cs" Inherits="UniversityLibrarySystem.ServerSideChecker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Console</title>
    <style>
        :root{
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-weight:bold;
            background-color:#000;
            color:#0f0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <script>
                // این تابع پارامترهای URL را پاک می‌کند
                function clearUrlParams() {
                    // URL فعلی را دریافت کنید
                    var currentUrl = window.location.href;

                    // فقط بخش اصلی URL را بدون پارامترها دریافت کنید
                    var baseUrl = currentUrl.split('.aspx?')[0];

                    // URL جدید را بدون پارامترها تنظیم کنید
                    window.history.replaceState(null, null, baseUrl);
                }

                // تابع را فراخوانی کنید تا پارامترهای URL پاک شوند
                clearUrlParams();

            </script>
        </div>
    </form>
</body>
</html>
