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

    public List<MethodData> Index()
    {
        var methods = this.GetType().GetMethods(BindingFlags.Public |
                                                BindingFlags.Instance |
                                                BindingFlags.DeclaredOnly);
        var result = methods
            .Select(x => new MethodData()
            {
                ReturnType = x.ReturnType.IsGenericType 
                    ? $"{x.ReturnType.Name}<{string.Join(", ", x.ReturnType.GetGenericArguments().Select(t => t.Name))}>"
                    : x.ReturnType.Name,
                Name = x.Name,
                Params = x.GetParameters()
                    .Select(p => new ParamInfo()
                    {
                        ParamName = p.Name,
                        ParamType = p.ParameterType.Name
                    })
                    .ToList()
            })
            .ToList();
        return result.ToList();
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
        var problem = new ProblemData()
        {
            Id = id,
            Name = name,
            Description = description,
            Theme = theme
        };
        _context.Update(problem);
        _context.SaveChanges();
        return Results.Ok();
    }
}