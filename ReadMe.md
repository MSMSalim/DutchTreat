﻿#Install dotnet ef

dotnet tool install --global dotnet-ef

#To Create/update a database

dotnet ef database update

#Add migration (Creates a new migration folder and add the initialise db script)

dotnet ef migrations add InitialDb

#To create the tables with appropriate keys and foreign key relationship execute below
#it will take the migration script and apply that

dotnet ef database update


#To Create initial seed data migration script

dotnet ef migrations add SeedData


#Identiy migration add storeuser

dotnet ef migrations add Identity

dotnet ef database drop

#Introduce typescript

 npm install -g typescript

 run tsc from DutchTreat folder and it will generate the js and sourcemap files by looking at tsconfig.json

#Introduce angular

npm install @angular/cli -g

ng new --help

With Dry Run
ng new dutch-app --dryRun --skip-git --inline-template --inline-style

Without Dry Run
ng new dutch-app  --skip-git --inline-template --inline-style

cd dutch-app

ng build

ng serve

#Create new ClientApp Folder in the root of this projexct
and copy all files from src folder in angular dutch-app folder

Then copy angular.json,tsconfig.app.json and tsconfig.json from angualar dutch-app folder to
the root of this project i.e DutchTreat