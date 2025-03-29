namespace ProblemMinimalApi.DatabaseAccessLayer;

public class MethodData
{
    public string Name { get; set; } = string.Empty;
    public List<ParamInfo> Params { get; set; } = new List<ParamInfo>();
    public string ReturnType { get; set; } = string.Empty;
}