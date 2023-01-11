using FastEndpoints;
using JIL;
using JIL.Authorization;
using JIL.Authorization.Converters;
using JIL.Converters;
using JIL.EFCore;
using JIL.WebAPI;
using JIL.WebAPI.Server;
using MediatR;
using Microsoft.EntityFrameworkCore;

var endpointAssemblies = new[]
{
    JIL.WebAPI.Server.AssemblyMarker.Assembly
};

var requestHandlerAssemblies = new[]
{
    JIL.EFCore.AssemblyMarker.Assembly
};

var mappingAssemblies = new[]
{
    JIL.WebAPI.Server.AssemblyMarker.Assembly
};

var responseTypeMapAssemblies = new[]
{
    JIL.WebAPI.Server.AssemblyMarker.Assembly
};

var builder = WebApplication.CreateBuilder(args);

builder.Services
    // .AddAuthentication()
    .AddFastEndpoints(options => options.Assemblies = endpointAssemblies)
    .AddMediatR(requestHandlerAssemblies)
    .AddMapster(mappingAssemblies)
    .AddJIL()
    .AddJILEFCore()
    .AddJILWebAPI()
    .AddJILWebAPIServer(options => options.Features.ResponseTypeMapRegistry.Assemblies = responseTypeMapAssemblies)
    .AddDbContextFactory<AuthorizationDbContext>(JIL.EFCore.PostgreSQL.AssemblyMarker.Assembly, options => options.UseNpgsql(builder.Configuration["JIL:Authorization:ConnectionStrings:PostgreSQL"]))
    .AddHashIdConverter<UserIdConverter>()
    .AddHashIdConverter<RoleIdConverter>()
    .AddHashIdConverter<UserRoleIdConverter>()
    .AddHashIdConverter<PermissionIdConverter>()
    .AddHashIdConverter<UserPermissionIdConverter>()
    ;

var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultExceptionHandler();
// app.UseAuthorization();
app.UseFastEndpoints(c => c.Errors.UseJILResponseBuilder());
app.Run();
