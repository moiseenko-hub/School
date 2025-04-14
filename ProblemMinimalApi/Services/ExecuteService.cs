using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace ProblemMinimalApi.Services;

public class ExecuteService : IExecuteService
{
    public async Task<int> ExecuteCode(string code)
    {
        var options = ScriptOptions
            .Default
            .WithImports("System");

        try
        {
            var result = await CSharpScript.EvaluateAsync<int>(code, options);
            return result;
        }
        catch (CompilationErrorException e)
        {
            throw new Exception(string.Join(Environment.NewLine, e.Diagnostics));
        }
    }
}