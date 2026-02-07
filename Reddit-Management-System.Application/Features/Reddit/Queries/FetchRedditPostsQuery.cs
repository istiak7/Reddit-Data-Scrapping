using Reddit_Management_System.Application.Common.MediatR;
using Reddit_Management_System.Application.Features.Email.Command.Dtos;
using Reddit_Management_System.Application.ServiceInterfaces.Reddit;

namespace Reddit_Management_System.Application.Features.Reddit.Queries
{
    public sealed record FetchRedditPostsQuery : IQuery<List<SrapResponseDto>>;

    public class FetchRedditPostsQueryHandler : IQueryHandler<FetchRedditPostsQuery, List<SrapResponseDto>>
    {
        private readonly IRedditQueryService _redditQueryService;

        public FetchRedditPostsQueryHandler(IRedditQueryService redditQueryService)
        {
            _redditQueryService = redditQueryService;
        }

        public async Task<List<SrapResponseDto>> Handle(FetchRedditPostsQuery query, CancellationToken cancellationToken)
        {
            return await _redditQueryService.FetchTopRedditPosts();
        }
    }
}
