namespace JIL;

public interface IApiMediator
{
    Task<IResponse> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest;

    Task<IResponse> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo> mutateRequest, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest;

    Task<IActionResult> SendThenMap<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest;

    Task<IActionResult> SendThenMap<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo> mutateRequest, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest;
}

public delegate T MutateRequest<T>(T request);

sealed class ApiMediator : IApiMediator
{
    public Task<IResponse> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        throw new NotImplementedException();
    }

    public Task<IResponse> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo> mutateRequest, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> SendThenMap<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> SendThenMap<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo> mutateRequest, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        throw new NotImplementedException();
    }
}
