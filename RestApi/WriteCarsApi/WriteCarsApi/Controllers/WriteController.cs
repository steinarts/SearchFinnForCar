using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
//using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WriteCarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriteController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public WriteController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("Write")]
        public IActionResult Post([FromBody] string content)
        {
            string urlPath = "/CarPool/cars.json";
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), urlPath);
            string filePath = "C:\\projects\\repos\\SearchFinnForCars\\cars.json";
            string json;
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };


            var newJson = JsonConvert.SerializeObject(content, Formatting.Indented, settings);
            try
            {

                json = System.IO.File.ReadAllText(filePath);
                json = json.Insert(json.LastIndexOf("]"), "," + newJson);
                System.IO.File.WriteAllText(urlPath, json);


                //try
                //{
                //System.IO.File.WriteAllText(filePath, content);
                //return Ok("Skriving til fil var vellykket.");
            }
            catch (IOException ex)
            {
                return BadRequest("Skriving til fil mislyktes: " + ex.Message);
            }
            return Ok("Skriving til fil var vellykket.");
        }

        [HttpGet("getPath")]
        public IEnumerable<string> Get()
        {

            string webRootPath = _env.WebRootPath;
            string contentRootPath = _env.ContentRootPath;

            string urlPath = "/CarPool/cars.json";
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), urlPath);
            string filePath = Path.Combine(_env.ContentRootPath, urlPath);
            //string webRootPath = _env.WebRootPath;
            string json;
            json = System.IO.File.ReadAllText(filePath);
            yield return json;
        }

    }

    //public IActionResult Index()
    //{
    //    return View();
    //}

    //private static readonly string[] Summaries = new[]
    //{
    //"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

    //[HttpGet("Write")]
    //public IEnumerable<WeatherForecast> Get()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    {
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}


    //private async Task<BinaryStreamContent> GetProvider()
    //{
    //    if (!Request.Content.IsMimeMultipartContent())
    //    {
    //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
    //    }
    //    var provider = await Request.Content.ReadAsMultipartAsync<BinaryStreamContent>(new BinaryStreamContent());
    //    return provider;
    //    //access form data  


    //}


    //[HttpPost]
    //public async Task<IHttpActionResult> ProductImage()
    //{

    //    var provider = await this.GetProvider();
    //    if (!provider.HasFiles)
    //        return BadRequest("No file uploaded");
    //    const string VARER = "VARER";
    //    try
    //    {
    //        var file = await provider.ReadCurrentFile();
    //        var fileName = provider.CurrentFilename;
    //        if (file.Length == 0)
    //        {
    //            return base.BadRequest("FileSize is zero");
    //        }
    //        var uploadFile = dbc.tblFiles.FirstOrDefault(t => t.Folder == VARER && t.FileName == fileName);
    //        if (uploadFile == null)
    //            uploadFile = new tblFile();

    //        using (var stream = new MemoryStream())
    //        {
    //            await file.CopyToAsync(stream);
    //            uploadFile.FileData = stream.ToArray();
    //            uploadFile.Folder = VARER;
    //            uploadFile.FileName = fileName;
    //            uploadFile.Created = DateTime.Now;
    //            uploadFile.FileType = Path.GetExtension(fileName).ToUpper().Replace(".", "");
    //            if (uploadFile.FileId == 0)
    //                dbc.tblFiles.InsertOnSubmit(uploadFile);
    //            dbc.SubmitChanges();
    //            return Ok(fileName);
    //        }
    //    }
    //    catch (System.Exception ex)
    //    {
    //        return BadRequest($"Internal server error: {ex}");
    //    }
    //}

}
