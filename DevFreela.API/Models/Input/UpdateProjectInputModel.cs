namespace DevFreela.API.Models.Input
{
    public class UpdateProjectInputModel
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
    }
}
