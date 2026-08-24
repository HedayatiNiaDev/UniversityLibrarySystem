<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="UniversityLibrarySystem.Manager.Dashboard" %>
<form runat="server" class="px-2">
    <style type="text/css">
        .loader {
            text-align: center;
            position: sticky;
            top: 75px;
            z-index: 1000;
        }

        svg {
            position: absolute;
            width: 2.25em;
            transform-origin: center;
            animation: rotate4 2s linear infinite;
            background-color: transparent;
            color: transparent;
            background-color: var(--custom-color-bg-menu-item);
            border-radius: 50%;
            padding: 5px
        }

        circle {
            fill: none;
            stroke: hsl(214, 97%, 59%);
            stroke-width: 7;
            stroke-dasharray: 1, 200;
            stroke-dashoffset: 0;
            stroke-linecap: round;
            animation: dash4 1.5s ease-in-out infinite;
            background-color: transparent;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                <ProgressTemplate>
                    <div class="loader">
                        <svg viewBox="25 25 50 50">
                            <circle r="20" cy="50" cx="50"></circle>
                        </svg>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="col-12 px-2 mb-4">
                <div class="row">
                    <h4>آمار حساب های کاربری</h4>
                    <div class="col-12 col-md-6 col-lg-3 mb-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="d-flex justify-content-between" style="position: relative;">
                                    <div class="d-flex align-items-center gap-3">
                                        <div class="avatar">
                                            <span class="avatar-initial bg-label-success rounded-circle"><i class="bx bx-group fs-4"></i></span>
                                        </div>
                                        <div class="card-info">
                                            <h5 class="card-title mb-0 me-2 primary-font"><%=allAccountCounter()%></h5>
                                            <small class="text-muted">تعداد کل حساب ها</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-6 col-lg-3 mb-4">
                        <div class="card">
                            <a href="Manager" class="card-body">
                                <div class="d-flex justify-content-between" style="position: relative;">
                                    <div class="d-flex align-items-center gap-3">
                                        <div class="avatar">
                                            <span class="avatar-initial bg-label-success rounded-circle"><i class="bx bx-user fs-4"></i></span>
                                        </div>
                                        <div class="card-info">
                                            <h5 class="card-title mb-0 me-2 primary-font"><%=PersonalCounter() %></h5>
                                            <small class="text-muted">تعداد حساب های مدیران</small>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-12 col-md-6 col-lg-3 mb-4">
                        <div class="card">
                            <a href="Students" class="card-body">
                                <div class="d-flex justify-content-between" style="position: relative;">
                                    <div class="d-flex align-items-center gap-3">
                                        <div class="avatar">
                                            <span class="avatar-initial bg-label-primary rounded-circle"><i class="bx bx-user fs-4"></i></span>
                                        </div>
                                        <div class="card-info">
                                            <h5 class="card-title mb-0 me-2 primary-font"><%=allActiveUserCounter()%></h5>
                                            <small class="text-muted">تعداد حساب های دانشجویان</small>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-12 col-md-6 col-lg-3 mb-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="d-flex justify-content-between" style="position: relative;">
                                    <div class="d-flex align-items-center gap-3">
                                        <div class="avatar">
                                            <span class="avatar-initial bg-label-danger rounded-circle"><i class="bx bx-user fs-4"></i></span>
                                        </div>
                                        <div class="card-info">
                                            <h5 class="card-title mb-0 me-2 primary-font"><%=allDeactiveCounter() %></h5>
                                            <small class="text-muted">تعداد حساب های غیر فعال</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 px-2 mb-4">
                <div class="row">
                    <h4>آمار کتاب ها</h4>
                    <div class="col-12 col-md-6 col-lg-3 mb-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="d-flex justify-content-between" style="position: relative;">
                                    <div class="d-flex align-items-center gap-3">
                                        <div class="avatar">
                                            <span class="avatar-initial bg-label-success rounded-circle"><i class="bx bx-book fs-4"></i></span>
                                        </div>
                                        <div class="card-info">
                                            <h5 class="card-title mb-0 me-2 primary-font"><%=allBookCounter()%></h5>
                                            <small class="text-muted">کتاب های قابل رزرو در کتاب خانه</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-6 col-lg-3 mb-4">
                        <a href="Reservations?Mode=1" class="card">
                            <div class="card-body">
                                <div class="d-flex justify-content-between" style="position: relative;">
                                    <div class="d-flex align-items-center gap-3">
                                        <div class="avatar">
                                            <span class="avatar-initial bg-label-success rounded-circle"><i class="bx bx-user fs-4"></i></span>
                                        </div>
                                        <div class="card-info">
                                            <h5 class="card-title mb-0 me-2 primary-font"><%=TempReserve() %></h5>
                                            <small class="text-muted">کتاب های رزرو موقت</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-12 col-md-6 col-lg-3 mb-4">
                        <a href="Reservations?Mode=2" class="card">
                            <div class="card-body">
                                <div class="d-flex justify-content-between" style="position: relative;">
                                    <div class="d-flex align-items-center gap-3">
                                        <div class="avatar">
                                            <span class="avatar-initial bg-label-primary rounded-circle"><i class="bx bx-book fs-4"></i></span>
                                        </div>
                                        <div class="card-info">
                                            <h5 class="card-title mb-0 me-2 primary-font"><%=Reserve()%></h5>
                                            <small class="text-muted">کتاب های <%=Classes.ReserveStatus.ReserveStatusToText(Classes.ReserveStatus.Reservation) %></small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-12 col-md-6 col-lg-3 mb-4">
                        <a href="Reservations?Mode=3" class="card">
                            <div class="card-body">
                                <div class="d-flex justify-content-between" style="position: relative;">
                                    <div class="d-flex align-items-center gap-3">
                                        <div class="avatar">
                                            <span class="avatar-initial bg-label-danger rounded-circle"><i class="bx bx-user fs-4"></i></span>
                                        </div>
                                        <div class="card-info">
                                            <h5 class="card-title mb-0 me-2 primary-font"><%=Fine() %></h5>
                                            <small class="text-muted">کتاب های در حال جریمه</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-12 px-2 mb-4">
                <div class="row">
                    <h4>آمار بیشترین رزرو ها</h4>
                    <div>
                        <div class="table-responsive card text-nowrap">
                            <table class="table table-striped">
                                <thead>
                                    <tr>
                                        <th>درج کننده</th>
                                        <th>تصویر کتاب</th>
                                        <th>عنوان</th>
                                        <th>دسته بندی</th>
                                        <th>تعداد موجود</th>
                                        <th>وضعیت</th>
                                        <th>مشاهده کتاب</th>
                                    </tr>
                                </thead>
                                <tbody class="table-border-bottom-0">
                                    <asp:ListView ID="ProuductDefualt" runat="server" DataSourceID="LinqDataSourceNews"
                                        ItemPlaceholderID="brandplacea42">
                                        <LayoutTemplate>
                                            <asp:PlaceHolder ID="brandplacea42" runat="server"></asp:PlaceHolder>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <ul class="list-unstyled users-list m-0 avatar-group d-flex align-items-center">
                                                        <li data-bs-toggle="tooltip" data-popup="tooltip-custom" data-bs-placement="top" class="avatar avatar-xs pull-up" title="" data-bs-original-title="<%#Eval("NameFamily")%>">
                                                            <img src="../img/users/<%#Eval("AdderPic")%>" style="min-width: 26px;" alt="Avatar" class="rounded-circle">
                                                        </li>
                                                        <a style="color: #000;" href="./Manager?Edit=<%# ID2hash(Eval("UserID"))%>"><%#Eval("NameFamily")%></a>
                                                    </ul>
                                                </td>
                                                <td>
                                                    <div class="avatar mr-1 avatar-xl">
                                                        <img class="example-image" onerror="this.src='../img/Error/no-photo.png'"
                                                            src='../img/Books/<%#Eval("PicName")%>' alt="" /></a>
                                                    </div>
                                                </td>
                                                <td><%#Eval("BookTitle")%></td>
                                                <td><%#Eval("CatName")%></td>
                                                <td><%#Eval("Available")%></td>
                                                <td><%#Eval("StatusHtml") %></td>
                                                <td>
                                                    <a class="btn btn-primary" href="../BookDetail-<%#Eval("BookID")%>-<%#Uri.EscapeDataString(Eval("BookTitle").ToString())%>"></i>مشاهده</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <asp:LinqDataSource ID="LinqDataSourceNews" runat="server" OnSelecting="LinqDataSourceNews_Selecting">
                                    </asp:LinqDataSource>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <p class="m-2">بروز شده در <%=DateTime.Now.ToShortTimeString() %></p>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>

    <!-- تایمر برای بروزرسانی هر 60 ثانیه -->
    <asp:Timer ID="Timer1" runat="server" Enabled="true" Interval="60000" OnTick="Timer1_Tick"></asp:Timer>
</form>
