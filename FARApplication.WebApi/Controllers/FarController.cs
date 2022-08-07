using FARApplication.Data;
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
        public ActionResult<IEnumerable<FAR>> Get()
        {
            try
            {
                var FARs = _repository.GetFARById();

                return Ok(FARs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get FAR BY Id {ex}");
                return BadRequest("Failed to get FAR BY Id");
            }
        }
    }
}
