namespace PAP
{
    public static class InClude
    {
        public static string Top()
        {
            return @"
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0"">

    <meta name=""description"" content="""">

    <!-- Favicon -->
    <link rel=""icon"" type=""image/x-icon"" href=""../img/logo/favicon.png"">

    <!-- Core CSS -->
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/css/rtl/core.css"" class=""template-customizer-core-css""/>
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/css/rtl/theme-default.css"" class=""template-customizer-theme-css""/>
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/css/rtl/core-dark.css"" class=""template-customizer-core-dark-css"" disabled/>
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/css/rtl/theme-default-dark.css"" class=""template-customizer-theme-dark-css"" disabled/>
    <!-- LightMode JS -->
    <script type=""text/javascript"">
        if (localStorage.length == 0) {
            localStorage.setItem('light-Mode', true);
        }
        const coreDark = document.querySelector('.template-customizer-core-dark-css');
        const themeDark = document.querySelector('.template-customizer-theme-dark-css');
        var lightMode = localStorage.getItem('light-Mode') === 'true';
        coreDark.disabled = lightMode;
        themeDark.disabled = lightMode;
        function togglelightMode() {
            var islightMode = !coreDark.disabled;
            localStorage.setItem('light-Mode', islightMode);
            coreDark.disabled = islightMode;
            themeDark.disabled = islightMode;

            /*const icon = document.getElementById('icon-toggle-dark-mode');

            if (islightMode) {
                icon.classList.remove('bx-moon');
                icon.classList.add('bx-sun');
            } else {
                icon.classList.remove('bx-sun');
                icon.classList.add('bx-moon');
            }*/
        }
        /*window.addEventListener('load', function () {
            const icon = document.getElementById('icon-toggle-dark-mode');
            document.getElementById('toggle-dark-mode').addEventListener('click', togglelightMode);
            if (lightMode) {
                icon.classList.remove('bx-moon');
                icon.classList.add('bx-sun');
            } else {
                icon.classList.remove('bx-sun');
                icon.classList.add('bx-moon');
            }

        });*/
    </script>

    <link rel=""stylesheet"" href=""../PAPAssets/css/demo.css""/>
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/css/rtl/rtl.css""/>

    <!-- Icons -->
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/fonts/boxicons.css""/>
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/fonts/fontawesome.css""/>
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/fonts/flag-icons.css""/>

    <!-- Vendors CSS -->
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/libs/perfect-scrollbar/perfect-scrollbar.css""/>
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/libs/typeahead-js/typeahead.css""/>
    <link rel=""stylesheet"" href=""../PAPAssets/vendor/libs/apex-charts/apex-charts.css""/>

    <!-- Page CSS -->
    <link href=""../PAPAssets/map/app/css/app.css"" rel=""stylesheet"" type=""text/css"" />
    <link href=""../PAPAssets/map/dist/css/fa/style.css"" rel=""stylesheet"" type=""text/css"" />
    <!-- Helpers -->
    <script src=""../PAPAssets/vendor/js/helpers.js""></script>

    <!--? Config:  Mandatory theme config file contain global vars & default theme options, Set your preferred theme option in this file.  -->
    <script src=""../PAPAssets/js/config.js""></script>

    <link href=""../PAPAssets/dist/dropzone.css"" rel=""stylesheet"" />
    <script src=""../PAPAssets/dist/dropzone.js""></script>

    <style>
        div.avatar-online img {
            border: #39da8a solid 2px;
        }

        div.avatar-offline img {
            border: transparent solid 2px;
        }
    </style>
";
        }

        public static string Bottom()
        {
            return @"
    <!-- Core JS -->
    <!-- build:js vendor/js/core.js -->
    <script src=""../PAPAssets/vendor/libs/jquery/jquery.js"" defer></script>
    <script src=""../PAPAssets/vendor/libs/popper/popper.js"" defer></script>
    <script src=""../PAPAssets/vendor/js/bootstrap.js"" defer></script>
    <script src=""../PAPAssets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js"" defer></script>

    <script src=""../PAPAssets/vendor/libs/hammer/hammer.js"" defer></script>

    <script src=""../PAPAssets/vendor/libs/i18n/i18n.js"" defer></script>
    <script src=""../PAPAssets/vendor/libs/typeahead-js/typeahead.js"" defer></script>

    <script src=""../PAPAssets/vendor/js/menu.js"" defer></script>
    <!-- endbuild -->

    <!-- Vendors JS -->
    <script src=""../PAPAssets/vendor/libs/apex-charts/apexcharts.js"" defer></script>

    <!-- Main JS -->
    <script src=""../PAPAssets/js/main.js"" defer></script>

    <!-- Page JS -->
    <script src=""../PAPAssets/js/dashboards-analytics.js"" defer></script>

    <!-- Nav JS -->
    <script src=""../PAPAssets/js/NavMenuItem.js"" defer></script>
";
            /*
             <%--<script type=""text/javascript"">
        Dropzone.options.myAwesomeDropzone = {
            paramName: ""file"", // The name that will be used to transfer the file
            maxFilesize: 100, // MB    
            addRemoveLinks: true,
            //        url: ""../hn_SimpeFileUploader.ashx""
            url: ""/Mananger/PicUploader.aspx?tid=<%=Request.QueryString[""tid""]%>""
        };
    </script>--%>
    */

        }
    }
}