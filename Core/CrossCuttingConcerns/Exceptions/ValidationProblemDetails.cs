namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ValidationProblemDetails : ExceptionProblemDetailsBase
    {
        public object Errors { get; set; }
    }
}
