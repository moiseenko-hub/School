using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using ProblemMinimalApi.DatabaseAccessLayer;

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

    public IResult AddProblem(string name, string description, string theme)
    {
        var problem = new ProblemData()
        {
            Name = name,
            Description = description,
            Theme = theme
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