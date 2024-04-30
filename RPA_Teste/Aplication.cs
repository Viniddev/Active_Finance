using System.Diagnostics;
using System;
using System.IO;

namespace RPA_Teste
{
    internal class Aplication
    {
        public static int ContadorLimiteTempo { get; set; }
        public static async Task Contador()
        {
            Task cont = Task.Run(async () =>
            {
                while (ContadorLimiteTempo < 600)
                {
                    Console.WriteLine(ContadorLimiteTempo);
                    ContadorLimiteTempo++;
                    await Task.Delay(1000);
                }
            });

            await cont;

            KillChromeDriver();
            Process processo = Process.GetCurrentProcess();
            processo.CloseMainWindow();
            processo.WaitForExit();
            Environment.Exit(1);
        }
        public static bool EhPeriodoUtil()
        {
            DateTime ProcessStart = DateTime.Now;

            if (ProcessStart.Hour < 10 || ProcessStart.Hour >= 17)
                return false;

            if (ProcessStart.DayOfWeek.ToString().Equals("Saturday") || ProcessStart.DayOfWeek.ToString().Equals("Sunday"))
                return false;

            return true;
        }
        public static void OnApplicationExit(object sender, EventArgs e)
        {

            KillChromeDriver();

        }
        public static int KillChromeDriver()
        {

            var i = Process.GetProcessesByName("ChromeDriver");
            int exitCode = 400;
            if (i.Length > 0)
            {
                string command = "taskkill /F /IM chromedriver.exe";
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + command;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                exitCode = process.ExitCode;
                process.Close();
            }


            var p = Process.GetProcesses();
            foreach (Process item in p)
            {
                if (item.ProcessName.ToUpper().Contains("CHROME") || item.ProcessName.Contains("MSEDGE") || item.ProcessName.Contains("IEXPLORE"))
                {
                    item.Kill();
                }
            }


            string tempfolder = @"C:\Selenium\Scope";
            if (!Directory.Exists(tempfolder))
                Directory.CreateDirectory(tempfolder);

            try
            {
                string[] tempfiles = Directory.GetDirectories(tempfolder, "scoped *", SearchOption.AllDirectories);
                foreach (var item in tempfiles)
                {
                    if (Directory.Exists(item))
                        Directory.Delete(item, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return exitCode;
        }
    }
}
