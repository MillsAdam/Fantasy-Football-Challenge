using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationDao _configurationDao;
        
        public ConfigurationController(IConfigurationDao configurationDao)
        {
            _configurationDao = configurationDao;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateConfiguration(Configuration configuration)
        {
            try
            {
                await _configurationDao.UpdateConfiguration(configuration);
                return Ok("Configuration updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating configuration: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Configuration>>> GetConfigurations()
        {
            try
            {
                return Ok(await _configurationDao.GetConfigurations());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting configurations: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}