using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Flex
{
    public class FlexSeasonAverageSqlDao : IFlexSeasonAverageDao
    {



        // TODO: Implement FlexSeasonAverageSqlDao methods
        // TODO: Add async to methods
        public Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByConfAsync(string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByTeamAsync(string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAsync(string pos)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAndConfAsync(string pos, string conf)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAndTeamAsync(string pos, string team)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAndNameAsync(string pos, string name)
        {
            throw new NotImplementedException();
        }

    }
}