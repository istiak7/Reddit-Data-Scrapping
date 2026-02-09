using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Reddit_Management_System.Application.ServiceInterfaces.Reddit;

namespace Reddit_Management_System.Service.Jobs
{
    public class RedditEmailJob : IJob
    {
        private readonly IRedditQueryService _redditQueryService;
        private readonly IServiceScopeFactory _scopeFactory;
        public RedditEmailJob(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("=== RedditEmailJob STARTED ===");
            using (var scope = _scopeFactory.CreateScope())
            {
                var redditQueryService = scope.ServiceProvider.GetRequiredService<IRedditQueryService>();
                await redditQueryService.FetchTopRedditPosts();
            } 
            Console.WriteLine("=== RedditEmailJob Finished ===");
        }
    }
}
