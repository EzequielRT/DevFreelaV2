namespace DevFreela.Infra.Auth;

public interface IAuthService
{
    Task<string> ComputeHashAsync(string password);
    Task<string> GenerateTokenAsync(string email, string role);
}
