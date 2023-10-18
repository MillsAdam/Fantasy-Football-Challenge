SELECT 
	o.PlayerID, 
	COUNT(DISTINCT o.Week) AS Week, 
	o.Team, 
	o.Position, 
	o.Name, 
	SUM(o.FieldGoalsMade) AS FieldGoalsMade, 
	SUM(o.FieldGoalsAttempted) AS FieldGoalsAttempted, 
	CASE 
		WHEN SUM(o.FieldGoalsAttempted) = 0 THEN 0 
		ELSE SUM(o.FieldGoalsMade) / SUM(o.FieldGoalsAttempted) * 100.0 
	END AS FieldGoalPercentage, 
	SUM(o.FieldGoalsMade0to19) AS FieldGoalsMade0to19, 
	SUM(o.FieldGoalsMade20to29) AS FieldGoalsMade20to29, 
	SUM(o.FieldGoalsMade30to39) AS FieldGoalsMade30to39, 
	SUM(o.FieldGoalsMade40to49) AS FieldGoalsMade40to49, 
	SUM(o.FieldGoalsMade50Plus) AS FieldGoalsMade50Plus, 
	SUM(o.ExtraPointsMade) AS ExtraPointsMade, 
	SUM(o.ExtraPointsAttempted) AS ExtraPointsAttempted, 
	CASE 
		WHEN SUM(o.ExtraPointsAttempted) = 0 THEN 0 
		ELSE SUM(o.ExtraPointsMade) / SUM(o.ExtraPointsAttempted) * 100.0
	END AS ExtraPointPercentage, 
	SUM(o.FantasyPointsPPR) AS TotalFantasyPoints, 
	AVG(o.FantasyPointsPPR) AS AvgFantasyPoints 
FROM offense_stats_2022 AS o 
WHERE o.Position = 'K' 
	AND o.PositionCategory = 'ST' 
	AND o.SeasonType = 1 
GROUP BY o.PlayerID, o.Team, o.Position, o.Name 
ORDER BY TotalFantasyPoints DESC;