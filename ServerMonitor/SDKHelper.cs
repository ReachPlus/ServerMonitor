using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Reflection;

namespace ServerMonitor
{
    class SDKHelper
    {
        public static void sendAlert()
        {
            System.Configuration.AppSettingsReader reader = new AppSettingsReader();
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string postData = File.ReadAllText(Path.Combine(path,reader.GetValue("alertfile", typeof(string)).ToString()));

            var uri = reader.GetValue("url", typeof(string)).ToString();

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Credentials = new NetworkCredential(reader.GetValue("user", typeof(string)).ToString(), reader.GetValue("password", typeof(string)).ToString());

            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/xml";

            request.ContentLength = postData.Length;
            try
            {
                EventlogHelper.logEvent(postData);
                StreamWriter postStream = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                postStream.Write(postData);
                postStream.Close();

                var response = (HttpWebResponse)request.GetResponse();
                EventlogHelper.logEvent(response.ToString());
            }
            catch (Exception ex)
            {
                EventlogHelper.logEvent(ex.ToString());
            }
        }
    }
}
