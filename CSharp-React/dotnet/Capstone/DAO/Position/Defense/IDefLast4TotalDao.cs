using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Defense
{
    public interface IDefLast4TotalDao
    {
        Task<List<PlayerStatsExtDto>> getDefLast4TotalStatsAsync();
        Task<List<PlayerStatsExtDto>> getDefLast4TotalStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getDefLast4TotalStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getDefLast4TotalStatsByNameAsync(string name);
    }
}