using Reddit_Management_System.Application.Features.Email.Command.Dtos;

namespace Reddit_Management_System.Application.RepositoryInterfaces.Reddit
{
    public interface IRedditRepository
    {
        Task<List<SrapResponseDto>> FetchRedditPostsFromApi();
    }
}
