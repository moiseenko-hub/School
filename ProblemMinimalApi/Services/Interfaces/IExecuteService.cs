namespace ProblemMinimalApi.Services;

public interface IExecuteService
{ 
    Task<int>? ExecuteCode(string code);
}