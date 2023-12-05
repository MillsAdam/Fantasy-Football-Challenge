using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;

namespace Capstone.DAO.Position.Kicker
{
    public interface IKLast4TotalDao
    {
        Task<List<PlayerStatsExtDto>> getKLast4TotalStatsAsync();
        Task<List<PlayerStatsExtDto>> getKLast4TotalStatsByConfAsync(string conf);
        Task<List<PlayerStatsExtDto>> getKLast4TotalStatsByTeamAsync(string team);
        Task<List<PlayerStatsExtDto>> getKLast4TotalStatsByNameAsync(string name);
    }
}