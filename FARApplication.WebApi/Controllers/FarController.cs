
using FARApplication.Data;
using FARApplication.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace FARApplication.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FarController : ControllerBase
    {
        private readonly ILogger<FarController> _logger;
        private IFARRepository _repository;
        public FarController(ILogger<FarController> logger, IFARRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FAR>> Get(int userId)
        {
            try
            {
                var FARs = _repository.GetFARById(userId);

                return Ok(FARs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get FAR BY Id {ex}");
                return BadRequest("Failed to get FAR BY Id");
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<FAR>> GetAllFAR()
        {
            try
            {
                var FARs = _repository.GetAllFAR();

                return Ok(FARs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all FAR {ex}");
                return BadRequest("Failed to get all FAR");
            }
        }


        [HttpPost]
        public ActionResult<int> Add(FAR far)
         {
            try
            {
                int result = this._repository.AddFAR(far);
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to create FAR  {ex}");
                return BadRequest("Failed to create FAR ");
            }
          }

        [HttpGet]
        public ActionResult<string> GetRequestId()
        {
            try
            {
                string result = this._repository.GetFARRequestId();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get FARRequestId  {ex}");
                return BadRequest("Failed to Get FARRequestId");
            }

        }
        [HttpGet]
        public ActionResult<FAR> GetFARDetails(int FARId)
        {
            try 
            {
                var result = _repository.getFARDetails(FARId);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Failed to getFARDetails ,{ex}");
                return BadRequest("Failed to getFARDetails");

            }
            
                
        }
        [HttpPut]
        public ActionResult<int> Update(FAR far)
        {
            try
            {
                int result = 0;
                result = _repository.Update(far);
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
