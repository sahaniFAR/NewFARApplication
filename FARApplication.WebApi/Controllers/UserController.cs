
using FARApplication.Data;
using FARApplication.Data.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

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
                var user = _repository.IsValidUser(email, EncryptPassword(password));
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
        [HttpGet]
        public ActionResult UpdateUserPassword(string emailId, string password)
        {
            try
            {

                var result = _repository.UpdatePassword(emailId, EncryptPassword(password));
                return Ok(result);

            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to update user password {ex}");
                return BadRequest("Failed to update user password ");
            }

        }
        [HttpGet]
        public ActionResult GetUserByRole(int approvalLevel)
        {
            try 
            { 
                var result = _repository.GetUserByRole(approvalLevel);
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get user by role{ex}");
                return BadRequest("Failed to get user by role ");
            }
        }
        [HttpGet]
        public ActionResult GetUserEmailIdsOnRole(int ApprovalLevel)
        {
            try
            {
                var result = _repository.GetUserEmailIdsOnRole(ApprovalLevel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user email by role{ex}");
                return BadRequest("Failed to get user email by role ");
            }
        }
        [HttpGet]
        public string EncryptPassword(string password)
        {
            string strPassword = string.Empty;
            byte[] enc_date = new byte[password.Length];
            enc_date = Encoding.UTF8.GetBytes(password);
            strPassword = Convert.ToBase64String(enc_date);

            return strPassword;
        }
        [HttpGet]
        public string DecryptPassword(string encodedData)
        {
            string strPassword = string.Empty;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            strPassword = new String(decoded_char);
            return strPassword;
        }

    }
}
