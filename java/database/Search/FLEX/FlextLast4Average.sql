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
		SUM(AvgTotalTouches) AS TotalTouches 
	FROM (
		SELECT 
			PlayerID, 
			Team, 
			Position, 
			AVG(RushingAttempts + ReceivingTargets) AS AvgTotalTouches 
		FROM offense_stats_2022 AS o
		CROSS JOIN StartingWeek 
		WHERE Position IN ('RB', 'WR', 'TE') 
			AND PositionCategory = 'OFF' 
			AND SeasonType = 1 
			AND o.Week <= StartingWeek.Week 
			AND o.Week >= StartingWeek.Week - 3
		GROUP BY PlayerID, Team, Position
	) AS Subquery
	GROUP BY Team, Position
) 
SELECT 
	o.PlayerID, 
	COUNT(DISTINCT o.Week) AS Week, 
	o.Team, 
	o.Position, 
	o.Name, 
	AVG(o.RushingAttempts) AS RushingAttempts, 
	AVG(o.RushingYards) AS RushingYards, 
	CASE 
		WHEN AVG(o.RushingAttempts) = 0 THEN 0 
		ELSE AVG(o.RushingYards) / AVG(o.RushingAttempts) 
	END AS RushingYardsPerAttempt, 
	AVG(o.RushingTouchdowns) AS RushingTouchdowns, 
	AVG(o.ReceivingTargets) AS ReceivingTargets, 
	AVG(o.Receptions) AS Receptions, 
	AVG(o.ReceivingYards) AS ReceivingYards, 
	CASE 
		WHEN AVG(o.Receptions) = 0 THEN 0 
		ELSE AVG(o.ReceivingYards) / AVG(o.Receptions) 
	END AS ReceivingYardsPerReception, 
	AVG(o.ReceivingTouchdowns) AS ReceivingTouchdowns, 
	AVG(o.PuntReturnTouchdowns + o.KickReturnTouchdowns) AS ReturnTouchdowns, 
	AVG(o.TwoPointConversionPasses + o.TwoPointConversionRuns + o.TwoPointConversionReceptions) AS TwoPointConversions, 
	AVG(o.RushingAttempts + o.ReceivingTargets) * 100.0 / AVG(pt.TotalTouches) AS Usage, 
	AVG(o.FumblesLost) AS FumblesLost, 
	AVG(o.FantasyPointsPPR) AS TotalFantasyPoints, 
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