using Reddit_Management_System.Application.Features.Email.Command.Dtos;
using Reddit_Management_System.Application.RepositoryInterfaces.Reddit;
using System.Text.Json;

namespace Reddit_Management_System.Repo.Repositories.Reddit
{
    public class RedditRepository : IRedditRepository
    {
        public async Task<List<SrapResponseDto>> FetchRedditPostsFromApi()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "PostAutomationBot/1.0");

            var allPosts = new List<SrapResponseDto>();
            var last24Hours = DateTimeOffset.UtcNow.AddHours(-24).ToUnixTimeSeconds();

            string[] urls = {
                "https://www.reddit.com/r/microsaas/.json?limit=50",
                "https://www.reddit.com/r/SideProject/.json?limit=50",
                "https://www.reddit.com/r/SaaS/.json?limit=50",
                "https://www.reddit.com/r/Entrepreneur/.json?limit=50"
            };

            foreach (var url in urls)
            {
                var response = await client.GetStringAsync(url);
                using var doc = JsonDocument.Parse(response);
                var posts = doc.RootElement.GetProperty("data").GetProperty("children");

                foreach (var post in posts.EnumerateArray())
                {
                    var data = post.GetProperty("data");
                    var createdUtc = data.GetProperty("created_utc").GetDouble();

                    if (createdUtc >= last24Hours)
                    {
                        allPosts.Add(new SrapResponseDto
                        {
                            Title = data.GetProperty("title").GetString(),
                            Description = data.GetProperty("selftext").GetString(),
                            Upvotes = data.GetProperty("ups").GetInt32()
                        });
                    }
                }
            }

            return allPosts;
        }
    }
}
