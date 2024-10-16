
namespace SvenePrøveProjekt.Repositories
{
    public class LoginRepo : ILoginRepo
    {
        private DatabaseContext _context { get; set; }
        private IConfiguration _configuration; // For accessing JWT settings

        public LoginRepo(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Login?> GetLoginByEmailAsync(string email)
        {
            return await _context.Login.Include(l => l.RoleType)
                                        .FirstOrDefaultAsync(l => l.Email == email);
        }

        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            var user = await GetLoginByEmailAsync(email);

            if (user == null || !VerifyPassword(password, user.Password))
                return null;

            return GenerateJwtToken(user);
        }

        // Method to add a new login
        public async Task AddLoginAsync(Login login)
        {
            await _context.Login.AddAsync(login);
            await _context.SaveChangesAsync();
        }


        // Method to update an existing login
        public async Task UpdateLoginAsync(Login login)
        {
            _context.Login.Update(login);
            await _context.SaveChangesAsync();
        }


        // Private method to generate the JWT token
        private string GenerateJwtToken(Login user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.RoleType?.RoleType ?? "User")
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Private method to verify the password hash
        private bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }





        public async Task<Login> CreateLogin(Login newLogin)
        {
            var existingLogin = await _context.Login.FirstOrDefaultAsync(e => e.Email == newLogin.Email);

            if (existingLogin != null)
            {
                throw new ArgumentException("Login already exists", nameof(newLogin.Email));
            }

            if (newLogin.RoleId.HasValue)
            {
                newLogin.RoleType = await _context.Role.FirstOrDefaultAsync(e => e.RoleID == newLogin.RoleId);
            }

            _context.Login.Add(newLogin);
            await _context.SaveChangesAsync();

            return newLogin;
        }


        public async Task<List<Login>> GetAllLogin()
        {
            //return await _context.Login.ToListAsync();
            return await _context.Login.Include(e => e.RoleType).ToListAsync();
        }

        public async Task<Login> GetLoginById(int loginId)
        {
            //return await _context.Login.FirstOrDefaultAsync(x => x.LoginID == loginId);
            return await _context.Login.Include(e => e.RoleType).FirstOrDefaultAsync(x => x.LoginID == loginId);
        }

        public async Task<Login> UpdateLogin(int loginId, Login updatelogin)
        {
            Login login = await GetLoginById(loginId);

            if (login != null)
            {
                login.Email = updatelogin.Email;
                login.Password = !string.IsNullOrEmpty(updatelogin.Password) ? updatelogin.Password : login.Password;

                if (updatelogin.RoleType != null)
                {
                    login.RoleType = await _context.Role.FirstOrDefaultAsync(e => e.RoleID == updatelogin.RoleType.RoleID);
                }
                else
                {
                    login.RoleType = null; // Clear the UserType if null is provided
                }

                _context.Entry(login).State = EntityState.Modified;


                await _context.SaveChangesAsync();
                return await GetLoginById(loginId);
            }
            return null;
        }
        public async Task<Login> DeleteLogin(int loginId)
        {
            Login login = await GetLoginById(loginId);
            if (login != null)
            {
                _context.Login.Remove(login);
                await _context.SaveChangesAsync();
            }
            return login;
        }


    }
}
