using Reddit_Management_System.Application.Features.Email.Command.Dtos;

namespace Reddit_Management_System.Application.ServiceInterfaces.Reddit
{
    public interface IRedditQueryService
    {
        Task<List<SrapResponseDto>> FetchTopRedditPosts();
    }
}
