using FARApplication.Data;
using FARApplication.Data.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace FARApplication.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
  

    public class SearchController : ControllerBase
    {
        readonly ILogger<SearchController> _logger;
        private IFARRepository _repository;
        public SearchController(ILogger<SearchController> logger, IFARRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Produces("application/json")]
        [HttpGet]
       // [EnableCors("AllowOrigin")]
        public IActionResult GetFARRequestId()
        {
                string prefix = HttpContext.Request.Query["prefix"].ToString()?? "1"; ;
            try
            {
                var FARs = _repository.GetAllFAR();

                var result = FARs.Where(p => p.Id.ToString().StartsWith(prefix)).Select(p => new { p.Id});

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [Produces("application/json")]
        [HttpGet]
        public IActionResult GetFAROnStatus(int status,  int pageIndex)
        {
            try
            {
                var result = _repository.GetallFARBasedOnStatus(status, 10, pageIndex);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet]
        public IActionResult GetAllFARBasedOnSubmiter(int submiterId, int pageIndex)
        {
            try
            {
                var result = _repository.GetAllFAROnSubmiter(submiterId, 10, pageIndex);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
