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
        Task<List<LineupPlayerDto>> GetLineupPlayerDtosByUser(User user);
        Task<List<LineupPlayerDto>> GetLineupPlayerDtosByUserAndWeek(User user, int gameWeek);
        Task<List<LineupPlayerDto>> GetLineupPlayerDtosByUserIdAndWeek(int userId, int gameWeek);
        Task CreateLineupPlayer(User user, int playerId, string lineupPosition);
        Task UpdateLineupPlayer(User user, int oldPlayerId, int newPlayerId, string oldLineupPosition, string newLineupPosition);
        Task DeleteLineupPlayer(User user, int playerId);
    }
}