using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Capstone.DAO
{
    public interface IFantasyRosterDao
    {
        Task CreateFantasyRoster(User user, string teamName);
        Task<List<FantasyRoster>> GetFantasyRosters();
        Task<FantasyRoster> GetFantasyRosterByUser(User user);
        
    }
}