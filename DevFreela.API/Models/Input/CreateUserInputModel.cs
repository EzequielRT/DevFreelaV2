namespace DevFreela.API.Models.Input
{
    public class CreateUserInputModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
