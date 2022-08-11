using FARApplication.Data;
using FARApplication.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserRepository _repository;
        public UserController(ILogger<UserController> logger, IUserRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult GetUserById(int Id)
        {
            try
            {
                var User = _repository.GetUserById(Id);
                return Ok(User);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get User BY Id {ex}");
                return BadRequest("Failed to get User BY Id");

            }
           

        }
    }
}
