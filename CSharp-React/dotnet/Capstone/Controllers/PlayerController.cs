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
                foreach (Team team in teams)
                {
                    PlayerDto playerDto = PlayerDto.FromTeam(team);
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
    }
}