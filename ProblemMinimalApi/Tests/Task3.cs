using ProblemMinimalApi.Services;

namespace ProblemMinimalApi.Tests;

public class Task3 : IProblemTest
{
    private readonly IExecuteService _executeService;

    public Task3()
    {
        _executeService = new ExecuteService();
    }

    public async Task<string> Method1(string userCode)
    {
        try
        {
            var result = await _executeService.ExecuteCode(userCode);

            if (result == 3)
            {
                return "Тест пройден";
            }
            else
            {
                return $"Тест не пройден. Ожидаемое значение: 3, полученное значение: {result}";
            }
        }
        catch (Exception e)
        {
            return $"Ошибка при выполнении теста: {e.Message}";
        }
    }
}