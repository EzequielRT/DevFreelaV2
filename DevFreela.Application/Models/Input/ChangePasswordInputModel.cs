namespace DevFreela.Application.Models.Input;

public record ChangePasswordInputModel(string Email, string Code, string NewPassword);