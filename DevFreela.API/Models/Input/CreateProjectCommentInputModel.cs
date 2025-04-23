namespace DevFreela.API.Models.Input
{
    public class CreateProjectCommentInputModel
    {
        public string Content { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
