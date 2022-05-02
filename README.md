# groc-list-api

## Requirments
- [.NET5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Docker Desktop (Linux/Mac/Windows)](https://www.docker.com/)
- [Visual Studio 2019+ (Mac/Windows)](https://visualstudio.microsoft.com/vs/community/) or [Visual Studio Code](https://code.visualstudio.com/)
- [dotnet-ef cli](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) required on host for migrations

## Install 
- Clone repo 
- Open in Visual Studio or run `dotnet restore`
- When in Visual Studio make sure Docker extension is installed 
- Run `docker volume create pg_volume` if it does not exists. Use `docker volume ls` to determine if the volume needs to be created.
- Run project with `docker-compose up` or execute debugging in Visual Studio 

## Dev Db 
- If running form docker, an instance of postgres will automatically be provided. This database will be created and reused when running through docker-compose. 
 - Below are commands to wipe the db
 
 
 ```
 DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
GRANT ALL ON SCHEMA public TO postgres;
 ```