<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Permission.ascx.cs" Inherits="UniversityLibrarySystem.Manager.Permission" %>

<style type="text/css">
    .overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: var(--custom-color-bg-menu-item);
        opacity: 0.8;
        z-index: 9999;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    /* From Uiverse.io by barisdogansutcu */
    svg {
        width: 3.25em;
        transform-origin: center;
        animation: rotate4 2s linear infinite;
    }

    circle {
        fill: none;
        stroke: hsl(214, 97%, 59%);
        stroke-width: 2;
        stroke-dasharray: 1, 200;
        stroke-dashoffset: 0;
        stroke-linecap: round;
        animation: dash4 1.5s ease-in-out infinite;
        background: none;
    }

    @keyframes rotate4 {
        100% {
            transform: rotate(360deg);
        }
    }

    @keyframes dash4 {
        0% {
            stroke-dasharray: 1, 200;
            stroke-dashoffset: 0;
        }

        50% {
            stroke-dasharray: 90, 200;
            stroke-dashoffset: -35px;
        }

        100% {
            stroke-dashoffset: -125px;
        }
    }

    svg {
        width: 3.25em;
        transform-origin: center;
        animation: rotate4 2s linear infinite;
        background-color:transparent;
        color:transparent;
    }

    circle {
        fill: none;
        stroke: hsl(214, 97%, 59%);
        stroke-width: 5;
        stroke-dasharray: 1, 200;
        stroke-dashoffset: 0;
        stroke-linecap: round;
        animation: dash4 1.5s ease-in-out infinite;
        background-color:transparent;
    }

    @keyframes rotate4 {
        100% {
            transform: rotate(360deg);
        }
    }

    @keyframes dash4 {
        0% {
            stroke-dasharray: 1, 200;
            stroke-dashoffset: 0;
        }

        50% {
            stroke-dasharray: 90, 200;
            stroke-dashoffset: -35px;
        }

        100% {
            stroke-dashoffset: -125px;
        }
    }
</style>
<style>
    /* استایل برای تبدیل چک‌باکس به سوئیچ */
    #Permission_CheckBoxListPermission input[type="checkbox"] {
        -webkit-appearance: none;
        -moz-appearance: none;
        appearance: none;
        width: 50px;
        height: 25px;
        background-color: #ccc;
        border-radius: 25px;
        position: relative;
        cursor: pointer;
        outline: none;
        border: 2px solid transparent;
        transition: background-color 0.3s, border-color 0.3s;
    }

        #Permission_CheckBoxListPermission input[type="checkbox"]:before {
            content: "";
            width: 20px;
            height: 90%;
            background-color: white;
            border-radius: 50%;
            position: absolute;
            top: 1.2px; /* دایره را در وسط قرار می‌دهد */
            left: 2px; /* دایره را در وسط قرار می‌دهد */
            transition: transform 0.3s;
        }

        #Permission_CheckBoxListPermission input[type="checkbox"]:checked {
            background-color: #275EFE; /* رنگ آبی */
        }

            #Permission_CheckBoxListPermission input[type="checkbox"]:checked:before {
                transform: translateX(25px);
            }

        /* استایل برای حالت غیرفعال */
        #Permission_CheckBoxListPermission input[type="checkbox"]:disabled {
            background-color: #e6e6e6;
            cursor: not-allowed;
        }

            #Permission_CheckBoxListPermission input[type="checkbox"]:disabled:before {
                background-color: #ccc;
            }
    /* تنظیم موقعیت label برای تراز شدن با سوئیچ */
    #Permission_CheckBoxListPermission label {
        display: inline-block;
        vertical-align: middle;
        margin-right: 5px;
        margin-bottom: 3.5%;
    }
</style>

<script type="text/javascript">

    $(document).ready(function () {
        // attach the event to the button control 
        $("#btn_CheckAll").click(function () {
            // i want to check all the checkboxes contained in the chkList control               
            $("#chkList").find("input[@type=checkbox]").each(function () {
                if (!this.checked) {
                    this.checked = true;
                    return;
                }
                this.checked = false;
            });
        });
    });
</script>

<style>
    .mainFull {
        position: fixed;
        padding: 0;
        margin: 0;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: rgba(255,255,255,0.5);
    }
</style>
<div class="content-wrapper">
    <!-- Content -->

    <div class="container-xxl flex-grow-1 container-p-y">
        <h4 class="py-3 breadcrumb-wrapper mb-4">سطح دسترسی مدیران
        </h4>
        <div class="mb-0">
            <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>
        </div>
        <form runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <!-- Striped Rows -->
            <div class="col-12">
                <div class="card mb-4">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-12 mb-2">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="lbAll" runat="server" OnClick="lbAll_Click">انتخاب همه</asp:LinkButton>
                                        <asp:LinkButton ID="lbNone" runat="server" OnClick="lbNone_Click">/ برداشتن</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <br />
                                <label class="form-label" for="phone-number-mask">نام مدیر:</label>
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList CssClass="form-select" ID="drpPersonal" name="drpPersonal" ValidationGroup="Filds"
                                                runat="server" DataSourceID="LinqDataSourceUserName" DataTextField="PersonalName"
                                                DataValueField="UserName" AutoPostBack="True" OnSelectedIndexChanged="drpPersonal_SelectedIndexChanged" AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True" Text="لطفا یک حساب کاربری را انتخاب کنید" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblUN" runat="server" Text=""></asp:Label>
                                            <asp:LinqDataSource ID="LinqDataSourceUserName" runat="server" OnSelecting="LinqDataSourceUserName_Selecting">
                                            </asp:LinqDataSource>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="CheckBoxListPermission" runat="server" CssClass="checkbox"
                                            DataValueField="ID" DataTextField="PageNameForDisplay" DataSourceID="LinqDataSourcePermission" Style="width: 400px;"
                                            AutoPostBack="True">
                                        </asp:CheckBoxList>
                                        <asp:LinqDataSource ID="LinqDataSourcePermission" runat="server" OnSelecting="LinqDataSourcePermission_Selecting">
                                        </asp:LinqDataSource>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div class="overlay">
                                        <center>
                                            <p>لطفا منتظر باشید...</p>
                                            <svg viewBox="25 25 50 50">
                                                <circle r="20" cy="50" cx="50"></circle>
                                            </svg>
                                        </center>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                            <hr class="my-4 mx-n4">
                            <div class="pt-4">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnSubmit" ValidationGroup="Valid" CssClass="btn btn-primary me-sm-3 me-1" runat="server" Text="ثبت" Enabled="false" OnClick="btnSubmit_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--/ Striped Rows -->

        </form>
    </div>
    <!-- / Content -->
    <div class="content-backdrop fade"></div>
</div>
