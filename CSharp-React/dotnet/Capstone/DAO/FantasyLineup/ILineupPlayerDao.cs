using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ILineupPlayerDao
    {
        Task<List<LineupPlayer>> GetLineupPlayers();
        Task<List<LineupPlayer>> GetLineupPlayersByUser(User user);
        Task CreateLineupPlayer(User user, int playerId);
        Task UpdateLineupPlayer(User user, int oldPlayerId, int newPlayerId);
        Task DeleteLineupPlayer(User user, int playerId);
    }
}