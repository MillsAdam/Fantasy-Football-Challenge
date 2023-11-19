WITH StartingWeek AS (
	SELECT Week 
	FROM offense_proj_2022 
	WHERE Week >= 1 
	ORDER BY Week 
	LIMIT 1
), PlayerTotals AS (
		SELECT 
		o.Team, 
		o.Position, 
		SUM(o.RushingAttempts + o.ReceivingTargets) AS TotalTouches 
	FROM offense_proj_2022 o
	CROSS JOIN StartingWeek
	WHERE o.Position IN ('RB', 'WR', 'TE') 
		AND o.PositionCategory = 'OFF' 
		AND o.SeasonType = 1 
		AND o.Week >= StartingWeek.Week 
		AND o.Week <= StartingWeek.Week + 3 
	GROUP BY o.Team, o.Position 
) 
SELECT 
	o.PlayerID, 
	COUNT(DISTINCT o.Week) AS Week, 
	o.Team, 
	o.Position, 
	o.Name, 
	SUM(o.RushingAttempts) AS RushingAttempts, 
	SUM(o.ReceivingTargets) AS ReceivingTargets, 
	SUM(o.RushingAttempts + o.ReceivingTargets) * 100.0 / TotalTouches AS Usage, 
	pt.TotalTouches AS TotalTouches, 
	SUM(o.FantasyPointsPPR) AS TotalFantasyPoints, 
	AVG(o.FantasyPointsPPR) AS AvgFantasyPoints 
FROM offense_proj_2022 AS o 
JOIN PlayerTotals AS pt ON o.Team = pt.Team AND o.Position = pt.Position 
CROSS JOIN StartingWeek 
WHERE o.Position IN ('RB', 'WR', 'TE') 
	AND o.Team = 'LAR' 
	AND o.PositionCategory = 'OFF' 
	AND o.SeasonType = 1 
	AND o.Week >= StartingWeek.Week 
	AND o.Week <= StartingWeek.Week + 3 
GROUP BY o.PlayerID, o.Team, o.Position, o.Name, pt.TotalTouches  
ORDER BY TotalFantasyPoints DESC;