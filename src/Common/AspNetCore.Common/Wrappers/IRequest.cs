using MediatR;

namespace AspNetCore.Common.Wrappers
{
    public interface IRequestWrapper<T> : IRequest<Response<T>> { }
    public interface IRequestWrapper : IRequest<Response> { }
    public interface IRequestHandlerWrapper<TIn> : IRequestHandler<TIn, Response> where TIn : IRequestWrapper { }
    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, Response<TOut>> where TIn : IRequestWrapper<TOut> { }
}
