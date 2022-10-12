using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public abstract class ExceptionProblemDetailsBase : ProblemDetails
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
