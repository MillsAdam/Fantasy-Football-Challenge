using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Quarterback
{
    public interface IQBWeeklyTotalDao
    {
        Task<List<PlayerStatsExtDto>> getQBWeeklyTotalStatsAsync(int week);
        Task<List<PlayerStatsExtDto>> getQBWeeklyTotalStatsByConfAsync(string conf, int week);
        Task<List<PlayerStatsExtDto>> getQBWeeklyTotalStatsByTeamAsync(string team, int week);
        Task<List<PlayerStatsExtDto>> getQBWeeklyTotalStatsByNameAsync(string name, int week);
    }
}