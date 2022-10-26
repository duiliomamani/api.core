using MediatR;

namespace AspNetCore.Common.Wrappers
{
    public interface IRequestWrapper<T> : IRequest<TResponse<T>> { }
    public interface IRequestWrapper : IRequest<TResponse> { }
    public interface IRequestHandlerWrapper<TIn> : IRequestHandler<TIn, TResponse> where TIn : IRequestWrapper { }
    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, TResponse<TOut>> where TIn : IRequestWrapper<TOut> { }
}
