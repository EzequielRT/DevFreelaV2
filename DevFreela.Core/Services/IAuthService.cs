namespace DevFreela.Core.Services;

public interface IAuthService
{
    Task<string> ComputeHashAsync(string password);
    Task<string> GenerateTokenAsync(string email, string role);
}
