using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerDao _playerDao;
        private readonly FantasyDataService _fantasyDataService;

        public PlayerController(IPlayerDao playerDao, FantasyDataService fantasyDataService)
        {
            _playerDao = playerDao;
            _fantasyDataService = fantasyDataService;
        }

        [HttpPost]
        public async Task<ActionResult> AddPlayer()
        {
            try
            {
                List<Team> teams = await _fantasyDataService.GetTeamsAsync();
                List<Player> players = await _fantasyDataService.GetPlayersAsync();
                foreach (Team team in teams)
                {
                    PlayerDto playerDto = PlayerDto.FromTeam(team);
                    await _playerDao.AddPlayerAsync(playerDto);
                };
                foreach (Player player in players)
                {
                    PlayerDto playerDto = PlayerDto.FromPlayer(player);
                    await _playerDao.AddPlayerAsync(playerDto);
                };
                return Ok("Players added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding players: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("upsert")]
        public async Task<ActionResult> UpsertPlayer()
        {
            try
            {
                List<Team> teams = await _fantasyDataService.GetTeamsAsync();
                List<Player> players = await _fantasyDataService.GetPlayersAsync();
                foreach (Team team in teams)
                {
                    PlayerDto playerDto = PlayerDto.FromTeam(team);
                    await _playerDao.UpsertPlayerAsync(playerDto);
                };
                foreach (Player player in players)
                {
                    PlayerDto playerDto = PlayerDto.FromPlayer(player);
                    await _playerDao.UpsertPlayerAsync(playerDto);
                };
                return Ok("Players upserted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error upserting players: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("name")]
        public async Task<ActionResult> GetPlayerIdByName([FromQuery] string playerName)
        {
            try
            {
                List<SearchPlayerDto> searchPlayerDtos = await _playerDao.GetPlayerIdByNameAsync(playerName);
                return Ok(searchPlayerDtos);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting player ID: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("team")]
        public async Task<ActionResult> GetPlayerIdByTeam([FromQuery] string teamName)
        {
            try
            {
                List<SearchPlayerDto> searchPlayerDtos = await _playerDao.GetPlayerIdByTeamAsync(teamName);
                return Ok(searchPlayerDtos);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting player ID: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("position")]
        public async Task<ActionResult> GetPlayerIdByPosition([FromQuery] string position)
        {
            try
            {
                List<SearchPlayerDto> searchPlayerDtos = await _playerDao.GetPlayerIdByPositionAsync(position);
                return Ok(searchPlayerDtos);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting player ID: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}