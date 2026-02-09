using Reddit_Management_System.Application.Dtos.Responses.AI;
using Reddit_Management_System.Application.Features.Email.Command.Dtos;
using Reddit_Management_System.Application.RepositoryInterfaces.Reddit;
using Reddit_Management_System.Application.ServiceInterfaces.AI;
using Reddit_Management_System.Application.ServiceInterfaces.Email;
using Reddit_Management_System.Application.ServiceInterfaces.Reddit;

namespace Reddit_Management_System.Service.Services.Reddit
{
    public class RedditQueryService : IRedditQueryService
    {
        private readonly IRedditRepository _redditRepository;
        private readonly IEmailCommandService _emailCommandService;
        private readonly IGeminiComamndService _geminiComamndService;
        public RedditQueryService(IRedditRepository redditRepository, 
                                  IEmailCommandService emailCommandService, 
                                  IGeminiComamndService geminiComamndService )
        {
            _emailCommandService = emailCommandService;
            _geminiComamndService = geminiComamndService;
            _redditRepository = redditRepository;
        }

        public async Task<List<SrapResponseDto>> FetchTopRedditPosts()
        {
            var allPosts = await _redditRepository.FetchRedditPostsFromApi();
            var topPosts = allPosts.OrderByDescending(p => p.Upvotes).Take(5).ToList();

            var postsForAI = topPosts.Select(p => (p.Title, p.Description, p.Upvotes)).ToList();
            var aiResponses = await _geminiComamndService.RearrangeBatchPostsAsync(postsForAI);
            List<SrapResponseDto> ScrapResponse = [];
            if (aiResponses != null && aiResponses.Count() > 0)
            {
                ScrapResponse = aiResponses.Select(aiResponseDto => new SrapResponseDto
                {
                    Title = aiResponseDto.Title,
                    Description = aiResponseDto.Description,
                    Upvotes = aiResponseDto.UpVotes
                }).ToList();
            }
            else
            {
                ScrapResponse = topPosts.Select(p => new SrapResponseDto
                {
                    Title = p.Title,
                    Description = p.Description,
                    Upvotes = p.Upvotes
                }).ToList();
            }
            if (ScrapResponse.Any())
            {
                List<string> emails = await _redditRepository.GetSubscribersEmails();
                foreach (var email in emails)
                {
                    await _emailCommandService.SendOtpEmailAsync(email, ScrapResponse);
                }
            }
            return ScrapResponse;
        }
    }
}
