SELECT 
	o.PlayerID, 
	COUNT(DISTINCT o.Week) AS Week, 
	o.Team, 
	o.Position, 
	o.Name, 
	SUM(o.PassingCompletions) AS PassingCompletions, 
	SUM(o.PassingAttempts) AS PassingAttempts, 
	CASE 
	WHEN SUM(o.PassingAttempts) = 0 THEN 0 
	ELSE SUM(o.PassingCompletions) * 100.0 / SUM(o.PassingAttempts) 
	END AS PassingCompletionPercentage, 
	SUM(o.PassingYards) AS PassingYards, 
	SUM(o.PassingTouchdowns) AS PassingTouchdowns, 
	SUM(o.PassingInterceptions) AS PassingInterceptions, 
	AVG(o.PassingRating) AS PassingRating, 
	SUM(o.RushingAttempts) AS RushingAttempts, 
	SUM(o.RushingYards) AS RushingYards, 
	SUM(o.RushingTouchdowns) AS RushingTouchdowns, 
	SUM(o.TwoPointConversionPasses + o.TwoPointConversionRuns + o.TwoPointConversionReceptions) AS TwoPointConversions, 
	SUM(o.FumblesLost) AS FumblesLost, 
	SUM(o.FantasyPointsPPR) AS TotalFantasyPoints, 
	AVG(o.FantasyPointsPPR) AS AvgFantasyPoints 
FROM offense_stats_2022 AS o 
WHERE o.Position = 'QB' 
	AND o.PositionCategory = 'OFF' 
	AND o.SeasonType = 1 
GROUP BY o.PlayerID, o.Team, o.Position, o.Name 
ORDER BY TotalFantasyPoints DESC;
			
			
			