# nunit-integration-testing
Small example of using sql server running in docker to write integration tests in a dotnet core application


## Running Tests With Docker

Ensure that the mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04 is installed and running

```
- docker pull mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
- docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Test123!" -p 1433:1433 --name sql1 -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```