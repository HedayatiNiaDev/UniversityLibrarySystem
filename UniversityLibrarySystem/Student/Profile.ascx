<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Profile.ascx.cs" Inherits="UniversityLibrarySystem.Student.Profile" %>
<style>
    #<%=ImageNopic.ClientID %> {
        border-radius: 0.25rem;
    }
</style>
<script type="text/javascript">
    function previewFile() {

        var preview = document.querySelector('#<%=ImageNopic.ClientID %>');
        var file = document.querySelector('#<%=fileUploadPic.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            preview.src = reader.result;
        }

        if (file) {
            reader.readAsDataURL(file);

        } else {
            preview.src = "../img/no-photo.png";
        }
    }
</script>

<!-- Content -->

<div class="container-xxl flex-grow-1 container-p-y">
    <h4 id="head-title" class="py-3 breadcrumb-wrapper mb-4">پروفایل من</h4>
    <asp:Literal ID="lblMessage" runat="server" Text="" Visible="False"></asp:Literal>

    <div class="row">
        <div class="col-md-12">
            <div class="card mb-4">
                <div class="card-body">
                    <form runat="server">
                        <div class="row">
                            <div class="mb-3 col-md-12">
                                <label for="txtAddress" class="form-label">تصویر پروفایل</label>
                                <div class="col-6">
                                    <asp:Image ID="ImageNopic" runat="server" Height="225px" ImageUrl="../img/Personal/no-photo.png"
                                        Width="225px" />
                                    <br />
                                    <asp:Button ID="btnResetProfile" Visible="false" CssClass="btn btn-danger my-2 mx-3" runat="server" OnClick="btnResetProfile_Click" Text="بازنشانی تصویر پروفایل" />
                                </div>
                                <br />
                                <hr />
                                <asp:FileUpload accept="image/*" ID="fileUploadPic" onChange="previewFile()" runat="server" class="form-control" />
                            </div>
                            <div class="mb-3 col-md-6">
                                <label for="firstName" class="form-label">نام و نام خانوادگی</label>
                                <input class="form-control" type="text" id="txtFullName" runat="server" validationgroup="Valid" placeholder="نام و نام خانوادگی">
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFullName" CssClass="badge bg-label-danger" Display="Dynamic" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator2">این قسمت الزامی است</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Valid"
                                    ControlToValidate="txtFullName" SetFocusOnError="True" CssClass="badge bg-label-danger" Display="Dynamic" ErrorMessage="تعداد کارکتر های مجاز بیشتر از حد مجاز است" ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                            </div>
                            <div class="mb-3 col-md-6">
                                <label for="txtHozeSabti" class="form-label">موبایل</label>
                                <input class="form-control text-start" dir="ltr" type="text" runat="server" validationgroup="Valid" id="txtMobile" placeholder="موبایل">
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile" CssClass="badge bg-label-danger" Display="Dynamic" ErrorMessage="این قسمت الزامی است" ValidationGroup="Valid" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator3">این قسمت الزامی است</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rgeMobileNumber" runat="server" ValidationGroup="Valid"
                                    ControlToValidate="txtMobile" SetFocusOnError="True" CssClass="badge bg-label-danger" Display="Dynamic" ErrorMessage="شماره موبایل به درستی وارد نشده است"
                                    ValidationExpression="09(0(\d)|1(\d)|2(\d)|3(\d)|(9(\d)))\d{7}$"></asp:RegularExpressionValidator>
                            </div>
                            <div class="mb-3 col-md-6">
                                <label for="txtHozeSabti" class="form-label">ایمیل</label>
                                <input class="form-control text-start" dir="ltr" type="text" runat="server" validationgroup="Valid" id="txtEmail" placeholder="ایمیل">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="Valid"
                                    ControlToValidate="txtEmail" SetFocusOnError="True" CssClass="badge bg-label-danger" Display="Dynamic" ErrorMessage="ایمیل به درستی وارد نشده است"
                                    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,255}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="mt-2">
                            <asp:Button ID="btnSubmit" ValidationGroup="Valid" CssClass="btn btn-primary me-2" runat="server" Text="ذخیره تغییرات" OnClick="btnSubmit_Click" />
                            <a href="./" class="btn btn-label-secondary">بازگشت</a>
                        </div>
                    </form>
                </div>
                <!-- /Account -->
            </div>
        </div>
    </div>
</div>
