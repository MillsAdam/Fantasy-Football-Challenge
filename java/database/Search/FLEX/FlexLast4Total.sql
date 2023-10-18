WITH StartingWeek AS (
	SELECT Week 
	FROM offense_stats_2022 
	WHERE Week = 8 --?
	ORDER BY Week 
	LIMIT 1
), PlayerTotals AS (
	SELECT 
		Team, 
		Position, 
		SUM(RushingAttempts + ReceivingTargets) AS TotalTouches 
	FROM offense_stats_2022 AS o
	CROSS JOIN StartingWeek 
	WHERE Position IN ('RB', 'WR', 'TE') 
		AND PositionCategory = 'OFF' 
		AND SeasonType = 1 
		AND o.Week <= StartingWeek.Week 
		AND o.Week >= StartingWeek.Week - 3 
	GROUP BY Team, Position
) 
SELECT 
	o.PlayerID, 
	COUNT(DISTINCT o.Week) AS Week, 
	o.Team, 
	o.Position, 
	o.Name, 
	SUM(o.RushingAttempts) AS RushingAttempts, 
	SUM(o.RushingYards) AS RushingYards, 
	CASE 
		WHEN SUM(o.RushingAttempts) = 0 THEN 0 
		ELSE SUM(o.RushingYards) / SUM(o.RushingAttempts) 
	END AS RushingYardsPerAttempt, 
	SUM(o.RushingTouchdowns) AS RushingTouchdowns, 
	SUM(o.ReceivingTargets) AS ReceivingTargets, 
	SUM(o.Receptions) AS Receptions, 
	SUM(o.ReceivingYards) AS ReceivingYards, 
	CASE 
		WHEN SUM(o.Receptions) = 0 THEN 0 
		ELSE SUM(o.ReceivingYards) / SUM(o.Receptions) 
	END AS ReceivingYardsPerReception, 
	SUM(o.ReceivingTouchdowns) AS ReceivingTouchdowns, 
	SUM(o.PuntReturnTouchdowns + o.KickReturnTouchdowns) AS ReturnTouchdowns, 
	SUM(o.TwoPointConversionPasses + o.TwoPointConversionRuns + o.TwoPointConversionReceptions) AS TwoPointConversions, 
	SUM(o.RushingAttempts + o.ReceivingTargets) * 100.0 / pt.TotalTouches AS Usage, 
	SUM(o.FumblesLost) AS FumblesLost, 
	SUM(o.FantasyPointsPPR) AS TotalFantasyPoints, 
	AVG(o.FantasyPointsPPR) AS AvgFantasyPoints 
FROM offense_stats_2022 AS o 
JOIN PlayerTotals AS pt ON o.Team = pt.Team AND o.Position = pt.Position 
JOIN teams_2022 AS t ON o.TeamID = t.TeamID 
CROSS JOIN StartingWeek 
WHERE o.PositionCategory = 'OFF' 
	AND o.SeasonType = 1 
	AND o.Week <= StartingWeek.Week 
	AND o.Week >= StartingWeek.Week - 3 
	AND o.Position IN ('RB', 'WR', 'TE')  
GROUP BY o.PlayerID, o.Team, o.Position, o.Name, pt.TotalTouches 
ORDER BY TotalFantasyPoints DESC;