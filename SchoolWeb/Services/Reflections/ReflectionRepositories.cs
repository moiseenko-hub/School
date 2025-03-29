using System.Reflection;
using StoreData.Repostiroties;

namespace WebStoryFroEveryting.Reflections;

public class ReflectionRepositories
{
    public void AddReflectionRepositories(IServiceCollection serviceCollection)
    {
        var baseRepository = typeof(BaseSchoolRepository<>);
        var assemblyStoreData = Assembly.GetAssembly(baseRepository);

        var repositoryTypes = assemblyStoreData
            .GetTypes()
            .Where(t => 
                t.IsClass &&
                t.BaseType != null &&
                t.BaseType.IsGenericType &&
                t.BaseType.GetGenericTypeDefinition() == baseRepository);

        foreach (var item in repositoryTypes)
        {
            serviceCollection.AddScoped(item);  
        }
    }
}