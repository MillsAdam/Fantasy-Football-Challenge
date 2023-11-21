using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Microsoft.AspNetCore.Mvc;
using Capstone.Models;
using Capstone.Services;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/lineupplayers")]
    public class LineupPlayerController : ControllerBase
    {
        private readonly ILineupPlayerDao _lineupPlayerDao;
        private readonly IUserDao _userDao;
        private readonly IFantasyLineupService _fantasyLineupService;

        public LineupPlayerController(ILineupPlayerDao lineupPlayerDao, IUserDao userDao, IFantasyLineupService fantasyLineupService)
        {
            _lineupPlayerDao = lineupPlayerDao;
            _userDao = userDao;
            _fantasyLineupService = fantasyLineupService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateLineupPlayer([FromQuery] int playerId)
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                await _fantasyLineupService.CreateLineupPlayerAsync(user, playerId);
                return Ok("Lineup player created successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error creating lineup player: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLineupPlayer([FromQuery] int playerId)
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                await _lineupPlayerDao.DeleteLineupPlayer(user, playerId);
                return Ok("Lineup player deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting lineup player: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLineupPlayer([FromQuery] int oldPlayerId, [FromQuery] int newPlayerId)
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                await _lineupPlayerDao.UpdateLineupPlayer(user, oldPlayerId, newPlayerId);
                return Ok("Lineup player updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating lineup player: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetLineupPlayers()
        {
            try
            {
                string username = User.Identity.Name;
                User user = _userDao.GetUserByUsername(username);
                List<LineupPlayer> lineupPlayers = await _lineupPlayerDao.GetLineupPlayersByUser(user);
                return Ok(lineupPlayers);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting lineup players: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetLineupPlayersByUser()
        {
            try
            {
                List<LineupPlayer> lineupPlayers = await _lineupPlayerDao.GetLineupPlayers();
                return Ok(lineupPlayers);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting lineup players: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}