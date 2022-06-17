# Smart Charging

This repository holds an implementation for
the Smart Charging.

## About

This is a try to domain driven approach, And in this project I implemented everything that can show a simple
intruduction of DDD, unit and integration tests, FluentValidation, and more.

## How to run

- Run SqlServer

    - If you have the `SQL Server` installed locally just skip this step
    - If you want to install `SQL Server` in docker run command below
    ```bash
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word"\
    -p 1433:1433 --name sql-dev -h sql-dev\
    -d mcr.microsoft.com/mssql/server:2019-latest
    ```
- Change the connection string in appsettings.Development.json if it's needed (You just need to change username and
  password in connection string)


- Database migrations

   ```text
    Database migrations will be automatically applied after running API project
    ```
- If you don't have .NET6, download and install the latest vertion of .Net6

- Restore dependencies

  After installing .NET 6 use dotnet restore command to restore all package dependencies.

  ```bash
  dotnet restore
  ```

- Run the API project

    ```bash
    dotnet run --project  src/API
    ```

- Open this link browser in your browser:  https://localhost:7058/swagger/index.html

# Usage

The API has Swagger and it's easy to use :

1. First of all you should add Groups from by calling post in Group API section
2. The second step is adding ChargeStation by calling post in ChargeStation API section
3. The final step is adding connectors, you can add them by calling post in Connector API section

You can also call GET, PATCH, and DELETE methods of each API to get update or remove groups, charge stations, and
connector.

## Technologies

- .Net 6
- Sql Server

## Libraries

- EntityFramework
- AutoMapper
- FluentValidation
- xunit
- Moq

## Architecture

This project is a try to Domain Driven Design approach using Microsoft sample reference application as template.

[Reference is here](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice
)

Figure below shows how a layered design is implemented in a sample DDD application:
![alt text](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/media/ddd-oriented-microservice/domain-driven-design-microservice.png)

And figure below shows dependencies between layers:
![alt text](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/media/ddd-oriented-microservice/ddd-service-layer-dependencies.png
)
