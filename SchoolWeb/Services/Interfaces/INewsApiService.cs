using WebStoryFroEveryting.Models.Apis;
using WebStoryFroEveryting.Services.Apis;

namespace WebStoryFroEveryting.Services;

public interface INewsApiService
{
    public Task<NewsViewModel> GetNewsAsync(string q = NewsApiConstans.DEFAULT_Q, int size = NewsApiConstans.DEFAULT_PAGE_SIZE);
}