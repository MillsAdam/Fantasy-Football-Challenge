using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public interface IKSeasonAverageDao
    {
        Task<List<PlayerStatsExtDto>> getKSeasonAverageStatsAsync();
        Task<List<PlayerStatsExtDto>> getKSeasonAverageStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getKSeasonAverageStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getKSeasonAverageStatsByNameAsync(string name);
    }
}