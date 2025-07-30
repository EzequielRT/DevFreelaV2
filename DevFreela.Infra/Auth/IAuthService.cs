namespace DevFreela.Infra.Auth;

public interface IAuthService
{
    Task<string> ComputeHash(string password);
    Task<string> GenerateHash(string email, string role);
}
