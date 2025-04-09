using WebStoryFroEveryting.Services.Apis;

namespace WebStoryFroEveryting.Models.Apis;

public class NewsViewModel
{
    public string Status { get; set; } = string.Empty;
    public int TotalResults { get; set; }
    public List<NewsArticle> Articles { get; set; }

}