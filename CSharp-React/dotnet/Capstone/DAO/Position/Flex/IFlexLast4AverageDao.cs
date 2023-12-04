using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public interface IFlexLast4AverageDao
    {
        Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsAsync();
        Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByNameAsync(string name);

        Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAsync(string pos);
        Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAndConfAsync(string pos, string conf);
        Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAndTeamAsync(string pos, string team);
        Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAndNameAsync(string pos, string name);
    }
}