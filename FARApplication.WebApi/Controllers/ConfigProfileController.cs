using FARApplication.Data;
using FARApplication.Data.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace FARApplication.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigProfileController : ControllerBase
    {
        private IFAREventRepository _FarEventrepository;
        private readonly ILogger<ConfigProfileController> _logger;
        private IUserRepository _repository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ConfigProfileController(IWebHostEnvironment hostingEnvironment, ILogger<ConfigProfileController> logger, IUserRepository repository, IFAREventRepository FarEventrepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _repository = repository;

            _FarEventrepository = FarEventrepository;


        }

        [HttpGet]
        public ActionResult<ConfigurationProfile>  GetConfigData()
        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, "jsonData/ConfigurationProfile/ConfigProfileData.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file

            if (string.IsNullOrWhiteSpace(jsonData)) return null; //if no data is present then return null or error if you wish

            var configdata = JsonConvert.DeserializeObject<List<ConfigurationProfile>>(jsonData); //deserialize object as a list in accordance with your json file

            if (configdata == null || configdata.Count == 0) return null; //if there's no data inside our list then return null or error if you wish

            var configprofile = configdata.FirstOrDefault(); //filter the 


            var result = _FarEventrepository.GetEventLogByFAR(0);

            var farlogOldest = result.OrderBy(x => x.EventDate).ToList().First();
            var farlogLatest = result.OrderByDescending(x => x.EventDate).ToList().First();

            var userCreated = _repository.GetUserById((int)farlogOldest.UserId);

            string strCreatedUserName = "";
            if (userCreated != null)
            {
                strCreatedUserName = string.Concat(userCreated.FirstName, " ", userCreated.LastName);

            }
            configprofile.CreatedBy = strCreatedUserName;
            configprofile.CreatedOn = farlogOldest.EventDate.ToString("dd-MM-yyyy hh:mm tt");


            var userModified = _repository.GetUserById((int)farlogLatest.UserId);

            string strModifiedUserName = "";
            if (userModified != null)
            {
                strModifiedUserName = string.Concat(userModified.FirstName, " ", userModified.LastName);

            }
            configprofile.LastModifiedBy = strModifiedUserName;
            configprofile.LastModifiedOn = farlogLatest.EventDate.ToString("dd-MM-yyyy hh:mm tt");



            return Ok(configprofile);

        }

        [HttpPut]
        public ActionResult<int> Update(ConfigurationProfile objConfigProfile)
        {
            try
            {
                int result = 0;
                result = _repository.Update(objConfigProfile);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update FAR ,{ex}");
                return BadRequest("Failed to Update FAR");

            }
        }
    }
}
