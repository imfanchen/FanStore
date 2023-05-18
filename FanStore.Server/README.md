# Fan Store Server

# Store App Credentials

Save the connection string to the Secret Manager

```powershell
dotnet user-secrets init
$sa_password = "<YOUR-SA-PASSWORD-GOES-HERE>"
dotnet user-secrets set "ConnectionString:FanStoreContext" "Server=localhost; Database=FanStore; User Id=sa; Password=$sa_password; TrustServiceCertificate=True"
```