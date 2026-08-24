using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Web.UI.WebControls;

namespace UniversityLibrarySystem.Manager
{
    public partial class ServerMonitor : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateServerStats();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            UpdateServerStats();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateServerStats();
        }

        private void UpdateServerStats()
        {
            try
            {
                // اطلاعات CPU
                float cpuUsage = GetCPUUsage();
                string cpuUsageFormatted = string.Format("{0:0.0}%", cpuUsage);

                lblCPU.Text = cpuUsageFormatted;
                lblCPUDetail.Text = string.Format("{0} در حال استفاده", cpuUsageFormatted);

                // اطلاعات RAM
                float ramUsage = GetRAMUsage();
                double usedRAM = GetUsedRAMInMB();
                double totalRAM = GetTotalRAMInMB();
                string ramUsageFormatted = string.Format("{0:0.0}%", ramUsage);

                lblRAM.Text = ramUsageFormatted;
                lblRAMDetail.Text = string.Format("{0:0.0} GB از {1:0.0} GB",
                    usedRAM / 1024, totalRAM / 1024);

                // اطلاعات Disk
                string driveLetter = "C";
                float diskUsage = GetDiskUsage(driveLetter);
                double freeDiskSpace = GetFreeDiskSpaceInGB(driveLetter);
                double totalDiskSpace = GetTotalDiskSpaceInGB(driveLetter);
                double usedDiskSpace = totalDiskSpace - freeDiskSpace;
                string diskUsageFormatted = string.Format("{0:0.0}%", diskUsage);

                lblDisk.Text = diskUsageFormatted;
                lblDiskDetail.Text = string.Format("{0:0.0} GB از {1:0.0} GB استفاده شده",
                    usedDiskSpace, totalDiskSpace);

                // زمان بروزرسانی
                lblLastUpdate.Text = DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                // مدیریت خطا - در صورت نیاز می‌توان پیام خطا را نمایش داد
            }
        }

        private float GetCPUUsage()
        {
            try
            {
                PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                cpuCounter.NextValue(); // اولین مقدار همیشه صفر است
                System.Threading.Thread.Sleep(500); // نیم ثانیه صبر کنید
                return cpuCounter.NextValue();
            }
            catch
            {
                return 0;
            }
        }

        private float GetRAMUsage()
        {
            try
            {
                ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);
                ManagementObjectCollection results = searcher.Get();

                double freePhysicalMemory = 0;
                double totalVisibleMemorySize = 0;

                foreach (ManagementObject result in results)
                {
                    freePhysicalMemory = Convert.ToDouble(result["FreePhysicalMemory"]);
                    totalVisibleMemorySize = Convert.ToDouble(result["TotalVisibleMemorySize"]);
                }

                double usedMemory = totalVisibleMemorySize - freePhysicalMemory;
                return (float)((usedMemory / totalVisibleMemorySize) * 100);
            }
            catch
            {
                return 0;
            }
        }

        private double GetUsedRAMInMB()
        {
            try
            {
                ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);
                ManagementObjectCollection results = searcher.Get();

                double freePhysicalMemory = 0;
                double totalVisibleMemorySize = 0;

                foreach (ManagementObject result in results)
                {
                    freePhysicalMemory = Convert.ToDouble(result["FreePhysicalMemory"]);
                    totalVisibleMemorySize = Convert.ToDouble(result["TotalVisibleMemorySize"]);
                }

                // تبدیل از KB به MB
                double usedMemory = (totalVisibleMemorySize - freePhysicalMemory) / 1024;
                return Math.Round(usedMemory, 2);
            }
            catch
            {
                return 0;
            }
        }

        private double GetTotalRAMInMB()
        {
            try
            {
                ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);
                ManagementObjectCollection results = searcher.Get();

                double totalVisibleMemorySize = 0;

                foreach (ManagementObject result in results)
                {
                    totalVisibleMemorySize = Convert.ToDouble(result["TotalVisibleMemorySize"]);
                }

                // تبدیل از KB به MB
                return Math.Round(totalVisibleMemorySize / 1024, 2);
            }
            catch
            {
                return 0;
            }
        }

        private float GetDiskUsage(string driveLetter)
        {
            try
            {
                DriveInfo drive = new DriveInfo(driveLetter);
                if (drive.IsReady)
                {
                    double totalSize = drive.TotalSize;
                    double freeSpace = drive.AvailableFreeSpace;
                    double usedSpace = totalSize - freeSpace;
                    return (float)((usedSpace / totalSize) * 100);
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private double GetFreeDiskSpaceInGB(string driveLetter)
        {
            try
            {
                DriveInfo drive = new DriveInfo(driveLetter);
                if (drive.IsReady)
                {
                    return Math.Round(drive.AvailableFreeSpace / (1024 * 1024 * 1024.0), 2);
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private double GetTotalDiskSpaceInGB(string driveLetter)
        {
            try
            {
                DriveInfo drive = new DriveInfo(driveLetter);
                if (drive.IsReady)
                {
                    return Math.Round(drive.TotalSize / (1024 * 1024 * 1024.0), 2);
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}