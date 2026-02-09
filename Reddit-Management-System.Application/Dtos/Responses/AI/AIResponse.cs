namespace Reddit_Management_System.Application.Dtos.Responses.AI;

public class AIResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int UpVotes { get; set; } 
}

public class GeminiResponse
{
    public List<Candidate> Candidates { get; set; }
}

public class Candidate
{
    public Content Content { get; set; }
}

public class Content
{
    public List<Part> Parts { get; set; }
}

public class Part
{
    public string Text { get; set; }
}