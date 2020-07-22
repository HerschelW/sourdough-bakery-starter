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

BAKER
-----
id (integer)
name (string)

REST API:
----------

GET /bakers/
HTTP BODY: NONE
{
  {
    "id": 1,
    "name": "jared"
  },
  {
    "id": 2,
    "name": "bob"
  }
}
POST /bakers/
HTTP BODY:
{
  "name": "jared" 
}
RESPONSE: 201 CREATED, with the actual baker object returned

GET /bakers/1
HTTP BODY: NONE
RESPONSE: 200 OK
{
  "id": 1,
  "name": "jared",
}

DELETE /bakers/1
HTTP BODY: NONE
RESPONSE: 204 NO-CONTENT

PUT /bakers/1
HTTP BODY:
{
  "id": 1,
  "name": "jared"
}
RESPONSE 200 OK
{
  "id": 1,
  "name": "jared"
}

BREAD INVENTORY
-------------------




What are our steps:
------------------
0. We'll turn on postgres support in startup.cs
1. we'll create our baker class; we'll also add attributes that help us define validations
2. we'll hook up our baker class to the database
  a. Adding it to the applicationContext
  b. Add a migration
  c. Run the migration
3. We'll write the controller methods (making sure the controller knows about ApplicationContext)
4. Test our controler methods with postman