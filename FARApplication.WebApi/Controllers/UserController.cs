
using FARApplication.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;


namespace FARApplication.Service.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [HttpGet("GetUserById")]
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

       
        [HttpGet]
        public ActionResult ValidateUser(string email, string password)
        {
            try
            {
                var user = _repository.IsValidUser(email, password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Validate User {ex}");
                return BadRequest("Failed to Validate User ");

            }


        }

        [HttpGet]
        public ActionResult GetApproverSelectionList()
        {
            try
            {
                var user = _repository.GetApproverSelectionList();
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to fetch approver Selection list {ex}");
                return BadRequest("Failed to fetch approver selection list ");

            }


        }

    }
}
