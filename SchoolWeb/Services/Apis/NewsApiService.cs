using System.Text.Json;
using System.Net.Http.Json;
using WebStoryFroEveryting.Models.Apis;
using WebStoryFroEveryting.Services;
using WebStoryFroEveryting.Services.Apis;

public class NewsApiService : INewsApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;

    public NewsApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient("NewsApi");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "WebStoryForEverything/1.0");
    }

    public async Task<NewsViewModel> GetNewsAsync(string q = NewsApiConstans.DEFAULT_Q, int size = NewsApiConstans.DEFAULT_PAGE_SIZE)
    {
        var response = await _httpClient.GetAsync($"everything?q=\"{q}\"&pageSize={size}&language=en&apiKey={NewsApiConstans.TOKEN}");
        response.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        NewsViewModel? content = await response.Content.ReadFromJsonAsync<NewsViewModel>(options);
        return content!;
    }
}