using FastEndpoints;
using JIL;
using JIL.Accounts;
using JIL.Accounts.Lookups;
using JIL.EFCore;
using JIL.WebAPI;
using JIL.WebAPI.Server;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
    .AddJILWebAPIServer(options => options.Features.ResponseTypeMapRegistry.Assemblies = responseTypeMapAssemblies)
    .AddJILAccounts(options => options.Security.UserPasswordF2BDecryption.PemFilePath = builder.Configuration["JIL:Accounts:Security:UserPasswordF2BDecryption:PemFilePath"])
    .AddJILAccountsLookups(builder.Configuration)
    .AddDbContextFactory<AccountsDbContext>(JIL.Accounts.EFCore.PostgreSQL.AssemblyMarker.Assembly, options => options.UseNpgsql(builder.Configuration["JIL:Accounts:ConnectionStrings:PostgreSQL"]));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();
