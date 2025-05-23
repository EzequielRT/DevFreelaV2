namespace DevFreela.Core.Entities;

public class ProjectComment : BaseEntity
{
    // EF Constructor
    protected ProjectComment() { }

    public ProjectComment(string content, long projectId, long userId)
    {
        Content = content;
        ProjectId = projectId;
        UserId = userId;
    }

    public string Content { get; private set; }
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }
    public long UserId { get; private set; }
    public User User { get; private set; }
}
