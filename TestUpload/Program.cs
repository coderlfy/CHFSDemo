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

            string filename = "djsy.pdf";

            string filepath = Path.Combine(System.Environment.CurrentDirectory, filename);

            string savedir = Path.Combine(System.Environment.CurrentDirectory, "tmp");

            if (!Directory.Exists(savedir))
                Directory.CreateDirectory(savedir);

            FileInfo fileinfor = new FileInfo(filename);

            string newfilename = String.Format("{0}{1}", Guid.NewGuid(), fileinfor.Extension);

            string newfilepath = Path.Combine(savedir, newfilename);

            if (!File.Exists(newfilepath))
                File.Copy(filepath, newfilepath);

            CHFSApi.init("localhost", 8001);
            CHFSApi.Login();
            CHFSApi.Upload(newfilepath);
        }
    }
}
