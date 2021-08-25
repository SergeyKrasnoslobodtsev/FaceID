using AutoMapper;
using FaceID.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FaceID.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetectorController : BaseApiController
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IDetectorFaceService _service;

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[HttpGet("{framebase64}")]
        //public IEnumerable<Location> Get(string frame)
        //{
        //    //try
        //    //{
        //    //    _logger.LogInformation("In EmployeesController Get");

        //    //    var employees = _mapper.Map<IEnumerable<FaceID.Core.Entities.Employee>>(await _service.GetAllEmployeesAsync());

        //    //    return Ok(employees);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    _logger.LogError($"Error in EmployeesController: {ex.Message}");
        //    //    return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
        //    //}
        //}

        // POST api/<DetectorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DetectorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DetectorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
