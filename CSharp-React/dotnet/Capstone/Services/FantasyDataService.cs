using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using Capstone.DAO;

namespace Capstone.Services
{
    public class FantasyDataService
    {
        private readonly HttpClient _client;
        private const string ApiBaseUrl = "https://api.sportsdata.io/api/nfl/fantasy/json";
        private const string ApiKey = "d9c343f71fad4e1dbb63f512b9bcdbcd";
        private readonly IConfigurationDao _configurationDao;

        public FantasyDataService(IConfigurationDao configurationDao)
        {
            _client = new HttpClient();
            _configurationDao = configurationDao;
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            try 
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/Teams");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var teams = JsonConvert.DeserializeObject<List<Team>>(jsonString);
                return teams;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data: {e.Message}");
                return null;
            }
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/Players");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var players = JsonConvert.DeserializeObject<List<Player>>(jsonString);
                return players;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data: {e.Message}");
                return null;
            }
        }

        public async Task<List<PlayerStats>> GetAllPlayerStatsAsync()
        {
            List<PlayerStats> allPlayerStats = new List<PlayerStats>();

            for (int week = 1; week <= 18; week++)
            {
                var weeklyStats = await GetPlayerStatsAsync("2023REG", week);
                allPlayerStats.AddRange(weeklyStats);
            }

            for (int week = 1; week <= 4; week++)
            {
                var weeklyStats = await GetPlayerStatsAsync("2023POST", week);
                allPlayerStats.AddRange(weeklyStats);
            }

            return allPlayerStats;
        }

        public async Task<List<PlayerStats>> GetPlayerStatsAsync(string season, int week)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/PlayerGameStatsByWeek/{season}/{week}");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var playerStats = JsonConvert.DeserializeObject<List<PlayerStats>>(jsonString);
                return playerStats;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data for {season} and {week}: {e.Message}");
                return null;
            }
        }

        public async Task<List<DefenseStats>> GetAllDefenseStatsAsync()
        {
            List<DefenseStats> allDefenseStats = new List<DefenseStats>();

            for (int week = 1; week <= 18; week++)
            {
                var weeklyStats = await GetDefenseStatsAsync("2023REG", week);
                allDefenseStats.AddRange(weeklyStats);
            }

            for (int week = 1; week <= 4; week++)
            {
                var weeklyStats = await GetDefenseStatsAsync("2023POST", week);
                allDefenseStats.AddRange(weeklyStats);
            }

            return allDefenseStats;
        }

        public async Task<List<DefenseStats>> GetDefenseStatsAsync(string season, int week)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/FantasyDefenseByGame/{season}/{week}");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var defenseStats = JsonConvert.DeserializeObject<List<DefenseStats>>(jsonString);
                return defenseStats;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data for {season} and {week}: {e.Message}");
                return null;
            }
        }

        public async Task<List<PlayerStats>> GetAllPlayerProjectionsAsync()
        {
            List<PlayerStats> allPlayerProjections = new List<PlayerStats>();

            for (int week = 1; week <= 18; week++)
            {
                var weeklyProjections = await GetPlayerProjectionsAsync("2023REG", week);
                allPlayerProjections.AddRange(weeklyProjections);
            }

            for (int week = 1; week <= 4; week++)
            {
                var weeklyProjections = await GetPlayerProjectionsAsync("2023POST", week);
                allPlayerProjections.AddRange(weeklyProjections);
            }

            return allPlayerProjections;
        }

        public async Task<List<PlayerStats>> GetPlayerProjectionsAsync(string season, int week)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/PlayerGameProjectionStatsByWeek/{season}/{week}");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var playerProjections = JsonConvert.DeserializeObject<List<PlayerStats>>(jsonString);
                return playerProjections;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data for {season} and {week}: {e.Message}");
                return null;
            }
        }

        public async Task<List<DefenseStats>> GetAllDefenseProjectionsAsync()
        {
            List<DefenseStats> allDefenseProjections = new List<DefenseStats>();

            for (int week = 1; week <= 18; week++)
            {
                var weeklyProjections = await GetDefenseProjectionsAsync("2023REG", week);
                allDefenseProjections.AddRange(weeklyProjections);
            }

            for (int week = 1; week <= 4; week++)
            {
                var weeklyProjections = await GetDefenseProjectionsAsync("2023POST", week);
                allDefenseProjections.AddRange(weeklyProjections);
            }

            return allDefenseProjections;
        }

        public async Task<List<DefenseStats>> GetDefenseProjectionsAsync(string season, int week)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/FantasyDefenseProjectionsByGame/{season}/{week}");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var defenseProjections = JsonConvert.DeserializeObject<List<DefenseStats>>(jsonString);
                return defenseProjections;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data for {season} and {week}: {e.Message}");
                return null;
            }
        }


        // Get Stats/Projections for Update
        public async Task<List<PlayerStats>> GetPlayerStatsForUpdateAsync()
        {
            int currentWeek = await _configurationDao.GetConfigurationValue("currentWeek");
            string season;
            int adjustedWeek;

            if (currentWeek <= 18)
            {
                season = "2023REG";
                adjustedWeek = currentWeek;
            }
            else if (currentWeek > 18)
            {
                season = "2023POST";
                adjustedWeek = currentWeek - 18;
            }
            else
            {
                throw new InvalidOperationException("Invalid current week");
            }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/PlayerGameStatsByWeek/{season}/{adjustedWeek}");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var playerStats = JsonConvert.DeserializeObject<List<PlayerStats>>(jsonString);
                return playerStats;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data for {season} and {adjustedWeek}: {e.Message}");
                return null;
            }
        }

        public async Task<List<DefenseStats>> GetDefenseStatsForUpdateAsync()
        {
            int currentWeek = await _configurationDao.GetConfigurationValue("currentWeek");
            string season;
            int adjustedWeek;

            if (currentWeek <= 18)
            {
                season = "2023REG";
                adjustedWeek = currentWeek;
            }
            else if (currentWeek > 18)
            {
                season = "2023POST";
                adjustedWeek = currentWeek - 18;
            }
            else
            {
                throw new InvalidOperationException("Invalid current week");
            }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/FantasyDefenseByGame/{season}/{adjustedWeek}");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var defenseStats = JsonConvert.DeserializeObject<List<DefenseStats>>(jsonString);
                return defenseStats;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data for {season} and {adjustedWeek}: {e.Message}");
                return null;
            }
        }

        public async Task<List<PlayerStats>> GetPlayerProjectionsForUpdateAsync()
        {
            int currentWeek = await _configurationDao.GetConfigurationValue("currentWeek");
            string season;
            int adjustedWeek;

            if (currentWeek <= 18)
            {
                season = "2023REG";
                adjustedWeek = currentWeek;
            }
            else if (currentWeek > 18)
            {
                season = "2023POST";
                adjustedWeek = currentWeek - 18;
            }
            else
            {
                throw new InvalidOperationException("Invalid current week");
            }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/PlayerGameProjectionStatsByWeek/{season}/{adjustedWeek}");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var playerProjections = JsonConvert.DeserializeObject<List<PlayerStats>>(jsonString);
                return playerProjections;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data for {season} and {adjustedWeek}: {e.Message}");
                return null;
            }
        }

        public async Task<List<DefenseStats>> GetDefenseProjectionsForUpdateAsync()
        {
            int currentWeek = await _configurationDao.GetConfigurationValue("currentWeek");
            string season;
            int adjustedWeek;

            if (currentWeek <= 18)
            {
                season = "2023REG";
                adjustedWeek = currentWeek;
            }
            else if (currentWeek > 18)
            {
                season = "2023POST";
                adjustedWeek = currentWeek - 18;
            }
            else
            {
                throw new InvalidOperationException("Invalid current week");
            }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/FantasyDefenseProjectionsByGame/{season}/{adjustedWeek}");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var defenseProjections = JsonConvert.DeserializeObject<List<DefenseStats>>(jsonString);
                return defenseProjections;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data for {season} and {adjustedWeek}: {e.Message}");
                return null;
            }
        }

    }
}