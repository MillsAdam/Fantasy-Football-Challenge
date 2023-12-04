using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public interface IFlexSeasonTotalDao
    {
        Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsAsync();
        Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByNameAsync(string name);

        Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByPosAsync(string pos);
        Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByPosAndConfAsync(string pos, string conf);
        Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByPosAndTeamAsync(string pos, string team);
        Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByPosAndNameAsync(string pos, string name);
    }
}