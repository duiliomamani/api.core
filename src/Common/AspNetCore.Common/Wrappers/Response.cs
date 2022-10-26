namespace AspNetCore.Common.Wrappers
{
    /// <summary>
    /// A standard response for service calls.
    /// </summary>
    /// <typeparam name="T">Return data type</typeparam>
    public class TResponse<T> : TResponse
    {
        public T Data { get; set; }
        public TResponse(T data)
        {
            Data = data;
        }

    }
    public class TResponse
    {
        public bool IsSuccessfull { get; set; } = true;
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public Pagination Pagination { get; set; }
    }
    public class Pagination
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
