using FastEndpoints;
using JIL;
using JIL.Accounts;
using JIL.WebAPI;
using JIL.WebAPI.Server;
using MediatR;

var endpointAssemblies = new[]
{
    JIL.Accounts.WebAPI.Server.AssemblyMarker.Assembly
};

var requestHandlerAssemblies = new[]
{
    JIL.Accounts.EFCore.AssemblyMarker.Assembly
};

var mappingAssemblies = new[]
{
    JIL.Accounts.AssemblyMarker.Assembly,
    JIL.Accounts.WebAPI.Server.AssemblyMarker.Assembly
};

var responseTypeMapAssemblies = new[]
{
    JIL.Accounts.WebAPI.Server.AssemblyMarker.Assembly
};

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFastEndpoints(options => options.Assemblies = endpointAssemblies)
    .AddMediatR(requestHandlerAssemblies)
    .AddMapster(mappingAssemblies)
    .AddJIL()
    .AddJILWebAPI()
    .AddJILWebAPIServer(options => options.ResponseTypeMapAssemblies = responseTypeMapAssemblies);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();
