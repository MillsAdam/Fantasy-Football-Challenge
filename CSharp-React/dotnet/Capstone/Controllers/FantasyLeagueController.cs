using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.DAO.FantasyLeague;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/fantasyleagues")]
    public class FantasyLeagueController : ControllerBase
    {
        private readonly IFantasyLeagueDao _fantasyLeagueDao;
        private readonly IFantasyMemberDao _fantasyMemberDao;
        private readonly IUserDao _userDao;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<FantasyLeagueController> _logger;

        public FantasyLeagueController(IFantasyLeagueDao fantasyLeagueDao, IFantasyMemberDao fantasyMemberDao, IUserDao userDao, IPasswordHasher passwordHasher, ILogger<FantasyLeagueController> logger)
        {
            _fantasyLeagueDao = fantasyLeagueDao;
            _fantasyMemberDao = fantasyMemberDao;
            _userDao = userDao;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterFantasyLeague(FantasyLeagueRegistration fantasyLeagueRegistration)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(fantasyLeagueRegistration.LeagueName) ||
                string.IsNullOrWhiteSpace(fantasyLeagueRegistration.LeaguePassword))
            {
                return BadRequest(new { message = "League name and password are required." });
            }

            try
            {
                FantasyLeagueModel existingFantasyLeague = _fantasyLeagueDao.GetFantasyLeagueByLeagueName(fantasyLeagueRegistration.LeagueName);
                if (existingFantasyLeague != null)
                {
                    return Conflict(new { message = "League name is already taken." });
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred while checking the league name.");
            }

            try 
            {
                User user = _userDao.GetUserByUsername(User.Identity.Name);
                FantasyLeagueModel createdLeague = await _fantasyLeagueDao.CreateFantasyLeague(user, fantasyLeagueRegistration.LeagueName, fantasyLeagueRegistration.LeaguePassword);

                // Fetch the newly created league
                if (createdLeague != null)
                {
                    await _userDao.SetCurrentLeagueAsync(user.UserId, createdLeague.FantasyLeagueId);

                    ReturnLeague returnLeague = new ReturnLeague 
                    { 
                        FantasyLeagueId = createdLeague.FantasyLeagueId, 
                        UserId = createdLeague.UserId, 
                        LeagueName = createdLeague.LeagueName 
                    };
                    return Created($"api/fantasyleagues/{createdLeague.FantasyLeagueId}", returnLeague);
                }
                else
                {
                    // Handle the case where the league is not found after creation
                    return StatusCode(500, "An error occurred after creating the fantasy league.");
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred while creating the fantasy league.");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult> GetListOfFantasyLeaguesByLeagueName([FromQuery] string leagueName)
        {
            if (string.IsNullOrWhiteSpace(leagueName))
            {
                return BadRequest("League name is required.");
            }

            try
            {
                List<FantasyLeagueModel> fantasyLeagues = await _fantasyLeagueDao.GetListOfFantasyLeaguesByLeagueName(leagueName);
                if (fantasyLeagues == null || fantasyLeagues.Count == 0)
                {
                    return NotFound("No leagues found matching the provided league name.");
                }
                else
                {
                    return Ok(fantasyLeagues);
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred while retrieving the fantasy league.");
            }
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinFantasyLeague(FantasyLeagueJoin fantasyLeagueJoin)
        {
            // Validate input
            if (fantasyLeagueJoin.FantasyLeagueId <= 0 ||
                string.IsNullOrWhiteSpace(fantasyLeagueJoin.LeaguePassword))
            {
                return BadRequest(new { message = "League ID and password are required." });
            }

            try
            {
                FantasyLeagueModel existingFantasyLeague = _fantasyLeagueDao.GetFantasyLeagueByLeagueId(fantasyLeagueJoin.FantasyLeagueId);
                if (existingFantasyLeague == null)
                {
                    return NotFound(new { message = "League name not found." });
                }
                else
                {
                    bool isPasswordValid = _passwordHasher.VerifyHashMatch(existingFantasyLeague.LeaguePasswordHash, fantasyLeagueJoin.LeaguePassword, existingFantasyLeague.LeagueSalt);
                    if (!isPasswordValid)
                    {
                        return BadRequest(new { message = "Invalid password." });
                    }
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred while checking the league name.");
            }

            try
            {
                User user = _userDao.GetUserByUsername(User.Identity.Name);
                FantasyLeagueModel fantasyLeague = _fantasyLeagueDao.GetFantasyLeagueByLeagueId(fantasyLeagueJoin.FantasyLeagueId);
                FantasyMember fantasyMember = new FantasyMember { UserId = user.UserId, FantasyLeagueId = fantasyLeague.FantasyLeagueId };
                await _fantasyMemberDao.AddMemberAsync(fantasyMember);
                await _userDao.SetCurrentLeagueAsync(user.UserId, fantasyLeague.FantasyLeagueId);
                return Ok();
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred while joining the fantasy league.");
            }
        }

        [HttpGet("myleagues")]
        public async Task<IActionResult> GetFantasyLeaguesByUserId()
        {
            try
            {
                User user = _userDao.GetUserByUsername(User.Identity.Name);
                List<FantasyLeagueModelDto> fantasyLeagues = await _fantasyMemberDao.GetFantasyLeaguesByUserIdAsync(user);
                
                return Ok(fantasyLeagues ?? new List<FantasyLeagueModelDto>());
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred while retrieving the fantasy leagues.");
            }
        }

        [HttpPut("setcurrentleague")]
        public async Task<ActionResult> SetCurrentLeague([FromQuery] int fantasyLeagueId)
        {
            try
            {
                User user = _userDao.GetUserByUsername(User.Identity.Name);
                await _fantasyLeagueDao.SetCurrentLeagueAsync(user, fantasyLeagueId);
                return Ok();
            }
            catch (DaoException ex)
            {
                 _logger.LogError(ex, "An error occurred while setting the current league for user {Username}.", User.Identity.Name);
                return StatusCode(500, "An error occurred while setting the current league.");
            }
        }

        [HttpGet("currentleagueid")]
        public async Task<ActionResult> GetCurrentFantasyLeagueIdByUser()
        {
            try
            {
                User user = _userDao.GetUserByUsername(User.Identity.Name);
                int currentFantasyLeagueId = await _fantasyLeagueDao.GetCurrentFantasyLeagueIdByUser(user);
                return Ok(currentFantasyLeagueId);
            }
            catch (DaoException ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the current league for user {Username}.", User.Identity.Name);
                return StatusCode(500, "An error occurred while retrieving the current league.");
            }
        }

    }
}