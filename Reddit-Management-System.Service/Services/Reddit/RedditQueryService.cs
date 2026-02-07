using Reddit_Management_System.Application.Features.Email.Command.Dtos;
using Reddit_Management_System.Application.RepositoryInterfaces.Reddit;
using Reddit_Management_System.Application.ServiceInterfaces.Email;
using Reddit_Management_System.Application.ServiceInterfaces.Reddit;

namespace Reddit_Management_System.Service.Services.Reddit
{
    public class RedditQueryService : IRedditQueryService
    {
        private readonly IRedditRepository _redditRepository;
        private readonly IEmailCommandService _emailCommandService;
        public RedditQueryService(IRedditRepository redditRepository, IEmailCommandService emailCommandService)
        {
            _emailCommandService = emailCommandService;
            _redditRepository = redditRepository;
        }

        public async Task<List<SrapResponseDto>> FetchTopRedditPosts()
        {
            var allPosts = await _redditRepository.FetchRedditPostsFromApi();
            var topPosts = allPosts.OrderByDescending(p => p.Upvotes).Take(5).ToList();

            List<string> emails = new()
            {
                "20101112@uap-bd.edu",
                //"ab.asma1084@gmail.com"
            };
            foreach (var email in emails)
            {
                await _emailCommandService.SendOtpEmailAsync(email, topPosts);
            }
            return topPosts;
        }
    }
}
