using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Testchfs.chfsapi
{
    public class CHFSApi
    {
        public static void init(string host, int port) {
            chfsURLHeader = string.Format("http://{0}:{1}/", host, port);
        }
        private static string chfsURLHeader = "";
        private static string Apiname_Login = "session";
        private static string Apiname_GetFiles = "files";
        private static string Apiname_Upload = "upload";
        private static string Apiname_Exist = "exist";


        private static ResponseResult loginresult = null;
        private static ResponseResult Request(
            string url,
            string reqType,
           List<FormData> pars,
            string[] cookies = null)
        {
            #region
            ResponseResult resData = new ResponseResult();
            HttpWebResponse httpWebResponse = null;
            HttpWebRequest req = null;
            try
            {
                string parsstring = "";
                foreach (var par in pars)
                    parsstring += string.Format("{0}={1}&", par.key, par.value);

                if (pars != null && pars.Count > 0) {
                    parsstring = parsstring.Remove(parsstring.Length - 1);
                    url += "?" + parsstring;
                }
                
                req = WebRequest.Create(url) as HttpWebRequest;
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = reqType;

                if (cookies != null)
                {
                    string cookiesstring = "";
                    foreach (var c in cookies)
                        cookiesstring += c + ";";

                    req.Headers.Add("Cookie", cookiesstring);
                }

                try { 
                    httpWebResponse = (HttpWebResponse)req.GetResponse(); 
                }
                catch (WebException ex) { 
                    httpWebResponse = (HttpWebResponse)ex.Response; 
                }

                Stream responseStream = httpWebResponse.GetResponseStream();
                resData.cookie = httpWebResponse.Headers.GetValues("Set-Cookie");
                using (StreamReader resSR = new StreamReader(responseStream))
                {
                    resData.data = resSR.ReadToEnd();
                    resSR.Close();
                    responseStream.Close();
                }

                resData.code = Convert.ToInt32(httpWebResponse.StatusCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("调用服务端接口时出错，异常说明：{0}", e));
                //记录日志todo：
            }
            return resData;
            #endregion
        }

        private static byte[] getFileText(
            FormData postParams, 
            string beginBoundary, 
            string endBoundary)
        {
            #region
            StringBuilder part2 = new StringBuilder();


            part2.Append(beginBoundary);
            part2.Append(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n"+"Content-Type: text/plain\r\n\r\n", postParams.key, postParams.value.Replace("+", "%2B")));

            var part2array = Encoding.UTF8.GetBytes(part2.ToString());

            return part2array;
            #endregion
        }

        private static byte[] getFormText(
            FormData postParams,
            string beginBoundary,
            string endBoundary, bool hasend = true)
        {
            #region
            StringBuilder part2 = new StringBuilder();


            part2.Append(beginBoundary);
            part2.Append(string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n"
                + "{1}", postParams.key, postParams.value.Replace("+", "%2B")));

            if (hasend)
                part2.Append(endBoundary);

            var part2array = Encoding.UTF8.GetBytes(part2.ToString());

            return part2array;
            #endregion
        }

        private static void addFileStream(
            MemoryStream memStream, string filePath)
        {
            #region
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var buffer = new byte[10];
            int bytesRead;
            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }
            fs.Close();
            #endregion
        }        
        
        private static ResponseResult upload(
            string path, 
            string[] cookies)
        {
            #region
            HttpWebResponse httpWebResponse = null;
            ResponseResult resData = new ResponseResult();
            var url = getApiURL(Apiname_Upload);
            var timestamp = DateTime.Now.Ticks.ToString("x");
            var boundary = "-------------------------" + timestamp;
            var beginBoundary = "\r\n--" + boundary + "\r\n";
            var beginBoundary_withoutwrap = "--" + boundary + "\r\n";
            var endBoundary = "\r\n--" + boundary + "--\r\n";


            //组织数据流
            var memStream = new MemoryStream();

            FormData filedata = new FormData { key = "file", value = path };
            var part1 = getFileText(filedata, beginBoundary, endBoundary);
            memStream.Write(part1, 0, part1.Length);

            addFileStream(memStream, path);

            FormData folderdata = new FormData { key = "folder", value = "" };
            var part2 = getFormText(folderdata, beginBoundary, endBoundary);
            memStream.Write(part2, 0, part2.Length);

            //根据数据流建立发送的数据字节
            var tempBuffer = new byte[memStream.Length];
            memStream.Position = 0;
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";

            if (cookies != null)
            {
                string cookiesstring = "";
                foreach (var c in cookies)
                    cookiesstring += c + ";";

                req.Headers.Add("Cookie", cookiesstring);
            }
            req.ContentType = "multipart/form-data;boundary=" + boundary;
            Stream myRequestStream = req.GetRequestStream();
            myRequestStream.Write(tempBuffer, 0, tempBuffer.Length);
            myRequestStream.Close();

            try
            {
                httpWebResponse = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                httpWebResponse = (HttpWebResponse)ex.Response;
            }

            Stream responseStream = httpWebResponse.GetResponseStream();
            using (StreamReader resSR = new StreamReader(responseStream))
            {
                resData.data = resSR.ReadToEnd();
                resSR.Close();
                responseStream.Close();
            }

            resData.code = Convert.ToInt32(httpWebResponse.StatusCode);

            return resData;
            #endregion
        }


        private static string getApiURL(
            string apiName)
        {
            #region
            if (string.IsNullOrEmpty(chfsURLHeader)) 
            {
                throw new Exception("CHFSApi must be to INIT!");
            }
            return chfsURLHeader + apiName;
            #endregion
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public static ResponseResult Login()
        {
            #region
            List<FormData> pars = new List<FormData>();
            pars.Add(new FormData { key = "user", value = CHFSServer.chfsuser });
            pars.Add(new FormData { key = "pwd", value = CHFSServer.chfspwd });

            loginresult = Request(getApiURL(Apiname_Login), "post", pars);
            return loginresult;
            #endregion
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <returns></returns>
        public static ResponseResult GetFiles()
        {
            #region
            List<FormData> pars = new List<FormData>();
            pars.Add(new FormData { key = "filepath", value = "/" });

            return Request(getApiURL(Apiname_GetFiles), "get", pars, loginresult.cookie);
            #endregion
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ResponseResult Upload(string path) 
        {
            #region

            return upload(path, loginresult.cookie);
            #endregion
        }

        /// <summary>
        /// 上传文件前要判断文件名是否重复
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static ResponseResult Exist(string file)
        {
            #region
            FileInfo info = new FileInfo(file);
            List<FormData> pars = new List<FormData>();
            pars.Add(new FormData { key = "file", value = info.Name });

            return Request(getApiURL(Apiname_Exist), "get", pars, loginresult.cookie);
            #endregion
        }
    }
}
