# Bakery Live Code

This is a simple `dotnet new react` project with a few things added:

  - The React client in `ClientApp/` does not automatically run with `dotnet run`. You have to manually start it with `npm start` in the `ClientApp/` folder. The client app is fully functional but tests have not been written yet to prove it.
  - PostgreSQL support has been added already. Just uncomment the relevant setup lines in `Startup.cs`
  - Controllers and Model stubs have been created in anticipation of in-class work.

To get the API hooked up:
  - Get your models set up properly in `Models/`
  - Add your HTTP methods in the relevant `Controllers/` file.
  - Add your models to `Models/ApplicationContext` to make the database-aware
  - Make sure your Postgres connection string is valid in `appsettings.json`
  - Run your migrations: `dotnet ef migrations add Initial` and `dotnet ef database update`, etc. To rollback: `dotnet ef migrations update 0` or just manually delete the tables from postico.

This document will be updated.