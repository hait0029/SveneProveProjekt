namespace SvenePrøveProjekt.Interfaces
{
    public interface ILoginRepo
    {
        public Task<List<Login>> GetAllLogin();
        public Task<Login> GetLoginById(int loginId);
        public Task<Login> CreateLogin(Login login);
        public Task<Login> UpdateLogin(int loginId, Login login);
        public Task<Login> DeleteLogin(int loginId);

        public Task<Login?> GetLoginByEmailAsync(string email);
        public Task<string?> AuthenticateAsync(string email, string password);
        public Task AddLoginAsync(Login login);
        public Task UpdateLoginAsync(Login login);
    }
}