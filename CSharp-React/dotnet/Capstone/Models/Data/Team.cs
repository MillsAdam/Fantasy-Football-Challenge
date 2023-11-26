using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capstone.Models
{
    public class Team
    {
        [JsonProperty("Key")]
        public string Key { get; set; }
        [JsonProperty("TeamId")]
        public int TeamID { get; set; }
        [JsonProperty("PlayerId")]
        public int PlayerID { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Conference")]
        public string Conference { get; set; }
        [JsonProperty("Division")]
        public string Division { get; set; }
        [JsonProperty("FullName")]
        public string FullName { get; set; }
        [JsonProperty("StadiumID")]
        public int StadiumID { get; set; }
        [JsonProperty("ByeWeek")]
        public int ByeWeek { get; set; }
        [JsonProperty("AverageDraftPosition")]
        public double? AverageDraftPosition { get; set; }
        [JsonProperty("AverageDraftPositionPPR")]
        public double? AverageDraftPositionPPR { get; set; }
        [JsonProperty("PrimaryColor")]
        public string PrimaryColor { get; set; }
        [JsonProperty("SecondaryColor")]
        public string SecondaryColor { get; set; }
        [JsonProperty("TertiaryColor")]
        public string TertiaryColor { get; set; }
        [JsonProperty("QuaternaryColor")]
        public string QuaternaryColor { get; set; }
        [JsonProperty("WikipediaLogoUrl")]
        public string WikipediaLogoUrl { get; set; }
        [JsonProperty("WikipediaWordMarkUrl")]
        public string WikipediaWordMarkUrl { get; set; }
        [JsonProperty("DraftKingsName")]
        public string DraftKingsName { get; set; }
        [JsonProperty("DraftKingsPlayerID")]
        public int DraftKingsPlayerID { get; set; }
        [JsonProperty("FanDuelName")]
        public string FanDuelName { get; set; }
        [JsonProperty("FanDuelPlayerID")]
        public int FanDuelPlayerID { get; set; }
        [JsonProperty("AverageDraftPosition2QB")]
        public double? AverageDraftPosition2QB { get; set; }
        [JsonProperty("AverageDraftPositionDynasty")]
        public double? AverageDraftPositionDynasty { get; set; }
        [JsonProperty("StadiumDetails")]
        public StadiumDetails StadiumDetails { get; set; }
    }

    public class StadiumDetails
    {
        [JsonProperty("StadiumID")]
        public int StadiumID { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("State")]
        public string State { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("Capacity")]
        public int Capacity { get; set; }
        [JsonProperty("PlayingSurface")]
        public string PlayingSurface { get; set; }
        [JsonProperty("GeoLat")]
        public double GeoLat { get; set; }
        [JsonProperty("GeoLong")]
        public double GeoLong { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}