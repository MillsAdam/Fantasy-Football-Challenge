#!/bin/bash
export PGPASSWORD='postgres1'
BASEDIR=$(dirname $0)
DATABASE=fantasy_stats
for i in 18 17 16 15 14 13 12 11 10 9
do
    file="c:/program files/postgresql/$i/"
    FILEPATH="c:/program files/postgresql/$i/bin"
    if [ -d "$file" ]
    then
        echo "Postgres version $i found."
        "$FILEPATH/psql.exe" -U postgres -f "$BASEDIR/dropdb.sql" &&
        "$FILEPATH/createdb.exe" -U postgres $DATABASE &&
        "$FILEPATH/psql.exe" -U postgres -d $DATABASE -f "$BASEDIR/schema.sql" &&
        "$FILEPATH/psql.exe" -U postgres -d $DATABASE -f "$BASEDIR/data.sql" &&
        "$FILEPATH/psql.exe" -U postgres -d $DATABASE -f "$BASEDIR/user.sql"
        break
    fi
done