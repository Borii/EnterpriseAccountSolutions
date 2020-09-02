# EnterpriseAccountSolutions

## Deployment

Prerequisites:
Dotnet SDK installed

In command line for each project run:
`dotnet restore`
`dotnet build`
`dotnet run`

Projects:
AccountApi - account microservice, hosted locally on `http://localhost:5001`
TransactionApi - transaction microservice, `http://localhost:5002`
CustomerApi - customer endpoint `http://localhost:5000` with pre-seeded data

Each service stores data in SQLite database on disk.
Account Api uses in memory database for testing the data access layer.

AccountAPi and Transaction api are both structured into separate layers:
Api layer with controller logic
Core layer which hosts business logic
And data access layer

Or download the executables from Azure pipelines:
https://dev.azure.com/borjanasavnik/Enterprise%20Account%20Solutions/_build?definitionId=1&_a=summary&repositoryFilter=1&branchFilter=3%2C3%2C3
