using WebStoryFroEveryting.Models.Apis;

namespace WebStoryFroEveryting.Services;

public interface INewsApiService
{
    public Task<NewsViewModel> GetNewsAsync();
}