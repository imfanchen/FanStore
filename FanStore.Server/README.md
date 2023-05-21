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

# View all the existing json web tokens
dotnet user-jwts list

# View the token's header, payload, and signature
dotnet user-jwts print <your-jwt-id>

# Create role based authorization
dotnet user-jwts create --role "admin"

# Create claims based authorization
dotnet user-jwts create --scope "books:read"

# Create authorization with a role and a resource permission
dotnet user-jwts create --role "admin" --scope "books:write" 

# Remove a specific token
dotnet user-jwts remove <your-jwt-id>

# Delete all the existing json web tokens
dotnet user-jwts clear
```
