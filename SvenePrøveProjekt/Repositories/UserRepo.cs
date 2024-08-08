
namespace SvenePrøveProjekt.Repositories
{
    public class UserRepo : IUserRepo
    {
        private DatabaseContext _context;
        public UserRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User newUser)
        {

            if(newUser.LoginId.HasValue)
            {
                newUser.login = await _context.Login.FirstOrDefaultAsync(e => e.LoginID == newUser.LoginId);
            }

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;

        }


        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(e => e.UserID == userId);
        }

        public async Task<User> UpdateUser(int userId, User updateUser)
        {
            User user = await GetUserById(userId);
            if (user != null && updateUser != null)
            {
                user.UserID = updateUser.UserID;
                user.FName = updateUser.FName;
                user.LName = updateUser.LName;
                user.PhoneNr = updateUser.PhoneNr;
                user.Address = updateUser.Address;

                await _context.SaveChangesAsync();
            }
            return user;
        }
        public async Task<User> DeleteUser(int userId)
        {
            User user = await GetUserById(userId);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }
    }
}
