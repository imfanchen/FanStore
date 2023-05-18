# Fan Store Database

## Starting SQL Server

Run Microsoft SQL Server in a Docker container

```powershell
$sa_password = "<YOUR-SA-PASSWORD-GOES-HERE>"
docker run -e "ACCEPT_EULA=Y" -e "MYSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v mssql_volume:/var/opt/mssql -d --rm --name mssql_instance mcr.microsoft.com/mssql/server:2022-latest
```