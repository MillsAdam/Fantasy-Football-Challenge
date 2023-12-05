using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public interface IDefWeeklyTotalDao
    {
        Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsAsync(int week);
        Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsByConfAsync(string conf, int week);
        Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsByTeamAsync(string team, int week);
        Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsByNameAsync(string name, int week);
    }
}