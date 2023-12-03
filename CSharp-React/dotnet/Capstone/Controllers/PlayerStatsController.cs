using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.DAO.Reference;
using Capstone.Models;
using Capstone.Models.Data;
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

        [HttpPost("stats/week")]
        public async Task<ActionResult> UpsertPlayerStatsByWeek()
        {
            try
            {
                List<PlayerStats> playerStats = await _fantasyDataService.GetPlayerStatsForUpdateAsync();
                List<DefenseStats> defenseStats = await _fantasyDataService.GetDefenseStatsForUpdateAsync();
                foreach (PlayerStats playerStat in playerStats)
                {
                    PlayerStatsDto playerStatsDto = PlayerStatsDto.FromPlayerStats(playerStat);
                    await _playerStatsDao.UpsertPlayerStatsDtoAsync(playerStatsDto);
                };
                foreach (DefenseStats defenseStat in defenseStats)
                {
                    PlayerStatsDto playerStatsDto = PlayerStatsDto.FromDefenseStats(defenseStat);
                    await _playerStatsDao.UpsertDefenseStatsDtoAsync(playerStatsDto);
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

        [HttpPost("projections/week")]
        public async Task<ActionResult> UpsertPlayerProjectionsByWeek()
        {
            try
            {
                List<PlayerStats> playerProjections = await _fantasyDataService.GetPlayerProjectionsForUpdateAsync();
                List<DefenseStats> defenseProjections = await _fantasyDataService.GetDefenseProjectionsForUpdateAsync();
                foreach (PlayerStats playerProjection in playerProjections)
                {
                    PlayerStatsDto playerProjectionsDto = PlayerStatsDto.FromPlayerStats(playerProjection);
                    await _playerStatsDao.UpsertPlayerProjectionsDtoAsync(playerProjectionsDto);
                };
                foreach (DefenseStats defenseProjection in defenseProjections)
                {
                    PlayerStatsDto playerProjectionsDto = PlayerStatsDto.FromDefenseStats(defenseProjection);
                    await _playerStatsDao.UpsertDefenseProjectionsDtoAsync(playerProjectionsDto);
                };
                return Ok("Player projections added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding player projections: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("stats/ext")]
        public async Task<ActionResult> AddPlayerStatsExt()
        {
            try
            {
                List<PlayerStats> playerStats = await _fantasyDataService.GetAllPlayerStatsAsync();
                List<DefenseStats> defenseStats = await _fantasyDataService.GetAllDefenseStatsAsync();
                foreach (PlayerStats playerStat in playerStats)
                {
                    PlayerStatsExt playerStatsExt = PlayerStatsExt.FromPlayerStatsExt(playerStat);
                    await _playerStatsDao.AddPlayerStatsExtAsync(playerStatsExt);
                };
                foreach (DefenseStats defenseStat in defenseStats)
                {
                    PlayerStatsExt playerStatsExt = PlayerStatsExt.FromDefenseStatsExt(defenseStat);
                    await _playerStatsDao.AddDefenseStatsExtAsync(playerStatsExt);
                };
                return Ok("Player stats extended added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding player stats extended: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("stats/ext/week")]
        public async Task<ActionResult> UpsertPlayerStatsExtByWeek()
        {
            try
            {
                List<PlayerStats> playerStats = await _fantasyDataService.GetPlayerStatsForUpdateAsync();
                List<DefenseStats> defenseStats = await _fantasyDataService.GetDefenseStatsForUpdateAsync();
                foreach (PlayerStats playerStat in playerStats)
                {
                    PlayerStatsExt playerStatsExt = PlayerStatsExt.FromPlayerStatsExt(playerStat);
                    await _playerStatsDao.UpsertPlayerStatsExtAsync(playerStatsExt);
                };
                foreach (DefenseStats defenseStat in defenseStats)
                {
                    PlayerStatsExt playerStatsExt = PlayerStatsExt.FromDefenseStatsExt(defenseStat);
                    await _playerStatsDao.UpsertDefenseStatsExtAsync(playerStatsExt);
                };
                return Ok("Player stats extended added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding player stats extended: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("projections/ext")]
        public async Task<ActionResult> AddPlayerProjectionsExt()
        {
            try
            {
                List<PlayerStats> playerProjections = await _fantasyDataService.GetAllPlayerProjectionsAsync();
                List<DefenseStats> defenseProjections = await _fantasyDataService.GetAllDefenseProjectionsAsync();
                foreach (PlayerStats playerProjection in playerProjections)
                {
                    PlayerStatsExt playerProjectionsExt = PlayerStatsExt.FromPlayerStatsExt(playerProjection);
                    await _playerStatsDao.AddPlayerProjectionsExtAsync(playerProjectionsExt);
                };
                foreach (DefenseStats defenseProjection in defenseProjections)
                {
                    PlayerStatsExt playerProjectionsExt = PlayerStatsExt.FromDefenseStatsExt(defenseProjection);
                    await _playerStatsDao.AddDefenseProjectionsExtAsync(playerProjectionsExt);
                };
                return Ok("Player projections extended added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding player projections extended: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("projections/ext/week")]
        public async Task<ActionResult> UpsertPlayerProjectionsExtByWeek()
        {
            try
            {
                List<PlayerStats> playerProjections = await _fantasyDataService.GetPlayerProjectionsForUpdateAsync();
                List<DefenseStats> defenseProjections = await _fantasyDataService.GetDefenseProjectionsForUpdateAsync();
                foreach (PlayerStats playerProjection in playerProjections)
                {
                    PlayerStatsExt playerProjectionsExt = PlayerStatsExt.FromPlayerStatsExt(playerProjection);
                    await _playerStatsDao.UpsertPlayerProjectionsExtAsync(playerProjectionsExt);
                };
                foreach (DefenseStats defenseProjection in defenseProjections)
                {
                    PlayerStatsExt playerProjectionsExt = PlayerStatsExt.FromDefenseStatsExt(defenseProjection);
                    await _playerStatsDao.UpsertDefenseProjectionsExtAsync(playerProjectionsExt);
                };
                return Ok("Player projections extended added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding player projections extended: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}