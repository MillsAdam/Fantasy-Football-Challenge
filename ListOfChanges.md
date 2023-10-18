# List of Changes from Original Final Capstone Code


## Database

* Incorporated Eric Cameron's updates to create.sh script which prevent the need to have the postgreSQL path set up a particular way (this makes deploying on students' laptops far easier).

## Java
* Fixed issue where registering with a username containing uppercase characters prevented users from being able to log in successfully


## Vue
* Added src-maps to allow easier Vue dev tools debugging
* Modified message on Home view to read  "If you are seeing this, you are authenticated." instead of "You must be authenticated to see this."