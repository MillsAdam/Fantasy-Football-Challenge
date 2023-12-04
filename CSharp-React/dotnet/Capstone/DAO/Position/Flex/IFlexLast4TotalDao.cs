using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public interface IFlexLast4TotalDao
    {
        Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsAsync();
        Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByNameAsync(string name);

        Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAsync(string pos);
        Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAndConfAsync(string pos, string conf);
        Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAndTeamAsync(string pos, string team);
        Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAndNameAsync(string pos, string name);
    }
}