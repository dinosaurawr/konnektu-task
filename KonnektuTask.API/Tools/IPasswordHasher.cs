namespace KonnektuTask.API.Tools
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPasswordHash(string hash, string password);
    }
}