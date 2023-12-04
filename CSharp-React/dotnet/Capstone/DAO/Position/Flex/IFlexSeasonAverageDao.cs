using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public interface IFlexSeasonAverageDao
    {
        Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsAsync();
        Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByNameAsync(string name);

        Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAsync(string pos);
        Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAndConfAsync(string pos, string conf);
        Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAndTeamAsync(string pos, string team);
        Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAndNameAsync(string pos, string name);
    }
}