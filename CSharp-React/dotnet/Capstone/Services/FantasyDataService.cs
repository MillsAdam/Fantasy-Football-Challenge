using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace Capstone.Services
{
    public class FantasyDataService
    {
        private readonly HttpClient _client;
        private const string ApiBaseUrl = "https://api.sportsdata.io/api/nfl/fantasy/json";
        private const string ApiKey = "d9c343f71fad4e1dbb63f512b9bcdbcd";

        public FantasyDataService()
        {
            _client = new HttpClient();
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

        public async Task<List<PlayerStats>> GetPlayerStatsAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/PlayerGameStatsByWeek/2023REG/1");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var playerStats = JsonConvert.DeserializeObject<List<PlayerStats>>(jsonString);
                return playerStats;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data: {e.Message}");
                return null;
            }
        }

        public async Task<List<DefenseStats>> GetDefenseStatsAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/FantasyDefenseByGame/2023REG/1");
                request.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var defenseStats = JsonConvert.DeserializeObject<List<DefenseStats>>(jsonString);
                return defenseStats;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data: {e.Message}");
                return null;
            }
        }

    }
}