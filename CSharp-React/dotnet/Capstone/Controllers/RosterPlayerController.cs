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
    [Route("api/rosterplayers")]
    public class RosterPlayerController : ControllerBase
    {
        private readonly IRosterPlayerDao _rosterPlayerDao;
        private readonly IUserDao _userDao;

        public RosterPlayerController(IRosterPlayerDao rosterPlayerDao, IUserDao userDao)
        {
            _rosterPlayerDao = rosterPlayerDao;
            _userDao = userDao;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRosterPlayer([FromQuery] int playerId)
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                await _rosterPlayerDao.CreateRosterPlayer(user, playerId);
                return Ok("Roster player created successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error creating roster player: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetRosterPlayers()
        {
            try
            {
                List<RosterPlayer> rosterPlayers = await _rosterPlayerDao.GetRosterPlayers();
                return Ok(rosterPlayers);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting roster players: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetRosterPlayersByUser()
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                List<RosterPlayer> rosterPlayers = await _rosterPlayerDao.GetRosterPlayersByUser(user);
                return Ok(rosterPlayers);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting roster players: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
}