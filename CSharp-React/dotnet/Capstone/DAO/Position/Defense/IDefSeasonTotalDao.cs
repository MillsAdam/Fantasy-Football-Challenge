using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public interface IDefSeasonTotalDao
    {
        Task<List<PlayerStatsExtDto>> getDefSeasonTotalStatsAsync();
        Task<List<PlayerStatsExtDto>> getDefSeasonTotalStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getDefSeasonTotalStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getDefSeasonTotalStatsByNameAsync(string name);
    }
}