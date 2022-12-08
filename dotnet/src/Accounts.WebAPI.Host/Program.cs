using FastEndpoints;
using JIL;
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

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFastEndpoints(options => options.Assemblies = endpointAssemblies)
    .AddMediatR(requestHandlerAssemblies)
    .AddMapster(mappingAssemblies);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();
