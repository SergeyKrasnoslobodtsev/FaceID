
using AutoMapper;
using FaceID.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _service;

        public EmployeeController(ILogger<EmployeeController> logger, IMapper mapper, FaceID.Core.Services.EmployeeService employeeService)
        {
            _logger = logger;
            _mapper = mapper;
            _service = employeeService;
        }
        [HttpGet (Name ="GetEmployee")]
        [ProducesResponseType(typeof(FaceID.Core.Entities.Employee), 200)]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("In EmployeesController Get");

                var employees = _mapper.Map<IEnumerable<FaceID.Core.Entities.Employee>>(await _service.GetAllEmployeesAsync());

                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in EmployeesController: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        [HttpPost(Name = "AddEmployee")]
        public async Task<IActionResult> Add(FaceID.Core.Entities.Employee employee)
        {
            try
            {
                _logger.LogInformation("In EmployeesController Add");

                var employees = _mapper.Map<IEnumerable<FaceID.Core.Entities.Employee>>(await _service.AddAsync(employee));

                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in EmployeesController: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation("In EmployeesController Get");

                var employees = _mapper.Map<IEnumerable<FaceID.Core.Entities.Employee>>(await _service.GetEmployeeByIdAsync(id));

                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in EmployeesController: {ex.Message}");
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }
    }
}
