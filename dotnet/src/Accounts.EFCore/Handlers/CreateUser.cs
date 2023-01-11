using JIL.Accounts.Security;
using JIL.Accounts.Utilities;

namespace JIL.Accounts.Handlers;

sealed class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    readonly IDbContextFactory<AccountsDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly IUserPasswordF2BHashAlgorithm _passwordHashAlgorithm;
    readonly IOptions<UserStatusesLookup> _statuses;
    readonly IUserFullNameBuilder _fullNameBuilder;
    readonly IRoleVerifier _roleVerifier;
    readonly IOptions<RolesLookup> _roles;
    readonly MediatR.IMediator _mediator;
    readonly IOptions<PermissionsLookup> _permissions;
    readonly IPermissionVerifier _permissionsVerifier;

    public CreateUserHandler(IDbContextFactory<AccountsDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider, IUserPasswordF2BHashAlgorithm passwordHashAlgorithm, IOptions<UserStatusesLookup> statuses, IUserFullNameBuilder fullNameBuilder, IRoleVerifier roleVerifier, IOptions<RolesLookup> roles, MediatR.IMediator mediator, IOptions<PermissionsLookup> permissions, IPermissionVerifier permissionsVerifier)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
        _passwordHashAlgorithm = passwordHashAlgorithm;
        _statuses = statuses;
        _fullNameBuilder = fullNameBuilder;
        _roleVerifier = roleVerifier;
        _roles = roles;
        _mediator = mediator;
        _permissions = permissions;
        _permissionsVerifier = permissionsVerifier;
    }

    public async Task<IResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        if (await context.Users.Where(_ => _.Username == request.Username).AnyAsync(cancellationToken))
        {
            return new UsernameAlreadyExistsError { Username = request.Username };
        }

        var auditInfo = _auditInfoProvider.Current;
        var statuses = _statuses.Value;
        var roles = _roles.Value;
        var permissions = _permissions.Value;
        var statusId = statuses.Pending;
        var password = await _passwordHashAlgorithm.ComputeAsync(request.CipherPassword, cancellationToken);

        if (await _roleVerifier.Verify(roles.Administrator, cancellationToken) || await _permissionsVerifier.Verify(permissions.ApproveUser, cancellationToken))
        {
            statusId = statuses.Active;
        }

        var user = _mapper.Map<CreateUserCommand, User>(request) with 
        {
            FullName = _fullNameBuilder.Build(request),
            StatusId = statusId,
            HashedPassword = password.HashedPassword,
            PasswordSalt = password.Salt,
            IsDeleted = false,
            InsertedById = auditInfo.User.Id,
            InsertedOn = auditInfo.Timestamp
        };
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        await _mediator.Publish(_mapper.Map<User, UserCreatedEvent>(user) with 
        {
            CreatedBy = _mapper.GetLoggedUser(auditInfo)
        }, cancellationToken);
        return new CreateUserCommand.Response { Id = user.Id };
    }
}
