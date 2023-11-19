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
    [Route("[controller]")]
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
        public ActionResult<TeamDto> AddTeam()
        {
            List<Team> teams = _fantasyDataService.GetTeamsAsync().Result;
            return _teamDao.AddTeam(teams[0]);
        }
    }
}