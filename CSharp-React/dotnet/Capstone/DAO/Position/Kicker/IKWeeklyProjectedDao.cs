using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public interface IKWeeklyProjectedDao
    {
        Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsAsync(int week);
        Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsByConfAsync(string conf, int week);
        Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsByTeamAsync(string team, int week);
        Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsByNameAsync(string name, int week);
    }
}