<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.ascx.cs" Inherits="PAP.AccessDenied" %>
<div class="content-wrapper">
    <!-- Content -->

    <div class="container-xxl flex-grow-1 container-p-y">
        <style>
            :root {
                --custom-color-bg-menu-item: #ffcccc;
                --custom-color-text-menu-item: #ff0000;
            }

            .message {
                text-align: center;
                padding: 20px;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            }

            h1 {
                font-size: 24px;
                margin-bottom: 10px;
            }

            p {
                font-size: 18px;
                margin: 0;
            }
        </style>
        <div class="card message">
            <h1>403 - دسترسی ممنوع شد</h1>
            <p>به نظر می‌رسد شما دسترسی لازم برای مشاهده این صفحه را ندارید یا دسترسی شما ممنوع است.</p>
        </div>
    </div>
    <!-- / Content -->
    <div class="content-backdrop fade"></div>
</div>
