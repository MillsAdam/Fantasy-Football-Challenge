WITH PlayerTotals AS (
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
		FROM offense_stats_2022 
		WHERE Position IN ('RB', 'WR', 'TE') 
			AND PositionCategory = 'OFF' 
			AND SeasonType = 1 
			AND Week IN (
				SELECT DISTINCT Week 
				FROM offense_stats_2022 
				WHERE Played = 1 
				ORDER BY Week DESC LIMIT 4
			)
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
	ROUND(AVG(o.RushingAttempts), 2) AS RushingAttempts, 
	ROUND(AVG(o.ReceivingTargets), 2) AS ReceivingTargets, 
	ROUND(AVG(o.RushingAttempts + o.ReceivingTargets) * 100.0 / AVG(TotalTouches), 2) AS Usage, 
	ROUND(AVG(pt.TotalTouches), 2) AS Touches, 
	ROUND(AVG(o.FantasyPointsPPR), 2) AS AvgFantasyPoints 
FROM offense_stats_2022 AS o 
JOIN PlayerTotals AS pt ON o.Team = pt.Team AND o.Position = pt.Position 
WHERE o.Position IN ('RB', 'WR', 'TE') 
	AND o.PositionCategory = 'OFF' 
	AND o.SeasonType = 1 
	AND o.Week IN (
		SELECT DISTINCT Week 
		FROM offense_stats_2022 
		WHERE Played = 1 
		ORDER BY Week DESC LIMIT 4
	)
GROUP BY o.PlayerID, o.Team, o.Position, o.Name 
ORDER BY AvgFantasyPoints DESC;
			
			
			
			