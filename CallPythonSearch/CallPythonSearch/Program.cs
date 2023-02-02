using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
//using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Security.Policy;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;


namespace CallPythonSearch
{

    internal class Program : ProgramBase
    {



        static void Main(string[] args)
        {

            var timer = new Timer(36000);
            timer.Elapsed += OnEventExecution;
            timer.Start();
            Console.ReadLine();
        }
        public static void OnEventExecution(Object sender, ElapsedEventArgs eventArgs)
        {
            var carId = "1.749.2000264";
            var url = "http://localhost:5000/search_finn_for_car/" + carId;

            var request = WebRequest.Create(url);

            var response = request.GetResponse();

            // Parse the JSON response to get the output values
            var outputValues = ParseJsonResponse(response);

            //var NewCarData = ConvertDataToJson(GetThisCar(outputValues, carId));
            //WriteValuesToJson("C:\\projects\\repos\\Python\\App_Data\\cars.json", carId, GetThisCar(outputValues, carId));


            WriteValuesToJson("C:\\projects\\repos\\SearchFinnForCars\\cars.json", GetThisCar(outputValues, carId));

            //WriteValuesToJsonWebReq("http://localhost:8080/App_Data/cars.json", GetThisCar(outputValues, carId));

            // Wait for 1 hour before calling the task again
            //await Task.Delay(3600);

            //// Call the task again
            //await ExecuteCode();
        }

        private static Car GetThisCar(object outputValues, string carId)
        {
            return new Car
            {
                CarId = carId,
                HigestPrice = (int)(outputValues as object[])[0],
                LowestPrice = (int)((object[])outputValues)[1],
                MeanValue = (int)((object[])outputValues)[2],
                MiddelValue = (int)((object[])outputValues)[3],
                NoOfPoints = (int)((object[])outputValues)[4],
                DateTime = ((object[])outputValues)[5].ToString()
            };
        }


        private static string ConvertDataToJson(Car thisCar)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(thisCar, Formatting.Indented, settings);

        }

        private static void WriteValuesToJsonWebReq(string urlFileName, Car thisCar)
        {
            string json;
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            var newJson = JsonConvert.SerializeObject(thisCar, Formatting.Indented, settings);

            try
            {

                json = ReadJsonFileWebReq(urlFileName);
                if (json.Length == 0)
                {
                    json = "[" + newJson + "]";
                }
                else
                    json = json.Insert(json.LastIndexOf("]"), "," + newJson);

                var ret = WriteJsonFileWebReq(urlFileName, json);

            }
            catch (WebException ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }


        }

        private static void WriteValuesToJson(string urlFileName, Car thisCar)
        {

            string json;
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };


            var newJson = JsonConvert.SerializeObject(thisCar, Formatting.Indented, settings);
            try
            {

                json = System.IO.File.ReadAllText(urlFileName);
                json = json.Insert(json.LastIndexOf("]"), "," + newJson);
                //System.IO.File.WriteAllText(urlFileName, json);
                // Write the JSON string to a file
                System.IO.File.WriteAllText(urlFileName, json);



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

        private static object ParseJsonResponse(WebResponse response)
        {
            // Read the response stream into a string
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream);
            var responseString = reader.ReadToEnd();

            // Use the JavaScriptSerializer to deserialize the response string into an object
            var serializer = new JavaScriptSerializer();
            var responseObject = serializer.DeserializeObject(responseString);

            return responseObject;
        }

    }
}
