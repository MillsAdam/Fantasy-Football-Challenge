using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public interface IKWeeklyTotalDao
    {
        Task<List<PlayerStatsExtDto>> getKWeeklyTotalStatsAsync(int week);
        Task<List<PlayerStatsExtDto>> getKWeeklyTotalStatsByConfAsync(string conf, int week);
        Task<List<PlayerStatsExtDto>> getKWeeklyTotalStatsByTeamAsync(string team, int week);
        Task<List<PlayerStatsExtDto>> getKWeeklyTotalStatsByNameAsync(string name, int week);
    }
}