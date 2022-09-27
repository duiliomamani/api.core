using MediatR;

namespace AspNetCore.Common.Wrappers
{
    public interface IRequestWrapper<T> : IRequest<Response<T>> { }

    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, Response<TOut>> where TIn : IRequestWrapper<TOut> { }
}
