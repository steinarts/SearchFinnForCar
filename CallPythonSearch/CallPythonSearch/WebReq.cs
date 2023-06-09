using System;
using System.IO;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using Newtonsoft.Json;

namespace CallPythonSearch
{
    internal class ProgramBase
    {

        private const string userAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";


        private static void WriteValuesToJsonWebReq(string urlFileName, Car thisCar)
        {

            string json;
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            var newJson = JsonConvert.SerializeObject(thisCar, Formatting.Indented, settings);

            json = System.IO.File.ReadAllText(urlFileName);
            json = json.Insert(json.LastIndexOf("]"), "," + newJson);
            //System.IO.File.WriteAllText(urlFileName, json);
            // Write the JSON string to a file
            System.IO.File.WriteAllText(urlFileName, json);


            try
            {

                //json = ReadJsonFileWebReq(urlFileName);
                //if (json.Length == 0)
                //{
                //    json = "[" + newJson + "]";
                //}
                //else
                //    json = json.Insert(json.LastIndexOf("]"), "," + newJson);

                //var ret = WriteJsonFileWebReq(urlFileName, json);

                //**************************************************************
                //WebClient client = new WebClient();
                //client.Headers.Add("Content-Type", "application/json");

                //if (CheckIfUrlExists(urlFileName))
                //{
                //    //json = System.IO.File.ReadAllText(fileName);
                //    json = client.DownloadString(urlFileName);
                //    json = json.Insert(json.LastIndexOf("]"), "," + newJson);
                //    //System.IO.File.WriteAllText(urlFileName, json);
                //    client.UploadString(urlFileName, "POST", json);
                //}
                //else
                //    client.UploadString(urlFileName, "POST", "[" + newJson + "]");

            }
            catch (WebException ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }
            //System.IO.File.WriteAllText(urlFileName, "[" + newJson + "]");


            //string json;
            //var settings = new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented
            //};

            //json = JsonConvert.SerializeObject(thisCar, Formatting.Indented, settings);



            // Write the JSON string to a file
            //File.AppendAllText(fileName, json);

        }


        private static bool CheckIfUrlExists(string url)
        {

            try
            {
                // Send a HEAD request to the URL
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";

                // Get the response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // If the status code is 200 OK, the file exists
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (WebException)
            {
                // An exception will be thrown if the URL is invalid or if there is no connection to the server
                return false;
            }


        }
        public static string ReadJsonFileWebReq(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            string json;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }

        public static bool WriteJsonFileWebReq(string url, string json)
        {

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                //request.ContentType = "application/json";
                request.CookieContainer = new CookieContainer();
                request.AllowAutoRedirect = false; // Do NOT automatically redirect
                request.UserAgent = userAgent;

                byte[] data = Encoding.UTF8.GetBytes(json);

                request.GetRequestStream().Write(data, 0, data.Length);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // check the status code, etc.
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                    // request was successful
                    return true;
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
                return false;
            }

        }
    }
}