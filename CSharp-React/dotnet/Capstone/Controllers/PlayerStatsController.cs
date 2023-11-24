using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.DAO.Reference;
using Capstone.Models;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/playerstats")]
    public class PlayerStatsController : ControllerBase
    {
        private readonly IPlayerStatsDao _playerStatsDao;
        private readonly FantasyDataService _fantasyDataService;

        public PlayerStatsController(IPlayerStatsDao playerStatsDao, FantasyDataService fantasyDataService)
        {
            _playerStatsDao = playerStatsDao;
            _fantasyDataService = fantasyDataService;
        }

        [HttpPost]
        public async Task<ActionResult> AddPlayerStats()
        {
            try
            {
                List<PlayerStats> playerStats = await _fantasyDataService.GetPlayerStatsAsync();
                List<DefenseStats> defenseStats = await _fantasyDataService.GetDefenseStatsAsync();
                foreach (PlayerStats playerStat in playerStats)
                {
                    PlayerStatsDto playerStatsDto = PlayerStatsDto.FromPlayerStats(playerStat);
                    await _playerStatsDao.AddPlayerStatsDtoAsync(playerStatsDto);
                };
                foreach(DefenseStats defenseStat in defenseStats)
                {
                    PlayerStatsDto playerStatsDto = PlayerStatsDto.FromDefenseStats(defenseStat);
                    await _playerStatsDao.AddDefenseStatsDtoAsync(playerStatsDto);
                };
                return Ok("Player stats added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding player stats: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}