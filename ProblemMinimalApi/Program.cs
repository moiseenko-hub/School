using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();
app.UseCors();

app.MapGet("/", () => "Hello World!");

app.MapPost("/addProblem", (ProblemDbContext dbContext,string name, string description,string theme) => {
    var problem = new ProblemData()
    {
        Name = name,
        Description = description,
        Theme = theme
    };
    dbContext.Add(problem);
    dbContext.SaveChanges();
    return Results.Ok(problem);
});

app.MapGet("/problems", (ProblemDbContext dbContext) =>
{
    var problems = dbContext.Problems.ToList();
    return problems;
});

app.MapPut("/problems/edit", (ProblemDbContext dbContext,int id, string name, string description, string theme) =>
{
    var problem = new ProblemData()
    {
        Id = id,
        Name = name,
        Description = description,
        Theme = theme
    };
    dbContext.Update(problem);
    dbContext.SaveChanges();
});

app.MapDelete("/problems", (ProblemDbContext dbContext,int id) =>
{
    var problem = dbContext.Problems.SingleOrDefault(p => p.Id == id);
    dbContext.Problems.Remove(problem);
    dbContext.SaveChanges();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
