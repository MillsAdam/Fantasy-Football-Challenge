using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO.FantasyLeague
{
    public interface IFantasyMemberDao
    {
        Task AddMemberAsync(FantasyMember fantasyMember);
        Task<List<FantasyMember>> GetFantasyMembersByLeagueIdAsync(int fantasyLeagueId);
        Task<List<FantasyLeagueModelDto>> GetFantasyLeaguesByUserIdAsync(User user);
    }
}