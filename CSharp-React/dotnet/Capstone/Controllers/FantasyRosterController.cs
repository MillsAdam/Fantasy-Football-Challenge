using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/fantasyrosters")]
    public class FantasyTeamController : ControllerBase
    {
        private readonly IFantasyRosterDao _fantasyRosterDao;
        private readonly IUserDao _userDao;
        private readonly IFantasyLineupDao _fantasyLineupDao;
        private readonly ILogger<FantasyTeamController> _logger;

        public FantasyTeamController(IFantasyRosterDao fantasyRosterDao, IUserDao userDao, IFantasyLineupDao fantasyLineupDao, ILogger<FantasyTeamController> logger)
        {
            _fantasyRosterDao = fantasyRosterDao;
            _userDao = userDao;
            _fantasyLineupDao = fantasyLineupDao;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateFantasyRoster([FromQuery] string teamName)
        {
            try
            {
                User user = _userDao.GetUserByUsername(User.Identity.Name);
                await _fantasyRosterDao.CreateFantasyRoster(user, teamName);
                FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                await _fantasyLineupDao.CreateFantasyLineup(fantasyRoster.FantasyRosterId);
                return Ok("Fantasy roster created successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error creating fantasy team: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetFantasyRosters()
        {
            try
            {
                User user = _userDao.GetUserByUsername(User.Identity.Name);
                List<FantasyRosterDto> fantasyRosterDtos = await _fantasyRosterDao.GetFantasyRosters(user);
                if (fantasyRosterDtos == null || !fantasyRosterDtos.Any())
                {
                    return Ok(new List<FantasyRosterDto>());
                }
                
                return Ok(fantasyRosterDtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error getting fantasy teams: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}