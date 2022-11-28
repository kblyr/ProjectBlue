using JIL.Accounts.Lookups;
using JIL.Accounts.Security;
using JIL.Accounts.Utilities;
using JIL.Auditing;
using MapsterMapper;

namespace JIL.Accounts.Handlers;

sealed class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    readonly IDbContextFactory<AccountsDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly IUserPasswordF2BHashAlgorithm _passwordHashAlgorithm;
    readonly IOptions<UserStatusesLookup> _statuses;
    readonly IUserFullNameBuilder _fullNameBuilder;

    public CreateUserHandler(IDbContextFactory<AccountsDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider, IUserPasswordF2BHashAlgorithm passwordHashAlgorithm, IOptions<UserStatusesLookup> statuses, IUserFullNameBuilder fullNameBuilder)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
        _passwordHashAlgorithm = passwordHashAlgorithm;
        _statuses = statuses;
        _fullNameBuilder = fullNameBuilder;
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
        var password = await _passwordHashAlgorithm.ComputeAsync(request.CipherPassword, cancellationToken);
        var user = _mapper.Map<CreateUserCommand, User>(request) with 
        {
            FullName = _fullNameBuilder.Build(request),
            StatusId = statuses.Pending,
            HashedPassword = password.HashedPassword,
            PasswordSalt = password.Salt,
            IsDeleted = false,
            InsertedById = auditInfo.UserId,
            InsertedOn = auditInfo.Timestamp
        };
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return new CreateUserCommand.Response { Id = user.Id };
    }
}
