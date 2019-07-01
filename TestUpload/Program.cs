using ChfsApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace TestUpload
{
    class Program
    {

        private static string filename = "djsy.pdf";
        private static string chfsserverip = "192.168.1.196";
        private static int chfsserverport = 8001;

        private static void initTestParams()
        {
            #region
            filename = ConfigurationManager.AppSettings["testfilename"].ToString();
            chfsserverip = ConfigurationManager.AppSettings["chfsserverip"].ToString();
            chfsserverport = int.Parse(ConfigurationManager.AppSettings["chfsserverport"].ToString());
            #endregion
        }
        static void Main(string[] args)
        {
            #region

            string newfileid = Guid.NewGuid().ToString();

            try
            {
                //初始化测试参数
                initTestParams();
                //调用chfs的封装接口
                CHFSApi.init(chfsserverip, chfsserverport);
                //如果需要上传则需要先进行登陆获取token，接口中已经进行了隐式传输token
                CHFSApi.Login();
                //开始上传文件
                CHFSApi.Upload(copyToTMP(newfileid));

            }
            catch (Exception ex) 
            {
                string logfilename = String.Format("{0}.txt", newfileid);
                string logfilepath = Path.Combine(System.Environment.CurrentDirectory, logfilename);
                File.AppendAllText(logfilepath, ex.ToString());//添加至文件
            }
            #endregion
        }

        /// <summary>
        /// 为测试多份文件同时上传，我们可以开启多次该应用，
        /// 那么就需要把文件来存储为多份来进行上传操作。
        /// </summary>
        /// <param name="newfileid"></param>
        /// <returns></returns>
        private static string copyToTMP(
            string newfileid)
        {
            #region
            string filepath = Path.Combine(
                System.Environment.CurrentDirectory, 
                filename);

            string savedir = Path.Combine(
                System.Environment.CurrentDirectory, 
                "tmp");

            if (!Directory.Exists(savedir))
                Directory.CreateDirectory(savedir);

            FileInfo fileinfor = new FileInfo(filename);
            string newfilename = String.Format(
                "{0}{1}", newfileid, fileinfor.Extension);

            string newfilepath = Path.Combine(savedir, newfilename);

            if (!File.Exists(newfilepath))
                File.Copy(filepath, newfilepath);

            return newfilepath;
            #endregion
        }
    }
}
