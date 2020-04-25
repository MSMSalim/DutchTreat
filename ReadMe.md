﻿Install dotnet ef

dotnet tool install --global dotnet-ef

To Create/update a database

dotnet ef database update

Add migration (Creates a new migration folder and add the initialise db script)

dotnet ef migrations add InitialDb

To create the tables with appropriate keys and foreign key relationship execute below
it will take the migration script and apply that

dotnet ef database update


To Create initial seed data migration script

dotnet ef migrations add SeedData