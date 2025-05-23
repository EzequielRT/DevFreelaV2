using DevFreela.Core.Entities;

namespace DevFreela.Application.Models.Input;

public class CreateProjectCommentInputModel
{
    public string Content { get; set; }
    public long ProjectId { get; private set; }
    public long UserId { get; set; }

    public void SetProjectId(long id) => ProjectId = id;

    public ProjectComment ToEntity()
        => new(Content, ProjectId, UserId);
}
