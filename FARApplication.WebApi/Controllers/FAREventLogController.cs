
using FARApplication.Data.Data.Entities;
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
    public class FAREventLogController : ControllerBase
    {
       private IFAREventRepository _repository;
       private readonly ILogger<FAREventLogController> _logger;
        public FAREventLogController(ILogger<FAREventLogController> logger, IFAREventRepository repository)
        {
            _logger = logger;
            _repository = repository;

        }
        [HttpGet]
        public IEnumerable<FAREventLog> Get(int FARId)
        {
            var result = _repository.GetEventLogByFARId(FARId);
            return result;
        }
        [HttpPost]
        public int AddFAREventLog(FAREventLog model)
        {
            var result = _repository.AddEventDetails(model);
            return result;
        }
    }
}
