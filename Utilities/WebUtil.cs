using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KevinColemanInc.Utilities
{
    public static class WebUtil
    {
        public static StringBuilder HTTPGet(string url)
        {
            int attempts = 0;
            while (attempts < 100)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    // used on each read operation
                    byte[] buf = new byte[8192];

                    // prepare the web page we will be asking for
                    HttpWebRequest request = (HttpWebRequest)
                        WebRequest.Create(url);
                    request.CookieContainer = new CookieContainer();
                    request.AllowAutoRedirect = false;
                    request.Accept = "application/json";
                    request.Timeout = 5000;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:2.0) Gecko/20100101 Firefox/4.0";


                    //if (url.StartsWith("https://"))
                   // {
                    //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    //    ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
                    //}

                    // execute the request
                    HttpWebResponse response = (HttpWebResponse)
                        request.GetResponse();

                    // we will read data via the response stream
                    Stream resStream = response.GetResponseStream();

                    string tempString = null;
                    int count = 0;

                    do
                    {
                        // fill the buffer with data
                        count = resStream.Read(buf, 0, buf.Length);

                        // make sure we read some data
                        if (count != 0)
                        {
                            // translate from bytes to ASCII text
                            tempString = Encoding.ASCII.GetString(buf, 0, count);

                            // continue building the string
                            sb.Append(tempString);
                        }
                    }
                    while (count > 0); // any more data to read?

                    // print out page source
                    return sb;
                }
                catch (Exception e)
                {
                    attempts += 1;
                }
            }
            return new StringBuilder();
        }

        public static StringBuilder HTTPPost(string url, string postData, bool json = false)
        {
            int attempts = 0;

            while (attempts < 100)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    // Set the Method property of the request to POST.
                    request.Method = "POST";
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    // Set the ContentType property of the WebRequest.
                    request.ContentType = "application/x-www-form-urlencoded";
                    if (json)
                    {
                        request.ContentType = "application/json";
                        request.Accept = "application/json";
                    }
                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;
                    // Get the request stream.
                    Stream dataStream = request.GetRequestStream();
                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    dataStream.Close();
                    // Get the response.
                    WebResponse response = request.GetResponse();
                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    return new StringBuilder(responseFromServer);
                }
                catch (Exception e)
                {
                    attempts += 1;
                }
            }
            return new StringBuilder();

        }
    }
}
