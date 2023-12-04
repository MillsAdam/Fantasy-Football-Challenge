using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public interface IFlexWeeklyProjectedDao
    {
        Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsAsync(int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByConfAsync(string conf, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByTeamAsync(string team, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByNameAsync(string name, int week);

        Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAsync(string pos, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAndConfAsync(string pos, string conf, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAndTeamAsync(string pos, string team, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAndNameAsync(string pos, string name, int week);
    }
}