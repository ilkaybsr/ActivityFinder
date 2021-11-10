# ActivityFinder
# Migration Olusturma
dotnet ef migrations add update3 -p DataAccess -s ActivityFinder 

# Update DB
dotnet ef database update -p DataAccess -s ActivityFinder 
