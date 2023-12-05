using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public interface IDefSeasonAverageDao
    {
        Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsAsync();
        Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsByNameAsync(string name);
    }
}