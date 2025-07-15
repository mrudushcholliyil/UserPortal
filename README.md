We used .Net 8 follows Clean architecture using 
CQRS and MediatR, 
Entity framework core sql server, 
global exception handling, 
generic Repository pattern,
Dependency injection, 
DTOs and Automapper, 
Serilog, 
IOptions 

To run this application, please update SqlServerConnectionString and
Run below commands to create tables in sql server based on entity provided.

open NuGet Package manager console
run command 1 > Add-Migration dbinit
run command 2 > Update-Database
