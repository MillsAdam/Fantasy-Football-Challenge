using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/playerstats")]
    public class PlayerStatsExtController : ControllerBase
    {
        private PlayerStatsExtService _playerStatsExtService;
        public PlayerStatsExtController(PlayerStatsExtService playerStatsExtService)
        {
            _playerStatsExtService = playerStatsExtService;
        }

        [HttpGet]
        public async Task<ActionResult> searchPlayerStatsAsync(
            [FromQuery] string position, 
            [FromQuery] string interval, 
            [FromQuery] string category, 
            [FromQuery] string filter = "", 
            [FromQuery] int? week = null)
        {
            return Ok(await _playerStatsExtService.searchPlayerStatsAsync(position, interval, category, filter, week));
        }
    }
}