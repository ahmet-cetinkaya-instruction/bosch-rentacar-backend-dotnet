using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public string FullName { get; set; }
    public string MethodName { get; set; }
    public List<LogParameter> Parameters { get; set; }
    public string User { get; set; }

    // todo?
    public virtual string ToJson() => JsonConvert.SerializeObject(this);
}