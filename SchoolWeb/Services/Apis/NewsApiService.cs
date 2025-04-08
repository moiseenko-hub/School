using System.Text.Json;
using System.Net.Http.Json;
using WebStoryFroEveryting.Models.Apis;
using WebStoryFroEveryting.Services;
using WebStoryFroEveryting.Services.Apis;

public class NewsApiService : INewsApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public NewsApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<NewsViewModel> GetNewsAsync()
    {
        var client = _httpClientFactory.CreateClient("NewsApi");
        client.DefaultRequestHeaders.Add("User-Agent", "WebStoryForEverything/1.0");

        var response = await client.GetAsync($"everything?q=bitcoin&apiKey={NewsApiConstans.TOKEN}");
        response.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        NewsViewModel? content = await response.Content.ReadFromJsonAsync<NewsViewModel>(options);
        return content!;
    }
}