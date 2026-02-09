using Reddit_Management_System.Application.Dtos.Responses.AI;

namespace Reddit_Management_System.Application.ServiceInterfaces.AI;

public interface IGeminiComamndService
{
    //Task<AIResponse> RearrangePostAsync(string title, string postContent, int upVote);
    Task<List<AIResponse>> RearrangeBatchPostsAsync(List<(string Title, string Description, int Upvotes)> posts);
}