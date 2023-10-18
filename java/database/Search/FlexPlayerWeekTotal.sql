WITH PlayerTotals AS (
	SELECT 
		Week, 
		Team, 
		Position, 
		SUM(RushingAttempts + ReceivingTargets) AS TotalTouches 
	FROM offense_stats_2022 
	WHERE Position IN ('RB', 'WR', 'TE') 
		AND PositionCategory = 'OFF' 
		AND SeasonType = 1 
		AND Week = 1
	GROUP BY Week, Team, Position
) 
SELECT 
	o.PlayerID, 
	COUNT(DISTINCT o.Week) AS Week, 
	o.Team, 
	o.Position, 
	o.Name, 
	SUM(o.RushingAttempts) AS RushingAttempts, 
	SUM(o.ReceivingTargets) AS ReceivingTargets, 
	CASE
		WHEN pt.TotalTouches = 0 THEN 0
		ELSE SUM(o.RushingAttempts + o.ReceivingTargets) * 100.0 / TotalTouches 
	END AS Usage, 
	pt.TotalTouches AS TotalTouches, 
	SUM(o.FantasyPointsPPR) AS TotalFantasyPoints, 
	AVG(o.FantasyPointsPPR) AS AvgFantasyPoints 
FROM offense_stats_2022 AS o 
JOIN PlayerTotals AS pt ON o.Week = pt.Week AND o.Team = pt.Team AND o.Position = pt.Position 
WHERE o.Position IN ('RB', 'WR', 'TE') 
	AND o.PositionCategory = 'OFF' 
	AND o.SeasonType = 1 
GROUP BY o.PlayerID, o.Team, o.Position, o.Name, TotalTouches 
ORDER BY TotalFantasyPoints DESC;