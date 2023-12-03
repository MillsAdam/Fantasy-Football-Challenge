using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Quarterback
{
    public interface IQBLast4TotalDao
    {
        Task<List<PlayerStatsExtDto>> getQBLast4TotalStatsAsync();
        Task<List<PlayerStatsExtDto>> getQBLast4TotalStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getQBLast4TotalStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getQBLast4TotalStatsByNameAsync(string name);
    }
}