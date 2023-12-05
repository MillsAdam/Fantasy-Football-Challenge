using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public interface IKLast4AverageDao
    {
        Task<List<PlayerStatsExtDto>> getKLast4AverageStatsAsync();
        Task<List<PlayerStatsExtDto>> getKLast4AverageStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getKLast4AverageStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getKLast4AverageStatsByNameAsync(string name);
    }
}