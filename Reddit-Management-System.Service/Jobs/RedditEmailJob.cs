using Quartz;
using Reddit_Management_System.Application.ServiceInterfaces.Reddit;

namespace Reddit_Management_System.Service.Jobs
{
    public class RedditEmailJob : IJob
    {
        private readonly IRedditQueryService _redditQueryService;

        public RedditEmailJob(IRedditQueryService redditQueryService)
        {
            _redditQueryService = redditQueryService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _redditQueryService.FetchTopRedditPosts();
        }
    }
}
