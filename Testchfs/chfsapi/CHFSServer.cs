using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Testchfs.chfsapi
{
    public class CHFSServer
    {
        public static string chfsuser = "admin";
        public static string chfspwd = "admin123";

        public static void Start()
        {
            #region
            Process[] process = Process.GetProcesses();//获取当前任务管理器所有运行中程序
            foreach (Process proces in process)//遍历
            {
                if (proces.ProcessName.Equals("chfs"))
                {
                    Console.WriteLine(proces.ProcessName);
                    proces.Kill();
                }

            }

            string path = Path.Combine(System.Environment.CurrentDirectory, "chfs.exe");

            int chfsport = 8001;

            runBat(path, string.Format(
                "--port={0} --rule=\"::|{1}:{2}:rw\"",
                chfsport, chfsuser, chfspwd));

            CHFSApi.init("localhost.", chfsport);
            #endregion
        }

        private static void runBat(
    string batPath, string args = "")
        {
            #region
            Process pro = new Process();
            FileInfo file = new FileInfo(batPath);
            pro.StartInfo.WorkingDirectory = file.Directory.FullName;

            pro.StartInfo.FileName = batPath;
            pro.StartInfo.UseShellExecute = false;//是否重定向标准输入 
            pro.StartInfo.RedirectStandardInput = false;//是否重定向标准转出 
            pro.StartInfo.RedirectStandardOutput = false;//是否重定向错误 
            pro.StartInfo.RedirectStandardError = false;//执行时是不是显示窗口 
            pro.StartInfo.CreateNoWindow = true;//启动 

            if (!string.IsNullOrEmpty(args))
                pro.StartInfo.Arguments = args;
            pro.Start();
            #endregion
        }

    }
}
