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

        [HttpDelete]
        public async Task<ActionResult> DeleteRosterPlayer([FromQuery] int playerId)
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                await _rosterPlayerDao.DeleteRosterPlayer(user, playerId);
                return Ok("Roster player deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting roster player: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRosterPlayer([FromQuery] int oldPlayerId, [FromQuery] int newPlayerId)
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                await _rosterPlayerDao.UpdateRosterPlayer(user, oldPlayerId, newPlayerId);
                return Ok("Roster player updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating roster player: {e.Message}");
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
                List<RosterPlayerDto> rosterPlayerDtos = await _rosterPlayerDao.GetRosterPlayerDtosByUser(user);
                if (rosterPlayerDtos == null || !rosterPlayerDtos.Any())
                {
                    return Ok(new List<FantasyRosterDto>());
                }
                return Ok(rosterPlayerDtos);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting roster players: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("league")]
        public async Task<ActionResult> GetRosterPlayersByUserId([FromQuery] int userId)
        {
            try
            {
                List<RosterPlayerDto> rosterPlayerDtos = await _rosterPlayerDao.GetRosterPlayerDtosByUserId(userId);
                return Ok(rosterPlayerDtos);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting roster players: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
}