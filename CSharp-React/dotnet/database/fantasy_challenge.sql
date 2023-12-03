BEGIN TRANSACTION;

CREATE TABLE users (
	user_id SERIAL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	CONSTRAINT PK_user PRIMARY KEY (user_id)
);

CREATE TABLE configuration (
	config_key varchar(50) NOT NULL,
	config_value int NOT NULL,
	CONSTRAINT PK_configuration_config_key PRIMARY KEY (config_key)
);

CREATE TABLE teams (
	team_id int UNIQUE NOT NULL,
	team varchar(10) NOT NULL,
	city varchar(50) NOT NULL,
	name varchar(50) NOT NULL,
	conference varchar(50) NOT NULL,
	division varchar(50) NOT NULL,
	status varchar(50) NOT NULL,
	CONSTRAINT PK_teams_team_id PRIMARY KEY (team_id)
);

CREATE TABLE players (
	player_id int UNIQUE NOT NULL,
	team_id int,
	name varchar(50) NOT NULL,
	position varchar(50) NOT NULL,
	status varchar(50),
	injury_status varchar(50),
	CONSTRAINT PK_players_player_id PRIMARY KEY (player_id),
	CONSTRAINT FK_players_team_id FOREIGN KEY (team_id) REFERENCES teams(team_id)
);

CREATE TABLE player_stats (
	player_id int NOT NULL,
	team_id int NOT NULL,
	season_type int NOT NULL,
	week int NOT NULL,
	name varchar(50) NOT NULL,
	position varchar(50) NOT NULL,
	status varchar(50),
	injury_status varchar(50),
	fantasy_points numeric(5,2),
	CONSTRAINT PK_player_stats PRIMARY KEY (player_id, team_id, season_type, week),
	CONSTRAINT FK_player_stats_player_id FOREIGN KEY (player_id) REFERENCES players(player_id),
	CONSTRAINT FK_player_stats_team_id FOREIGN KEY (team_id) REFERENCES teams(team_id)
);

CREATE TABLE player_projections (
	player_id int NOT NULL,
	team_id int NOT NULL,
	season_type int NOT NULL,
	week int NOT NULL,
	name varchar(50) NOT NULL,
	position varchar(50) NOT NULL,
	status varchar(50),
	injury_status varchar(50),
	fantasy_points numeric(5,2),
	CONSTRAINT PK_player_projections PRIMARY KEY (player_id, team_id, season_type, week),
	CONSTRAINT FK_player_projections_player_id FOREIGN KEY (player_id) REFERENCES players(player_id),
	CONSTRAINT FK_player_projections_team_id FOREIGN KEY (team_id) REFERENCES teams(team_id)
);


CREATE TABLE fantasy_rosters (
	roster_id SERIAL,
	user_id int UNIQUE NOT NULL,
	team_name varchar(50) UNIQUE NOT NULL,
	total_score numeric(5,2),
	CONSTRAINT PK_fantasy_rosters_team_id PRIMARY KEY (roster_id),
	CONSTRAINT FK_fantasy_rosters_user_id FOREIGN KEY (user_id) REFERENCES users(user_id)
);

CREATE TABLE roster_players (
	roster_id int NOT NULL,
	player_id int NOT NULL,
	FOREIGN KEY (roster_id) REFERENCES fantasy_rosters(roster_id),
	FOREIGN KEY (player_id) REFERENCES players(player_id),
	PRIMARY KEY (roster_id, player_id)
);

CREATE TABLE fantasy_lineups (
	lineup_id SERIAL,
	roster_id int NOT NULL,
	game_week int NOT NULL,
	total_score numeric(5,2),
	CONSTRAINT PK_fantasy_lineups_lineup_id PRIMARY KEY (lineup_id),
	CONSTRAINT FK_fantasy_lineups_roster_id FOREIGN KEY (roster_id) REFERENCES fantasy_rosters(roster_id)
);

CREATE TABLE lineup_players (
	lineup_id int NOT NULL,
	player_id int NOT NULL,
	lineup_position varchar(50) NOT NULL,
	FOREIGN KEY (lineup_id) REFERENCES fantasy_lineups(lineup_id),
	FOREIGN KEY (player_id) REFERENCES players(player_id),
	PRIMARY KEY (lineup_id, player_id)
);

CREATE TABLE player_stats_ext (
	player_id int NOT NULL,
	team_id int NOT NULL,
	season_type int NOT NULL,
	week int NOT NULL,
	name varchar(50) NOT NULL,
	position varchar(50) NOT NULL,
	status varchar(50),
	injury_status varchar(50),
	fantasy_points numeric(5,2),
	passing_completions numeric(5,2),
	passing_attempts numeric(5,2),
	passing_yards numeric(5,2),
	passing_touchdowns numeric(5,2),
	passing_interceptions numeric(5,2),
	passing_rating numeric(5,2),
	rushing_attempts numeric(5,2),
	rushing_yards numeric(5,2),
	rushing_touchdowns numeric(5,2),
	receiving_targets numeric(5,2),
	receptions numeric(5,2),
	receiving_yards numeric(5,2),
	receiving_touchdowns numeric(5,2),
	return_touchdowns numeric(5,2),
	two_point_conversions numeric(5,2),
	fumbles_lost numeric(5,2),
	field_goals_made numeric(5,2),
	field_goals_attempted numeric(5,2),
	field_goals_made_0_to_19 numeric(5,2),
	field_goals_made_20_to_29 numeric(5,2),
	field_goals_made_30_to_39 numeric(5,2),
	field_goals_made_40_to_49 numeric(5,2),
	field_goals_made_50_plus numeric(5,2),
	extra_points_made numeric(5,2),
	extra_points_attempted numeric(5,2),
	defensive_touchdowns numeric(5,2),
	special_teams_touchdowns numeric(5,2),
	touchdowns_scored numeric(5,2),
	fumbles_forced numeric(5,2),
	fumbles_recovered numeric(5,2),
	interceptions numeric(5,2),
	tackles_for_loss numeric(5,2),
	quarterback_hits numeric(5,2),
	sacks numeric(5,2),
	safeties numeric(5,2),
	blocked_kicks numeric(5,2),
	points_allowed numeric(5,2),
	CONSTRAINT PK_player_stats_ext PRIMARY KEY (player_id, team_id, season_type, week),
	CONSTRAINT FK_player_stats_ext_player_id FOREIGN KEY (player_id) REFERENCES players(player_id),
	CONSTRAINT FK_player_stats_ext_team_id FOREIGN KEY (team_id) REFERENCES teams(team_id)
);

CREATE TABLE player_projections_ext (
	player_id int NOT NULL,
	team_id int NOT NULL,
	season_type int NOT NULL,
	week int NOT NULL,
	name varchar(50) NOT NULL,
	position varchar(50) NOT NULL,
	status varchar(50),
	injury_status varchar(50),
	fantasy_points numeric(5,2),
	passing_completions numeric(5,2),
	passing_attempts numeric(5,2),
	passing_yards numeric(5,2),
	passing_touchdowns numeric(5,2),
	passing_interceptions numeric(5,2),
	passing_rating numeric(5,2),
	rushing_attempts numeric(5,2),
	rushing_yards numeric(5,2),
	rushing_touchdowns numeric(5,2),
	receiving_targets numeric(5,2),
	receptions numeric(5,2),
	receiving_yards numeric(5,2),
	receiving_touchdowns numeric(5,2),
	return_touchdowns numeric(5,2),
	two_point_conversions numeric(5,2),
	fumbles_lost numeric(5,2),
	field_goals_made numeric(5,2),
	field_goals_attempted numeric(5,2),
	field_goals_made_0_to_19 numeric(5,2),
	field_goals_made_20_to_29 numeric(5,2),
	field_goals_made_30_to_39 numeric(5,2),
	field_goals_made_40_to_49 numeric(5,2),
	field_goals_made_50_plus numeric(5,2),
	extra_points_made numeric(5,2),
	extra_points_attempted numeric(5,2),
	defensive_touchdowns numeric(5,2),
	special_teams_touchdowns numeric(5,2),
	touchdowns_scored numeric(5,2),
	fumbles_forced numeric(5,2),
	fumbles_recovered numeric(5,2),
	interceptions numeric(5,2),
	tackles_for_loss numeric(5,2),
	quarterback_hits numeric(5,2),
	sacks numeric(5,2),
	safeties numeric(5,2),
	blocked_kicks numeric(5,2),
	points_allowed numeric(5,2),
	CONSTRAINT PK_player_projections_ext PRIMARY KEY (player_id, team_id, season_type, week),
	CONSTRAINT FK_player_projections_ext_player_id FOREIGN KEY (player_id) REFERENCES players(player_id),
	CONSTRAINT FK_player_projections_ext_team_id FOREIGN KEY (team_id) REFERENCES teams(team_id)
);


COMMIT TRANSACTION;