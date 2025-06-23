# ThoughtFul
A basic API for a blogging platform, built with .NET 6 WEB API


# Run the project using dockerfile :
- docker build . -t thoughtfulapi

- # Run the project using docker-compose :
-docker-compose up --build

- # when doing a change in the code , run :
-docker-compose down -v
-docker-compose up --build

- # list all running containers :
- docker container ps

- # go inside the bash terminal of a container :
- docker exec -it thoughtfulapi /bin/bash
-ls /app/wwwroot/uploads/contributions/

