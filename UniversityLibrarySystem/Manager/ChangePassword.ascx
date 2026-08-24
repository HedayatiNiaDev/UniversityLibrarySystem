<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs" Inherits="UniversityLibrarySystem.Manager.ChangePassword" %>
<%@ Register Assembly="BotDetect" Namespace="BotDetect.Web.UI" TagPrefix="BotDetect" %>
<style>
    .BDC_CaptchaIconsDiv {
        display: none;
    }
</style>
<div class="container-xxl flex-grow-1 container-p-y">
    <div class="row">
        <div class="col-md-12">
            <div class="card mb-4">
                <h5 class="card-header">تغییر کلمه عبور</h5>
                <form class="card-body" runat="server">
                    <div class="row g-3">
                        <div class="col-12">
                            <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-7">
                            <div class="form-password-toggle">
                                <label class="form-label" for="multicol-password">رمز عبور کنونی</label>
                                <div class="input-group input-group-merge">
                                    <input type="password" id="CurrentPassword" validationgroup="Valid" runat="server" class="form-control text-start" dir="ltr" placeholder="············" aria-describedby="multicol-password2">
                                    <span class="input-group-text cursor-pointer" id="multicol-password2"><i class="bx bx-hide"></i></span>
                                </div>
                                <asp:RequiredFieldValidator CssClass="badge bg-label-danger" ID="RequiredFieldValidator1"
                                    runat="server" ErrorMessage="رمز عبور کنونی الزامیست" ControlToValidate="CurrentPassword" Display="Dynamic"
                                    ValidationGroup="Valid"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="CurrentPassword"
                                    ValidationExpression="^[A-Za-z\d@$!%*?&]{8,}$"
                                    ErrorMessage="رمز عبور باید حداقل ۸ کاراکتر باشد و می‌تواند شامل حروف لاتین یا اعداد باشد"
                                    ValidationGroup="Valid"
                                    Display="Dynamic" CssClass="badge bg-label-danger"></asp:RegularExpressionValidator>
                                <label id="lblInvalidCurrentPassword" visible="false" runat="server" class="badge bg-label-danger">رمز عبور کنونی وارد شده نادرست است</label>

                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-password-toggle">
                                <label class="form-label" for="multicol-password">رمز عبور</label>
                                <div class="input-group input-group-merge">
                                    <input type="password" id="txtPassword" validationgroup="Valid" runat="server" class="form-control text-start" dir="ltr" placeholder="············" aria-describedby="multicol-password2">
                                    <span class="input-group-text cursor-pointer" id="multicol-password2"><i class="bx bx-hide"></i></span>
                                </div>
                                <asp:RequiredFieldValidator CssClass="badge bg-label-danger" ID="RequiredFieldValidatorTextBoxNewPassword"
                                    runat="server" ErrorMessage="رمز جدید الزامیست" ControlToValidate="txtPassword" Display="Dynamic"
                                    ValidationGroup="Valid"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                                    ValidationExpression="^[A-Za-z\d@$!%*?&]{8,}$"
                                    ErrorMessage="رمز عبور باید حداقل ۸ کاراکتر باشد و می‌تواند شامل حروف لاتین یا اعداد باشد"
                                    ValidationGroup="Valid"
                                    Display="Dynamic" CssClass="badge bg-label-danger"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-password-toggle">
                                <label class="form-label" for="multicol-confirm-password">تایید رمز عبور</label>
                                <div class="input-group input-group-merge">
                                    <input type="password" id="txtRePassword" validationgroup="Valid" runat="server" class="form-control text-start" dir="ltr" placeholder="············" aria-describedby="multicol-confirm-password2">
                                    <span class="input-group-text cursor-pointer" id="multicol-confirm-password2"><i class="bx bx-hide"></i></span>
                                </div>
                                <asp:RequiredFieldValidator CssClass="badge bg-label-danger" ValidationGroup="Valid" Display="Dynamic" ID="RequiredFieldValidatorTextBoxConfirmPassword"
                                    runat="server" ErrorMessage="تکرار رمز جدید الزامیست" ControlToValidate="txtRePassword"></asp:RequiredFieldValidator>
                                <asp:CompareValidator CssClass="badge bg-label-danger" Display="Dynamic" ID="CompareValidatorTextBoxConfirmPassword"
                                    runat="server" ErrorMessage="رمزجدید و تکرار آن یکسان نیستند." ControlToCompare="txtPassword"
                                    ValidationGroup="Valid" ControlToValidate="txtRePassword"></asp:CompareValidator>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="CaptchaCodeTextBoxLabel" CssClass="form-label" runat="server" AssociatedControlID="CaptchaCodeTextBox">کد امنیتی</asp:Label>
                        <a href="" class="w-100">
                            <BotDetect:WebFormsCaptcha ID="BotCaptcha" ImageSample runat="server" />
                        </a>
                        <br />
                        <asp:TextBox runat="server" ID="CaptchaCodeTextBox" ValidationGroup="Valid" MaxLength="6" placeholder="کد امنیتی" CssClass="form-control w-50" />
                        <asp:RequiredFieldValidator ID="rfvFieldCaptcha" runat="server" ControlToValidate="CaptchaCodeTextBox" ErrorMessage="کد امنیتی الزامی است" Display="Dynamic" CssClass="text-danger" ValidationGroup="Valid"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="CaptchaCodeTextBox" ErrorMessage="تعداد کارکتر های مجاز بیشتر از حد مجاز است" ValidationExpression="^.{0,10}$"
                            Display="Dynamic" CssClass="badge bg-label-danger" ValidationGroup="Valid"></asp:RegularExpressionValidator>
                        <label id="rfvInvalidCode" visible="false" runat="server" class="badge bg-label-danger">کد امنیتی وارد شده نادرست است</label>

                    </div>
                    <hr class="my-4 mx-n4">
                    <div class="pt-4">
                        <asp:Button ID="btnSubmit" ValidationGroup="Valid" CssClass="btn btn-primary me-sm-3 me-1" runat="server" Text="ثبت" OnClick="btnSubmit_Click" />
                        <a href="./ChangePassword" class="btn btn-label-secondary">بازگشت</a>
                    </div>
                    <script>window.history.replaceState(null, null, window.location.href.split('?')[0]);</script>
                </form>
            </div>
        </div>
    </div>
</div>
