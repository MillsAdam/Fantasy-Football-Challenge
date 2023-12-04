using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Quarterback
{
    public interface IQBWeeklyProjectedDao
    {
        Task<List<PlayerStatsExtDto>> getQBWeeklyProjectedStatsAsync(int week);
        Task<List<PlayerStatsExtDto>> getQBWeeklyProjectedStatsByConfAsync(string conf, int week);
        Task<List<PlayerStatsExtDto>> getQBWeeklyProjectedStatsByTeamAsync(string team, int week);
        Task<List<PlayerStatsExtDto>> getQBWeeklyProjectedStatsByNameAsync(string name, int week);
    }
}