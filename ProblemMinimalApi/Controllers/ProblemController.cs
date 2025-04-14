using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using ProblemMinimalApi.DatabaseAccessLayer;
using ProblemMinimalApi.Dto;
using ProblemMinimalApi.Tests;

namespace ProblemMinimalApi.Controllers;

public class ProblemController
{
    private readonly ProblemDbContext _context;

    public ProblemController(ProblemDbContext context)
    {
        _context = context;
    }

    public List<ProblemData> GetProblems()
    {
        return _context.Problems.ToList();
    }

    public IProblemTest GetProblemTest(AnswerDto dto)
    {
        var problem = _context
            .Problems
            .Where(p => p.Id == dto.Id)!
            .FirstOrDefault();
        
        var type = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => 
                t.Name == problem.TestName && 
                typeof(IProblemTest).IsAssignableFrom(t) &&
                !t.IsAbstract);
        return (IProblemTest)Activator.CreateInstance(type);
    }

    public IResult AddProblem(string name, string description, string theme, string testName)
    {
        var problem = new ProblemData()
        {
            Name = name,
            Description = description,
            Theme = theme,
            TestName = testName
        };
        
        _context.Problems.Add(problem);
        _context.SaveChanges();
        return Results.Ok();
    }

    public IResult DeleteProblem(int id)
    {
        var problem = _context.Problems
            .FirstOrDefault(p => p.Id == id);
        if (problem != null) _context.Problems.Remove(problem);
        _context.SaveChanges();
        return Results.Ok();
    }

    public IResult UpdateProblem(int id, string name, string description, string theme)
    {
        var problem = _context.Problems.Find(id);
    
        if (problem == null)
            return Results.NotFound();

        problem.Name = name;
        problem.Description = description;
        problem.Theme = theme;

        _context.SaveChanges();
        return Results.Ok();
    }

}