using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Quarterback
{
    public interface IQBSeasonTotalDao
    {
        Task<List<PlayerStatsExtDto>> getQBSeasonTotalStatsAsync();
        Task<List<PlayerStatsExtDto>> getQBSeasonTotalStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getQBSeasonTotalStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getQBSeasonTotalStatsByNameAsync(string name);
    }
}