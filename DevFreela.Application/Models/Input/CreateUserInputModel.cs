using DevFreela.Core.Entities;

namespace DevFreela.Application.Models.Input;

public class CreateUserInputModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public User ToEntity()
        => new(FullName, Email, BirthDate, Password, Role);
}
