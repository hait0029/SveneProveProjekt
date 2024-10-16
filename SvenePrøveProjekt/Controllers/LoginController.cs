using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SvenePrøveProjekt.Models;

namespace SvenePrøveProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginRepo _loginrepo;
        public LoginController(ILoginRepo temp)
        {
            _loginrepo = temp;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login logins)
        {
            var token = await _loginrepo.AuthenticateAsync(logins.Email, logins.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Login registerLog)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerLog.Password);
            var login = new Login
            {
                Email = registerLog.Email,
                Password = hashedPassword,
                RoleId = registerLog.RoleId
            };

            await _loginrepo.AddLoginAsync(login);
            return Ok();
        }




        [HttpGet]
        public async Task<ActionResult> GetLogins()
        {
            try
            {
                var logins = await _loginrepo.GetAllLogin();

                if (logins == null)
                {
                    return Problem("Nothing was returned from logins, this is unexpected");
                }
                return Ok(logins);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{loginId}")]
        public async Task<ActionResult> GetLoginssById(int loginId)
        {
            try
            {
                var login = await _loginrepo.GetLoginById(loginId);

                if (login == null)
                {
                    return NotFound($"login with id {loginId} was not found");
                }
                return Ok(login);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Update Method
        [HttpPut("{loginId}")]
        public async Task<ActionResult> PutLogin(int loginId, Login login)
        {
            try
            {
                var loginResult = await _loginrepo.UpdateLogin(loginId, login);

                if (login == null)
                {
                    return NotFound($"login with id {loginId} was not found");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(login);

        }

        //Create Method
        [HttpPost]
        public async Task<ActionResult> PostLogin(Login login)
        {
            try
            {
                var createLogin = await _loginrepo.CreateLogin(login);

                if (createLogin == null)
                {
                    return StatusCode(500, "Login was not created. Something failed...");
                }
                return CreatedAtAction("PostLogin", new { loginId = createLogin.LoginID }, createLogin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the login {ex.Message}");
            }
        }

        //Delete Method
        [HttpDelete("{loginId}")]
        public async Task<ActionResult> DeleteLogin(int loginId)
        {
            try
            {
                var login = await _loginrepo.DeleteLogin(loginId);

                if (login == null)
                {
                    return NotFound($"login with id {loginId} was not found");
                }
                return Ok(login);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }

}
