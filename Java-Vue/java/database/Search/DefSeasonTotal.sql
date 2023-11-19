SELECT 
	d.PlayerID, 
	COUNT(DISTINCT d.Week) AS Week, 
	d.Team, 
	SUM(d.DefensiveTouchdowns) AS DefensiveTouchdowns, 
	SUM(d.SpecialTeamsTouchdowns) AS SpecialTeamsTouchdowns, 
	SUM(d.TouchdownsScored) AS TouchdownsScored, 
	SUM(d.FumblesForced) AS FumblesForced, 
	SUM(d.FumblesRecovered) AS FumblesRecovered, 
	SUM(d.Interceptions) AS Interceptions, 
	SUM(d.TacklesForLoss) AS TacklesForLoss, 
	SUM(d.QuarterbackHits) AS QuarterbackHits, 
	SUM(d.Sacks) AS Sacks, 
	SUM(d.Safeties) AS Safeties, 
	SUM(d.BlockedKicks) AS BlockedKicks, 
	SUM(d.PointsAllowed) AS PointsAllowed, 
	SUM(d.FantasyPoints) AS TotalFantasyPoints, 
	AVG(d.FantasyPoints) AS AvgFantasyPoints 
FROM defense_stats_2022 AS d 
WHERE d.SeasonType = 1 
GROUP BY d.PlayerID, d.Team 
ORDER BY TotalFantasyPoints DESC;