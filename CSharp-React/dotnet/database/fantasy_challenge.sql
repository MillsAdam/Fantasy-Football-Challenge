BEGIN TRANSACTION;

CREATE TABLE users (
	user_id SERIAL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	CONSTRAINT PK_user PRIMARY KEY (user_id)
);

CREATE TABLE teams (
	team_id int UNIQUE NOT NULL,
	team varchar(10) NOT NULL,
	city varchar(50) NOT NULL,
	name varchar(50) NOT NULL,
	conference varchar(50) NOT NULL,
	division varchar(50) NOT NULL,
	full_name varchar(100) NOT NULL,
	status varchar(50) NOT NULL,
	CONSTRAINT PK_teams_team_id PRIMARY KEY (team_id)
);

CREATE TABLE players (
	player_id int UNIQUE NOT NULL,
	team_id int,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	position varchar(50) NOT NULL,
	position_category varchar(10),
	status varchar(50),
	injury_status varchar(50),
	CONSTRAINT PK_players_player_id PRIMARY KEY (player_id),
	CONSTRAINT FK_players_team_id FOREIGN KEY (team_id) REFERENCES teams(team_id)
);

CREATE TABLE fantasy_rosters (
	roster_id SERIAL,
	user_id int UNIQUE NOT NULL,
	team_name varchar(50) UNIQUE NOT NULL,
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
	CONSTRAINT PK_fantasy_lineups_lineup_id PRIMARY KEY (lineup_id),
	CONSTRAINT FK_fantasy_lineups_roster_id FOREIGN KEY (roster_id) REFERENCES fantasy_rosters(roster_id)
);

CREATE TABLE lineup_players (
	lineup_id int NOT NULL,
	player_id int NOT NULL,
	position varchar(50) NOT NULL,
	FOREIGN KEY (lineup_id) REFERENCES fantasy_lineups(lineup_id),
	FOREIGN KEY (player_id) REFERENCES players(player_id),
	PRIMARY KEY (lineup_id, player_id)
);

COMMIT TRANSACTION;