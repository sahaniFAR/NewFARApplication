using FARApplication.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace FARApplication.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigProfileController : ControllerBase
    {

        private readonly IWebHostEnvironment _hostingEnvironment;

        public ConfigProfileController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public ConfigurationProfile GetConfigData(string firstName)
        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, "jsonData/ConfigurationProfile/ConfigProfileData.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file

            if (string.IsNullOrWhiteSpace(jsonData)) return null; //if no data is present then return null or error if you wish

            var configdata = JsonConvert.DeserializeObject<List<ConfigurationProfile>>(jsonData); //deserialize object as a list in accordance with your json file

            if (configdata == null || configdata.Count == 0) return null; //if there's no data inside our list then return null or error if you wish

            var configprofile = configdata.FirstOrDefault(); //filter the 

            return configprofile;

        }
    }
}
