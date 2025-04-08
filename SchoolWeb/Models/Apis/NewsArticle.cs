using WebStoryFroEveryting.Models.Apis;

namespace WebStoryFroEveryting.Services.Apis;

public class NewsArticle
{
    public NewsSource Source { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string UrlToImage { get; set; } = string.Empty;
    public string PublishedAt {get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

}