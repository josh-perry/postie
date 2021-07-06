# postie
A Reddit clone made for fun!

## Features
* User-created boards
* Posts
* Comment chains
* User profile pages for viewing recent posting history

## Building and running
### API
* From the Backend folder
* Run `dotnet run`
* Check the API is alive by browsing to https://localhost:5001/swagger/index.html

On startup the database (app.db) will be created if it doesn't already exist. If in development mode, it will also be seeded with random test data.

### Frontend
* From the Frontend folder
* Run `npm run serve`
* Browse to http://localhost:3000

## Technologies
### Postie.API
* C# 8
* ASP.NET Core
* EF Core 5
* Swagger
* SQLite as backing store

### Frontend
* Vue
* Axios

### etc.
* Auth0 for authentication
