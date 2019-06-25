using ChfsApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            string newfileid = Guid.NewGuid().ToString();

            try
            {
                string filename = "djsy.pdf";

                string filepath = Path.Combine(System.Environment.CurrentDirectory, filename);

                string savedir = Path.Combine(System.Environment.CurrentDirectory, "tmp");

                if (!Directory.Exists(savedir))
                    Directory.CreateDirectory(savedir);

                FileInfo fileinfor = new FileInfo(filename);

                string newfilename = String.Format("{0}{1}", newfileid, fileinfor.Extension);

                string newfilepath = Path.Combine(savedir, newfilename);

                if (!File.Exists(newfilepath))
                    File.Copy(filepath, newfilepath);

                CHFSApi.init("192.168.1.196", 8001);
                CHFSApi.Login();
                CHFSApi.Upload(newfilepath);

            }
            catch (Exception ex) 
            {
                string logfilename = String.Format("{0}.txt", newfileid);
                string logfilepath = Path.Combine(System.Environment.CurrentDirectory, logfilename);
                File.AppendAllText(logfilepath, ex.ToString());//添加至文件
            }
        }
    }
}
