using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;
using ProblemMinimalApi.Controllers;
using ProblemMinimalApi.DatabaseAccessLayer;
using ProblemMinimalApi.Dto;
using ProblemMinimalApi.Services;
using ProblemMinimalApi.Tests;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.SetIsOriginAllowed(x => true);
        p.AllowCredentials();
    });
});

builder.Services.AddDbContext<ProblemDbContext>( o => o.UseSqlServer(
        ProblemDbContext.CONNECTION_STRING
    ));

builder.Services.AddScoped<ProblemController>();
builder.Services.AddScoped<IExecuteService, ExecuteService>();

var app = builder.Build();
app.UseCors();


app.MapGet("/api-docs", (EndpointDataSource dataSource) =>
{
    var endpoints = dataSource.Endpoints
        .OfType<RouteEndpoint>()
        .Select(endpoint =>
        {
            var methodInfo = endpoint.Metadata.OfType<MethodInfo>().FirstOrDefault();
            var parameters = methodInfo?.GetParameters()
                .Select(p => new { Name = p.Name, Type = p.ParameterType.Name })
                .ToList();

            return new
            {
                Route = endpoint.RoutePattern.RawText,
                Methods = endpoint.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods,
                Parameters = parameters,
                ReturnType = methodInfo?.ReturnType.Name
            };
        });

    return Results.Json(endpoints);
});

app.MapGet("/", () => "Hello");
// Почему не работает с Post в браузере?
app.MapPost("/addProblem", (ProblemController problemController, string name, string description, string theme, string testName) =>
    problemController.AddProblem(name, description, theme, testName));

app.MapGet("/problems", (ProblemController problemController) => problemController.GetProblems());

app.MapPut("/problems/edit", (ProblemController problemController,int id, string name, string description, string theme) =>
    problemController.UpdateProblem(id, name, description, theme));

app.MapDelete("/problems", (ProblemController problemController,int id) => problemController.DeleteProblem(id));

app.MapPost("/answer", async (ProblemController problemController, [FromBody] AnswerDto dto) =>
{
    var test = problemController.GetProblemTest(dto);
    var parameters = new object[] { dto.Content };
    var methods = test.GetType().GetMethods()
        .Where(m => !m.IsSpecialName && !m.IsStatic && m.DeclaringType == test.GetType());

    var resultBuilder = new StringBuilder();

    foreach (var method in methods)
    {
        var methodParameters = method.GetParameters();

        if (methodParameters.Length == 1 && methodParameters[0].ParameterType == typeof(string))
        {
            if (method.ReturnType == typeof(Task<string>))
            {
                var result = await (Task<string>)method.Invoke(test, parameters);
                resultBuilder.AppendLine(result);
            }
            else
            {
                var result = method.Invoke(test, parameters);
                resultBuilder.AppendLine(result?.ToString() ?? "null");
            }
        }
        else
        {
            resultBuilder.AppendLine($"Метод {method.Name} не поддерживается");
        }
    }

    return Results.Ok(resultBuilder.ToString());
});


app.UseSwagger();
app.UseSwaggerUI();

app.Run();
