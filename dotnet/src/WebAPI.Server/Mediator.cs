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
    readonly MediatR.IMediator _mediator;
    readonly MapsterMapper.IMapper _mapper;
    readonly IResponseMapper _responseMapper;

    public ApiMediator(MediatR.IMediator mediator, MapsterMapper.IMapper mapper, IResponseMapper responseMapper)
    {
        _mediator = mediator;
        _mapper = mapper;
        _responseMapper = responseMapper;
    }

    public async Task<IResponse> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        var requestTo = _mapper.Map<TRequestFrom, TRequestTo>(requestFrom);
        return await _mediator.Send(requestTo, cancellationToken);
    }

    public async Task<IResponse> Send<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo> mutateRequest, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        var requestTo = mutateRequest(_mapper.Map<TRequestFrom, TRequestTo>(requestFrom));
        return await _mediator.Send(requestTo, cancellationToken);
    }

    public async Task<IActionResult> SendThenMap<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        var response = await Send<TRequestFrom, TRequestTo>(requestFrom, cancellationToken);
        return _responseMapper.Map(response);
    }

    public async Task<IActionResult> SendThenMap<TRequestFrom, TRequestTo>(TRequestFrom requestFrom, MutateRequest<TRequestTo> mutateRequest, CancellationToken cancellationToken = default)
        where TRequestFrom : IApiRequest
        where TRequestTo : IRequest
    {
        var response = await Send<TRequestFrom, TRequestTo>(requestFrom, mutateRequest, cancellationToken);
        return _responseMapper.Map(response);
    }
}
