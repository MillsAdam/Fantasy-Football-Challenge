using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/scores")]
    public class ScoresController : ControllerBase
    {
        private readonly ScoreService _scoreService;

        public ScoresController(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpPut("lineups")]
        public async Task<ActionResult> UpdateLineupTotalScores()
        {
            try
            {
                await _scoreService.UpdateLineupTotalScores();
                return Ok("Total scores updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating total scores: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("rosters")]
        public async Task<ActionResult> UpdateRosterTotalScores()
        {
            try
            {
                await _scoreService.UpdateRosterTotalScores();
                return Ok("Total scores updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating total scores: {e.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}