<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SystemSettings.ascx.cs" Inherits="UniversityLibrarySystem.Manager.SystemSettings" %>
<form runat="server">
    <!-- Content -->
    <div class="container-xxl flex-grow-1 container-p-y">
        <h4 id="head-title" class="py-3 breadcrumb-wrapper mb-4">تنظیمات سامانه</h4>
        <asp:Literal ID="lblMessage" runat="server" Text="" Visible="False"></asp:Literal>
        <div class="col-12">
            <div class="card mb-4">
                <div class="card-body">
                    <div class="row">
                        <!-- Site Name -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="siteName">نام سایت</label>
                            <asp:TextBox runat="server" ID="txtSiteName" CssClass="form-control" placeholder="نام سایت"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtSiteName" ErrorMessage="نام سایت معتبر نیست" ValidationExpression=".{1,}" CssClass="badge bg-label-danger"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSiteName" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator5">این قسمت الزامی است</asp:RequiredFieldValidator>
                        </div>

                        <!-- Telephone -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="telephone">تلفن</label>
                            <asp:TextBox runat="server" ID="txtTelephone" CssClass="form-control" placeholder="تلفن"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTelephone" ErrorMessage="شماره تلفن نامعتبر است" ValidationExpression="^\d{10,15}$" CssClass="badge bg-label-danger"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelephone" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است" ToolTip="این قسمت الزامی است" ID="RequiredFieldValidator6">این قسمت الزامی است</asp:RequiredFieldValidator>
                        </div>

                        <!-- Email -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="email">ایمیل</label>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="ایمیل"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="ایمیل نامعتبر است" ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,6}$" CssClass="badge bg-label-danger"></asp:RegularExpressionValidator>
                        </div>

                        <!-- Address -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="address">آدرس</label>
                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" placeholder="آدرس"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtAddress" ErrorMessage="آدرس معتبر نیست" ValidationExpression=".{1,}" CssClass="badge bg-label-danger"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtAddress" runat="server" CssClass="form-control" ErrorMessage="این قسمت الزامی است"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Map Link -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="mapLink">لینک نقشه</label>
                            <asp:TextBox runat="server" ID="txtMapLink" CssClass="form-control" placeholder="لینک نقشه"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMapLink" ErrorMessage="لینک نقشه نامعتبر است" ValidationExpression="https?://.+" CssClass="badge bg-label-danger"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtMapLink" runat="server" CssClass="form-control" ErrorMessage="این قسمت الزامی است"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Short About Us -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="shortAboutUs">خلاصه درباره ما</label>
                            <asp:TextBox runat="server" ID="txtShortAboutUs" TextMode="MultiLine" CssClass="form-control" placeholder="خلاصه درباره ما"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtShortAboutUs" runat="server" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است"></asp:RequiredFieldValidator>
                        </div>

                        <!-- About Us -->
                        <div class="col-xl-12 col-md-12 col-sm-12 mb-4">
                            <label class="form-label" for="aboutUs">درباره ما</label>
                            <asp:TextBox runat="server" ID="txtAboutUs" Height="300px" TextMode="MultiLine" CssClass="form-control" placeholder="درباره ما"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAboutUs" runat="server" CssClass="badge bg-label-danger" ErrorMessage="این قسمت الزامی است"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Reserve Day -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="reserveDay">تعداد روز رزرو</label>
                            <asp:TextBox runat="server" ID="txtReserveDay" TextMode="Number" CssClass="form-control" MaxLength="365" placeholder="روز رزرو"></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="RangeValidator3" ControlToValidate="txtReserveDay" MinimumValue="1" MaximumValue="50" Type="Integer" ErrorMessage="لطفاً مقداری بین 1 و 365 وارد کنید." CssClass="badge bg-label-danger"></asp:RangeValidator>
                        </div>

                        <!-- Max User Reserve -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="maxUserReserve">حداکثر رزرو کاربران</label>
                            <asp:TextBox runat="server" ID="txtMaxUserReserve" TextMode="Number" MaxLength="50" CssClass="form-control" placeholder="حداکثر رزرو کاربران"></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="rvMaxUserReserve" ControlToValidate="txtMaxUserReserve" MinimumValue="1" MaximumValue="50" Type="Integer" ErrorMessage="لطفاً مقداری بین 1 و 50 وارد کنید." CssClass="badge bg-label-danger"></asp:RangeValidator>

                        </div>

                        <!-- Reserve Again -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="reserveAgain">تمدید</label>
                            <asp:TextBox runat="server" ID="txtReserveAgain" TextMode="Number" MaxLength="50" CssClass="form-control" placeholder="تعداد مجاز تمدید"></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="RangeValidator1" ControlToValidate="txtReserveAgain" MinimumValue="1" MaximumValue="50" Type="Integer" ErrorMessage="لطفاً مقداری بین 1 و 50 وارد کنید." CssClass="badge bg-label-danger"></asp:RangeValidator>

                        </div>

                        <!-- Liability -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <label class="form-label" for="liability">بدهی(واحد پول ريال)</label>
                            <asp:TextBox runat="server" ID="txtLiability" TextMode="Number" CssClass="form-control" placeholder="بدهی"></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="RangeValidator2" ControlToValidate="txtLiability" MinimumValue="1" MaximumValue="999999999" Type="Integer" ErrorMessage="لطفاً مقداری بین 1 و 999999999 وارد کنید." CssClass="badge bg-label-danger"></asp:RangeValidator>
                        </div>

                        <!-- Status -->
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <div class="form-check form-switch mb-2">
                                <input type="checkbox" id="chkSiteStatus" class="form-check-input" runat="server" />
                                <label for="s1-14">وضعیت</label>
                            </div>
                        </div>
                        <div class="col-xl-6 col-md-6 col-sm-12 mb-4">
                            <div class="form-check form-switch mb-2">
                                <input type="checkbox" id="chkres" class="form-check-input" runat="server" />
                                <label for="s1-14"><%=Classes.ReserveStatus.ReserveStatusToText(Classes.ReserveStatus.TempReservation) %></label>
                            </div>
                        </div>
                        <hr class="my-4 mx-n4">
                        <div class="pt-4">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary me-sm-3 me-1" runat="server" Text="بروزرسانی" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>
