<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServerMonitor.ascx.cs" Inherits="UniversityLibrarySystem.Manager.ServerMonitor" %>
<title>پنل مدیریت منابع سرور</title>
<style>
    * {
        margin: 0;
        padding: 0;
        box-sizing: border-box;
    }

    .container {
        max-width: 1200px;
        margin: 20px auto;
        padding: 0 20px;
    }

    .section-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 15px;
        padding: 0 10px;
    }

    .section-title {
        font-size: 20px;
        font-weight: 500;
    }

    .cards-container {
        display: flex;
        flex-wrap: wrap;
        gap: 20px;
        margin-bottom: 40px;
    }

    .stat-card {
        border-radius: 7px;
        background: var(--custom-color-bg-menu-item);
        box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
        padding: 20px;
        flex: 1;
        min-width: 200px;
        position: relative;
        overflow: hidden;
        transition: transform 0.2s, box-shadow 0.2s;
        display: flex;
        align-items: center;
    }

    .stat-icon {
        width: 50px;
        height: 50px;
        font-size:20px;
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-left: 15px;
    }

    .cpu-icon {
        background-color: rgba(244, 67, 54, 0.1);
        color: #f44336;
    }

    .ram-icon {
        background-color: rgba(33, 150, 243, 0.1);
        color: #2196f3;
    }

    .disk-icon {
        background-color: rgba(76, 175, 80, 0.1);
        color: #4caf50;
    }

    .stat-content {
        flex: 1;
    }

    .stat-value {
        font-size: 28px;
        font-weight: 700;
        margin-bottom: 5px;
        letter-spacing: -0.5px;
    }

    .stat-label {
        color: #666;
        font-size: 14px;
    }

    .chart-container {
        background: var(--custom-color-bg-menu-item);
        border-radius: 7px;
        box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
        padding: 20px;
        margin-bottom: 40px;
    }

    .chart-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 20px;
    }

    .chart-title {
        font-size: 18px;
        font-weight: 500;
    }

    .resource-details {
        display: flex;
        flex-direction: column;
        gap: 10px;
        margin-bottom: 20px;
    }

    .resource-item {
        display: flex;
        justify-content: space-between;
        padding: 10px 0;
        border-bottom: 1px solid #f0f0f0;
    }

    .resource-name {
        display: flex;
        align-items: center;
    }

    .resource-indicator {
        width: 8px;
        height: 8px;
        border-radius: 50%;
        margin-left: 8px;
    }

    .cpu-color {
        background-color: #f44336;
    }

    .ram-color {
        background-color: #2196f3;
    }

    .disk-color {
        background-color: #4caf50;
    }

    .resource-value {
        font-weight: 500;
    }

    .progress-container {
        margin-bottom: 15px;
        padding: 10px 0;
    }

    .progress-label {
        display: flex;
        justify-content: space-between;
        margin-bottom: 8px;
    }

    .progress-bar {
        height: 10px;
        background-color: #f0f0f0;
        border-radius: 50px;
        overflow: hidden;
    }

    .progress-fill {
        height: 100%;
        border-radius: 50px;
        transition: width 0.8s ease;
    }

    .cpu-fill {
        background-color: #f44336;
    }

    .ram-fill {
        background-color: #2196f3;
    }

    .disk-fill {
        background-color: #4caf50;
    }

    .footer {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 15px 0;
        font-size: 14px;
    }

    .refresh-button {
        background-color: #f8f9fa;
        border: 1px solid #dadce0;
        color: #3c4043;
        padding: 8px 16px;
        border-radius: 7px;
        cursor: pointer;
        font-size: 14px;
        transition: background-color 0.2s;
        display: flex;
        align-items: center;
    }

        .refresh-button:hover {
            background-color: #f1f3f4;
        }

        .refresh-button i {
            margin-left: 6px;
        }
</style>
<form id="form1" runat="server">
    <div class="container">
        <div class="section-header">
            <div class="section-title">آمار منابع سرور<span class="mx-1" style="display:inline-block;padding:0.25em 0.6em;font-size:0.75rem;font-weight:600;color:#fff;background-color:#007bff;border-radius:1rem;">آزمایشی</span></div>
            <div>
                آخرین بروزرسانی: 
                    <asp:Label ID="lblLastUpdate" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- کارت‌های آمار -->
                <div class="cards-container">
                    <!-- کارت CPU -->
                    <div class="stat-card">
                        <div class="stat-icon cpu-icon">
                            <i class="fas fa-microchip"></i>
                        </div>
                        <div class="stat-content">
                            <div class="stat-value">
                                <asp:Label ID="lblCPU" runat="server" Text="0%"></asp:Label>
                            </div>
                            <div class="stat-label">میزان استفاده از پردازنده</div>
                        </div>
                    </div>

                    <!-- کارت RAM -->
                    <div class="stat-card">
                        <div class="stat-icon ram-icon">
                            <i class="fas fa-memory"></i>
                        </div>
                        <div class="stat-content">
                            <div class="stat-value">
                                <asp:Label ID="lblRAM" runat="server" Text="0%"></asp:Label>
                            </div>
                            <div class="stat-label">میزان استفاده از حافظه</div>
                        </div>
                    </div>

                    <!-- کارت Disk -->
                    <div class="stat-card">
                        <div class="stat-icon disk-icon">
                            <i class="fas fa-hdd"></i>
                        </div>
                        <div class="stat-content">
                            <div class="stat-value">
                                <asp:Label ID="lblDisk" runat="server" Text="0%"></asp:Label>
                            </div>
                            <div class="stat-label">میزان استفاده از دیسک</div>
                        </div>
                    </div>
                </div>

                <!-- نمودارهای پیشرفت -->
                <div class="chart-container">
                    <div class="chart-header">
                        <div class="chart-title">جزئیات منابع</div>
                    </div>

                    <div class="resource-details">
                        <!-- جزئیات CPU -->
                        <div class="resource-item">
                            <div class="resource-name">
                                <span class="resource-indicator cpu-color"></span>
                                <span>پردازنده (CPU)</span>
                            </div>
                            <div class="resource-value">
                                <asp:Label ID="lblCPUDetail" runat="server" Text="0% در حال استفاده"></asp:Label>
                            </div>
                        </div>

                        <!-- جزئیات RAM -->
                        <div class="resource-item">
                            <div class="resource-name">
                                <span class="resource-indicator ram-color"></span>
                                <span>حافظه (RAM)</span>
                            </div>
                            <div class="resource-value">
                                <asp:Label ID="lblRAMDetail" runat="server" Text="0 MB از 0 MB"></asp:Label>
                            </div>
                        </div>

                        <!-- جزئیات Disk -->
                        <div class="resource-item">
                            <div class="resource-name">
                                <span class="resource-indicator disk-color"></span>
                                <span>فضای دیسک (Disk)</span>
                            </div>
                            <div class="resource-value">
                                <asp:Label ID="lblDiskDetail" runat="server" Text="0 GB از 0 GB"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="footer">
                    <div>به‌روزرسانی خودکار هر 5 ثانیه</div>
                    <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
                    <asp:Button ID="btnRefresh" runat="server" Text="بروزرسانی" OnClick="btnRefresh_Click" CssClass="btn btn-primary" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</form>
