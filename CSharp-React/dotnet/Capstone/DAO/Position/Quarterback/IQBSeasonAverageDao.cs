using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Quarterback
{
    public interface IQBSeasonAverageDao
    {
        Task<List<PlayerStatsExtDto>> getQBSeasonAverageStatsAsync();
        Task<List<PlayerStatsExtDto>> getQBSeasonAverageStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getQBSeasonAverageStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getQBSeasonAverageStatsByNameAsync(string name);
    }
}