SELECT pg_terminate_backend(pid)
FROM pg_stat_activity
WHERE datname = 'fantasy_challenge';

DROP DATABASE fantasy_challenge;


DROP USER fantasy_appuser;