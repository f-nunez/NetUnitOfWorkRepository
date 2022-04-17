# NetUnitOfWorkRepository
Base unit of work with repository pattern in .NET 6

## About
The project contains model, data and business layers to show the implementation.
Even include an Api project for testing purpose.

## Setup
Install as global vars the ef command for easy life.
```bash
dotnet tool install --global dotnet-ef
```

At solution's root folder you should execute the following command to create the data base.
```bash
dotnet ef database update -p .\Fnunez.UnitOfWork.Data\ -s .\Fnunez.UnitOfWork.Web\ -c UnitOfWorkDbContext -v
```

## API on swagger
Test the CRUD methods through Swagger.
https://localhost:your_port/Swagger/index.html