using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public interface IFlexWeeklyTotalDao
    {
        Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsAsync(int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByConfAsync(string conf, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByTeamAsync(string team, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByNameAsync(string name, int week);

        Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAsync(string pos, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAndConfAsync(string pos, string conf, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAndTeamAsync(string pos, string team, int week);
        Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAndNameAsync(string pos, string name, int week);
    }
}