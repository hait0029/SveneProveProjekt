namespace SvenePrøveProjekt.Interfaces
{
    public interface IUserRepo
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int userId);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(int userId, User user);
        public Task<User> DeleteUser(int userId);
    }
}
