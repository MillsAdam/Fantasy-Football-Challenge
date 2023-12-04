using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Quarterback
{
    public interface IQBLast4AverageDao
    {
        Task<List<PlayerStatsExtDto>> getQBLast4AverageStatsAsync();
        Task<List<PlayerStatsExtDto>> getQBLast4AverageStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getQBLast4AverageStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getQBLast4AverageStatsByNameAsync(string name);
    }
}