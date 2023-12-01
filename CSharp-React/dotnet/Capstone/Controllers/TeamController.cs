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
    [Route("api/teams")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamDao _teamDao;
        private readonly FantasyDataService _fantasyDataService;
        
        public TeamController(ITeamDao teamDao, FantasyDataService fantasyDataService)
        {
            _teamDao = teamDao;
            _fantasyDataService = fantasyDataService;
        }

        [HttpPost]
        public async Task<ActionResult> AddTeam()
        {
            try
            {
                List<Team> teams = await _fantasyDataService.GetTeamsAsync();
                foreach (Team team in teams)
                {
                    TeamDto teamDto = TeamDto.FromTeam(team);
                    await _teamDao.AddTeamAsync(teamDto);
                };
                return Ok("Teams added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding teams: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> ToggleTeamStatusAsync([FromQuery] string teamName)
        {
            try
            {
                await _teamDao.ToggleTeamStatusAsync(teamName);
                return Ok("Team status updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating team status: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<TeamDto>>> GetTeamsAsync()
        {
            try
            {
                List<TeamDto> teams = await _teamDao.GetTeamsAsync();
                return Ok(teams);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting teams: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<TeamDto>>> GetActiveTeamsAsync()
        {
            try
            {
                List<TeamDto> teams = await _teamDao.GetActiveTeamsAsync();
                return Ok(teams);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting active teams: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}