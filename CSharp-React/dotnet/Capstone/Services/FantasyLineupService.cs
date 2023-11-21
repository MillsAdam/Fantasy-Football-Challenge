using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;

namespace Capstone.Services
{
    public class FantasyLineupService : IFantasyLineupService
    {
        private readonly ILineupPlayerDao _lineupPlayerDao;
        private readonly IPlayerDao _playerDao;
        private readonly IFantasyLineupDao _fantasyLineupDao;
        private readonly IUserDao _userDao;

        public FantasyLineupService(ILineupPlayerDao lineupPlayerDao, IPlayerDao playerDao, IFantasyLineupDao fantasyLineupDao, IUserDao userDao)
        {
            _lineupPlayerDao = lineupPlayerDao;
            _playerDao = playerDao;
            _fantasyLineupDao = fantasyLineupDao;
            _userDao = userDao;
        }

        public async Task CreateLineupPlayerAsync(User user, int playerId)
        {
            var fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user, 1);
            var position = await _playerDao.GetPlayerPositionByPlayerIdAsync(playerId);
            var currentLineupPlayers = await _lineupPlayerDao.GetLineupPlayersByUser(user);
            var positionLimits = new Dictionary<string, int>
            {
                {"QB", 2},
                {"RB", 2},
                {"WR", 3},
                {"TE", 1},
                {"FLEX", 1},
                {"K", 1},
                {"DST", 1}
            };
            var positionCounts = currentLineupPlayers.GroupBy(p => p.Position).ToDictionary(g => g.Key, g => g.Count());

            if (positionCounts.TryGetValue(position, out int currentCount) && currentCount >= positionLimits[position])
            {
                throw new InvalidOperationException($"Cannot add another player to the {position} position.  The limit has been reached.");
            }

            if (position == "RB" || position == "WR" || position == "TE")
            {
                int flexCount = currentLineupPlayers.Count(p => p.Position == "RB" || p.Position == "WR" || p.Position == "TE");
                if (flexCount >= positionLimits["FLEX"])
                {
                    throw new InvalidOperationException($"Cannot add another player to the FLEX position.  The limit has been reached.");
                }
            }

            await _lineupPlayerDao.CreateLineupPlayer(user, playerId);
        }
    }
}