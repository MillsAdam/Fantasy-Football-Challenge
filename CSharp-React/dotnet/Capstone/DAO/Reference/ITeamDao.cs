using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ITeamDao
    {
        Task AddTeamAsync(TeamDto teamDto);
        Task ToggleTeamStatusAsync(string teamName);
        Task<List<TeamDto>> GetTeamsAsync();
        Task<List<TeamDto>> GetActiveTeamsAsync();
    }
}