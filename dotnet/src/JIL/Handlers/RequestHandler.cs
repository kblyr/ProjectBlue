namespace JIL.Handlers;

public interface IRequestHandler<T> : MediatR.IRequestHandler<T, IResponse> where T : IRequest { }