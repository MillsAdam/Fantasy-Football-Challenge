using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public interface IKSeasonTotalDao
    {
        Task<List<PlayerStatsExtDto>> getKSeasonTotalStatsAsync();
        Task<List<PlayerStatsExtDto>> getKSeasonTotalStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getKSeasonTotalStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getKSeasonTotalStatsByNameAsync(string name);
    }
}