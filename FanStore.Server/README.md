# Fan Store Server

## Store App Credentials

Save the connection string to the Secret Manager

```powershell
dotnet user-secrets init

$sa_password = "<YOUR-SA-PASSWORD-GOES-HERE>"
dotnet user-secrets set "ConnectionString:FanStoreContext" "Server=localhost; Database=FanStore; User Id=sa; Password=$sa_password; TrustServerCertificate=True"

dotnet user-secrets list
```

## Create Database Context
```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

## Create Database Migration
```powershell
dotnet tool install --global dotnet-ef

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add InitialCreation --output-dir Data\Migrations
```

## Apply Database Migration
```powershell
dotnet ef database update
```

## Use Access Tokens
```powershell
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

# Create token based authentication
dotnet user-jwts create

# View the details of JWT's header, payload, and signature
dotnet user-jwts print <previous-generated-jwt-id>

# Create role based authorization
dotnet user-jwts create --role "Admin"
```
