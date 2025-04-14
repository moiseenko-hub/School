using ProblemMinimalApi.Services;

namespace ProblemMinimalApi.Tests;

public class Task1 : IProblemTest
{
    private readonly IExecuteService _executeService;

    public Task1()
    {
        _executeService = new ExecuteService();
    }

    public async Task<string> Method1(string userCode)
    {
        try
        {
            var result = await _executeService.ExecuteCode(userCode);

            if (result == 1)
            {
                return "Тест пройден";
            }
            else
            {
                return $"Тест не пройден. Ожидаемое значение: 1, полученное значение: {result}";
            }
        }
        catch (Exception e)
        {
            return $"Ошибка при выполнении теста: {e.Message}";
        }
    }
}