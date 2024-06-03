namespace SvenePrøveProjekt.Interfaces
{
    public interface ILoginRepo
    {
        public Task<List<Login>> GetLogin();
        public Task<Login> GetLoginById(int loginId);
        public Task<Login> CreateLogin(Login login);
        public Task<Login> UpdateLogin(int loginId, Login login);
        public Task<Login> DeleteLogin(int loginId);
    }
}
