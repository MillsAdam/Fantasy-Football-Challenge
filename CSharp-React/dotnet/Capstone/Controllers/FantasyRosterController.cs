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
        private readonly IFantasyLineupDao _fantasyLineupDao;

        public FantasyTeamController(IFantasyRosterDao fantasyRosterDao, IUserDao userDao, IFantasyLineupDao fantasyLineupDao)
        {
            _fantasyRosterDao = fantasyRosterDao;
            _userDao = userDao;
            _fantasyLineupDao = fantasyLineupDao;
        }

        [HttpPost]
        public async Task<ActionResult> CreateFantasyRoster([FromQuery] string teamName)
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                await _fantasyRosterDao.CreateFantasyRoster(user, teamName);
                FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                await _fantasyLineupDao.CreateFantasyLineup(fantasyRoster.FantasyRosterId);
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
                List<FantasyRosterDto> fantasyRosterDtos = await _fantasyRosterDao.GetFantasyRosters();
                return Ok(fantasyRosterDtos);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting fantasy teams: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}