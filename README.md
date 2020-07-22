# Bakery Live Code

This is a simple `dotnet new react` project with a few things added:

  - The React client in `ClientApp/` does not automatically run with `dotnet run`. You have to manually start it with `npm start` in the `ClientApp/` folder. The client app is fully functional but tests have not been written yet to prove it.
  - PostgreSQL support has been added already. Just uncomment the relevant setup lines in `Startup.cs`
  - Controllers and Model stubs have been created in anticipation of in-class work.

To get the API hooked up:
  - `dotnet tool install --global dotnet-ef`
  - Get your models set up properly in `Models/`
  - Add your HTTP methods in the relevant `Controllers/` file.
  - Add your models to `Models/ApplicationContext` to make the database-aware
  - Make sure your Postgres connection string is valid in `appsettings.json`
  - Run your migrations: `dotnet ef migrations add Initial` and `dotnet ef database update`, etc. To rollback: `dotnet ef migrations update 0` or just manually delete the tables from postico.

This document will be updated.

BAKER
-----

id (int, primary key)
name (string)
emailAddress (string, required, email address format)

REST API
--------

```
GET /bakers/
HTTP BODY: NONE
[
    {
        "id": 1,
        "name": "blaine",
        "emailAddress": "me@email.com"
    },
    {
        "id": 2,
        "name": "levi",
        "emailAddress": "me@levi.com"
    }
]

POST /bakers/
HTTP BODY:
{
    "name": "blaine",
    "emailAddress": "me@email.com"
}
RESPONSE: 201 CREATED OR 400 BAD REQUEST (for invalid data)
{
    "id": 1,
    "name": "blaine",
    "emailAddress": "me@email.com"
}

GET /bakers/1
HTTP BODY: NONE
RESPONSE: 200 OK OR 404 if id is invalid
{
    "id": 1,
    "name": "blaine",
    "emailAddress": "me@email.com"
}

DELETE /bakers/1
HTTP BODY: NONE
RESPONSE: 204 NO-CONTENT

PUT /bakers/1
HTTP BODY:
{
    "id": 1,
    "name": "blaine booher",
    "emailAddress": "blaine@email.com"
}
RESPONSE: 200 OK OR 400 BAD REQUEST (for invalid data)
{
    "id": 1,
    "name" "blaine booher",
    "emailAddress": "blaine@email.com"
}
```

BREAD INVENTORY
---------------

id (int, primary key)
name (string): name of the bread
inventory (int): how many there are
breadType (enum string): What type of bread it is: sourdough, focassia, rye
bakerId (int, foreign key): who baked this bread?


What are our steps:
-------------------

0. [x] We'll turn on postgres support in Startup.cs
1. [x] We'll create our Baker class; we'll also add attributes that help us define validations
2. [ ] We'll HOOK UP our Baker class to the database
    a. [x] Adding it to the ApplicationContext
    b. [x] Add a migration
    c. [x] Run the migration
3. [ ] We'll write the controller methods (making sure the controller knows about ApplicationContext)
4. Test our controller methods with postman.