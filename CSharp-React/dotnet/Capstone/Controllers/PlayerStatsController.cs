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
    [Route("api/players")]
    public class PlayerStatsController : ControllerBase
    {
        private readonly IPlayerStatsDao _playerStatsDao;
        private readonly FantasyDataService _fantasyDataService;

        public PlayerStatsController(IPlayerStatsDao playerStatsDao, FantasyDataService fantasyDataService)
        {
            _playerStatsDao = playerStatsDao;
            _fantasyDataService = fantasyDataService;
        }

        [HttpPost("stats")]
        public async Task<ActionResult> AddPlayerStats()
        {
            try
            {
                List<PlayerStats> playerStats = await _fantasyDataService.GetAllPlayerStatsAsync();
                List<DefenseStats> defenseStats = await _fantasyDataService.GetAllDefenseStatsAsync();
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

        [HttpPost("projections")]
        public async Task<ActionResult> AddPlayerProjections()
        {
            try
            {
                List<PlayerStats> playerProjections = await _fantasyDataService.GetAllPlayerProjectionsAsync();
                List<DefenseStats> defenseProjections = await _fantasyDataService.GetAllDefenseProjectionsAsync();
                foreach (PlayerStats playerProjection in playerProjections)
                {
                    PlayerStatsDto playerProjectionsDto = PlayerStatsDto.FromPlayerStats(playerProjection);
                    await _playerStatsDao.AddPlayerProjectionsDtoAsync(playerProjectionsDto);
                };
                foreach (DefenseStats defenseProjection in defenseProjections)
                {
                    PlayerStatsDto playerProjectionsDto = PlayerStatsDto.FromDefenseStats(defenseProjection);
                    await _playerStatsDao.AddDefenseProjectionsDtoAsync(playerProjectionsDto);
                };
                return Ok("Player projections added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding player projections: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("stats")]
        public async Task<ActionResult> UpdatePlayerStats()
        {
            try
            {
                List<PlayerStats> playerStats = await _fantasyDataService.GetPlayerStatsForUpdateAsync();
                List<DefenseStats> defenseStats = await _fantasyDataService.GetDefenseStatsForUpdateAsync();
                foreach (PlayerStats playerStat in playerStats)
                {
                    PlayerStatsDto playerStatsDto = PlayerStatsDto.FromPlayerStats(playerStat);
                    await _playerStatsDao.UpdatePlayerStatsDtoAsync(playerStatsDto);
                };
                foreach (DefenseStats defenseStat in defenseStats)
                {
                    PlayerStatsDto playerStatsDto = PlayerStatsDto.FromDefenseStats(defenseStat);
                    await _playerStatsDao.UpdateDefenseStatsDtoAsync(playerStatsDto);
                };
                return Ok("Player stats updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating player stats: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("projections")]
        public async Task<ActionResult> UpdatePlayerProjections()
        {
            try
            {
                List<PlayerStats> playerProjections = await _fantasyDataService.GetPlayerProjectionsForUpdateAsync();
                List<DefenseStats> defenseProjections = await _fantasyDataService.GetDefenseProjectionsForUpdateAsync();
                foreach (PlayerStats playerProjection in playerProjections)
                {
                    PlayerStatsDto playerProjectionsDto = PlayerStatsDto.FromPlayerStats(playerProjection);
                    await _playerStatsDao.UpdatePlayerProjectionsDtoAsync(playerProjectionsDto);
                };
                foreach (DefenseStats defenseProjection in defenseProjections)
                {
                    PlayerStatsDto playerProjectionsDto = PlayerStatsDto.FromDefenseStats(defenseProjection);
                    await _playerStatsDao.UpdateDefenseProjectionsDtoAsync(playerProjectionsDto);
                };
                return Ok("Player projections updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating player projections: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}