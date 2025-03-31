using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;
using ProblemMinimalApi.Controllers;
using ProblemMinimalApi.DatabaseAccessLayer;

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
app.MapPost("/addProblem", (ProblemController problemController, string name, string description, string theme) =>
    problemController.AddProblem(name, description, theme));

app.MapGet("/problems", (ProblemController problemController) => problemController.GetProblems());

app.MapPut("/problems/edit", (ProblemController problemController,int id, string name, string description, string theme) =>
    problemController.UpdateProblem(id, name, description, theme));

app.MapDelete("/problems", (ProblemController problemController,int id) => problemController.DeleteProblem(id));

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
