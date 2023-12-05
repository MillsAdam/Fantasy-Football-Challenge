using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public interface IDefWeeklyProjectedDao
    {
        Task<List<PlayerStatsExtDto>> getDefWeeklyProjectedStatsAsync(int week);
        Task<List<PlayerStatsExtDto>> getDefWeeklyProjectedStatsByConfAsync(string conf, int week);
        Task<List<PlayerStatsExtDto>> getDefWeeklyProjectedStatsByTeamAsync(string team, int week);
        Task<List<PlayerStatsExtDto>> getDefWeeklyProjectedStatsByNameAsync(string name, int week);
    }
}