namespace DevFreela.API.Models.Input
{
    public class UpdateProjectInputModel
    {
        public long ProjectId { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }

        public void SetProjectId(long id) => ProjectId = id;
    }
}
