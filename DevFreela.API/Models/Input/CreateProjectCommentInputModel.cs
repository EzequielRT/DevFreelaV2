using DevFreela.API.Entities;

namespace DevFreela.API.Models.Input
{
    public class CreateProjectCommentInputModel
    {
        public string Content { get; set; }
        public int ProjectId { get; private set; }
        public int UserId { get; set; }

        public void SetProjectId(int id) => ProjectId = id;

        public ProjectComment ToEntity()
            => new(Content, ProjectId, UserId);
    }
}
