
using Microsoft.EntityFrameworkCore;
using SvenePrøveProjekt.Models;

namespace SvenePrøveProjekt.Repositories
{
    public class LoginRepo : ILoginRepo
    {
        private DatabaseContext _context { get; set; }
        public LoginRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Login> CreateLogin(Login newLogin)
        {
            _context.Login.Add(newLogin);
            await _context.SaveChangesAsync();
            return newLogin;
        }


        public async Task<List<Login>> GetLogin()
        {
            return await _context.Login.ToListAsync();
        }

        public async Task<Login> GetLoginById(int loginId)
        {
            return await _context.Login.FirstOrDefaultAsync(x => x.LoginID == loginId);
        }

        public async Task<Login> UpdateLogin(int loginId, Login updatelogin)
        {
            Login login = await GetLoginById(loginId);
            if (login != null && updatelogin != null)
            {
                login.LoginID = updatelogin.LoginID;
                login.Email = updatelogin.Email;
                login.Password = updatelogin.Password;

                await _context.SaveChangesAsync();
            }
            return login;
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
