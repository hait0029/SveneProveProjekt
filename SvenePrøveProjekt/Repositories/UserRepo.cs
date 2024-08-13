
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
            //This method checks if the our loginid property has a value, it also allows our value to be nullable
            if (newUser.LoginId.HasValue)
            {
                // This line asynchronously queries the Login table in the database to find the first record where the LoginID matches newUser.LoginId. If a match is found, it assigns the result to newUser.login.

                //FirstOrDefaultAsync returns the first element that satisfies the condition specified by the lambda expression or null if no such element is found.
                newUser.login = await _context.Login.FirstOrDefaultAsync(e => e.LoginID == newUser.LoginId);
            }
            // Check if CityId has a value
            else if (newUser.CityId.HasValue)
            {
                // Asynchronously fetch the City entity associated with the CityId
                newUser.cities = await _context.City.FirstOrDefaultAsync(c => c.CityID == newUser.CityId);

            }
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;

        }


        public async Task<List<User>> GetAllUsers()
        {
            //return await _context.User.ToListAsync();

            return await _context.User.Include(e => e.login).Include(x => x.cities).ToListAsync();


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
            }

            if (updateUser.login != null)
            {
                user.login = await _context.Login.FirstOrDefaultAsync(e => e.LoginID == updateUser.login.LoginID);
            }

            else 
            {
                user.login = null;
            }

            if (updateUser.cities != null) 
            {
                user.cities = await _context.City.FirstOrDefaultAsync(e => e.CityID == updateUser.cities.CityID);

            }
            else
            {
                user.cities = null;
            }

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return await GetUserById(userId);

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
