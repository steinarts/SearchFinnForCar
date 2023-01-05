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

namespace CallPythonSearch
{
    internal class Program
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
            //WriteValuesToJson("C:\\projects\\repos\\Python\\cars.json", carId, GetThisCar(outputValues, carId));

            WriteValuesToJson("C:\\projects\\repos\\SearchFinnForCars\\cars.json", GetThisCar(outputValues, carId));

            //WriteValuesToJson("~/cars.json", GetThisCar(outputValues, carId));

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

        private static void WriteValuesToJson(string fileName, Car thisCar)
        {

            string json;
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            var newJson = JsonConvert.SerializeObject(thisCar, Formatting.Indented, settings);

            if (File.Exists(fileName))
            {
                json = System.IO.File.ReadAllText(fileName);
                json = json.Insert(json.LastIndexOf("]"), "," + newJson);
                System.IO.File.WriteAllText(fileName, json);
            }
            else
                System.IO.File.WriteAllText(fileName, "[" + newJson + "]");


            //string json;
            //var settings = new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented
            //};

            //json = JsonConvert.SerializeObject(thisCar, Formatting.Indented,settings);



            //// Write the JSON string to a file
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

        class Car
        {
            public string CarId { get; set; }
            public int HigestPrice { get; set; }
            public int LowestPrice { get; set; }
            public int MeanValue { get; set; }
            public int MiddelValue { get; set; }
            
            public int NoOfPoints { get; set; }

            public string DateTime { get; set; }

        }

    }
}
