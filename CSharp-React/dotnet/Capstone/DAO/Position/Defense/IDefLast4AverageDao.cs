using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public interface IDefLast4AverageDao
    {
        Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsAsync();
        Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsByNameAsync(string name);
    }
}