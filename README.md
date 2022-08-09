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

Restore project dependencies. Running the following command at Solution's folder.
```bash
dotnet restore

eg:
C:\Repositories\NetUnitOfWorkRepository> dotnet restore
```

Apply migration to create the data base. At solution's folder just execute the following command.
```bash
dotnet ef database update -p .\Fnunez.UnitOfWork.Data\ -s .\Fnunez.UnitOfWork.Api\ -c UnitOfWorkDbContext -v

eg:
C:\Repositories\NetUnitOfWorkRepository> dotnet ef database update -p .\Fnunez.UnitOfWork.Data\ -s .\Fnunez.UnitOfWork.Api\ -c UnitOfWorkDbContext -v
```

## Test API
At APi folder run the following command to test the CRUD methods through Swagger.
```bash
dotnet watch run

eg:
C:\Repositories\NetUnitOfWorkRepository\Fnunez.UnitOfWork.Api> dotnet watch run
```

(n_n)/