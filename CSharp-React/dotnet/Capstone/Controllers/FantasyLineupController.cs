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
    [Route("api/fantasylineups")]
    public class FantasyLineupController : ControllerBase
    {
        private readonly IFantasyLineupDao _fantasyLineupDao;
        private readonly IUserDao _userDao;

        public FantasyLineupController(IFantasyLineupDao fantasyLineupDao, IUserDao userDao)
        {
            _fantasyLineupDao = fantasyLineupDao;
            _userDao = userDao;
        }

        [HttpGet("score")]
        public async Task<ActionResult> GetWeeklyScoreByUserAndWeek([FromQuery] int gameWeek)
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                double weeklyScore = await _fantasyLineupDao.GetWeeklyScoreByUserAndWeek(user, gameWeek);
                return Ok(weeklyScore);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting weekly score: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }

}