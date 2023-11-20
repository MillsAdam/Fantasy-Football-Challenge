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
    [Route("api/fantasyrosters")]
    public class FantasyTeamController : ControllerBase
    {
        private readonly IFantasyRosterDao _fantasyRosterDao;
        private readonly IUserDao _userDao;

        public FantasyTeamController(IFantasyRosterDao fantasyRosterDao, IUserDao userDao)
        {
            _fantasyRosterDao = fantasyRosterDao;
            _userDao = userDao;
        }

        [HttpPost]
        public async Task<ActionResult> CreateFantasyRoster([FromQuery] string teamName)
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                await _fantasyRosterDao.CreateFantasyRoster(user, teamName);
                return Ok("Fantasy roster created successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error creating fantasy team: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetFantasyRosters()
        {
            try
            {
                List<FantasyRoster> fantasyRosters = await _fantasyRosterDao.GetFantasyRosters();
                return Ok(fantasyRosters);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting fantasy teams: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}