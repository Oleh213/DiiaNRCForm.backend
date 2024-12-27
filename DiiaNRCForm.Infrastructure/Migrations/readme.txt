
1. How to add a new migration step:
run this command in the solution folder

linux vs windows slash vs backslash
dotnet ef migrations add Init_Database --project DiiaNRCForm.Infrastructure/DiiaNRCForm.Infrastructure.csproj --startup-project DiiaNRCForm/DiiaNRCForm.csproj --context DiiaNRCFormDbContext

2. Apply the step to your database:
dotnet ef database update --project DiiaNRCForm.Infrastructure/DiiaNRCForm.Infrastructure.csproj --startup-project DiiaNRCForm/DiiaNRCForm.csproj --context DiiaNRCFormDbContext
