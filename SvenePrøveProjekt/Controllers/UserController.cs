using Microsoft.EntityFrameworkCore;

namespace SvenePrøveProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepo _userRepo;
        private readonly DatabaseContext _context;

        public UserController(IUserRepo temp, DatabaseContext context)
        {
            _userRepo = temp;
            _context = context;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User logins)
        {
            var token = await _userRepo.AuthenticateAsync(logins.Email, logins.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User registerLog)
        {
            try
            {
                // Check if email already exists
                var existingUser = await _context.User.FirstOrDefaultAsync(l => l.Email == registerLog.Email);
                if (existingUser != null)
                {
                    return BadRequest("User already exists");
                }

            

                // Hash Password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerLog.Password);

                // Create new user object using only CityId
                var newUser = new User
                {
                    Email = registerLog.Email,
                    Password = hashedPassword,
                    FName = registerLog.FName,
                    LName = registerLog.LName,
                    PhoneNr = registerLog.PhoneNr,
                    Address = registerLog.Address,

                };

                // Create user in the repository
                var createdUser = await _userRepo.CreateUser(newUser);

                if (createdUser == null)
                {
                    return StatusCode(500, "User was not created. Something failed...");
                }

                return CreatedAtAction("Register", new { userId = createdUser.UserID }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the User: {ex.Message}");
            }
        }









        [HttpGet]
        public async Task<ActionResult> getUsers()
        {
            try
            {
                var users = await _userRepo.GetAllUsers();
                if (users == null)
                {
                    return Problem("Nothing was returned from users, this is unexpected");
                }
                return Ok(users);
            }

            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserById(int userId)
        {
            try
            {
                var users = await _userRepo.GetUserById(userId);
                if (users == null)
                {
                    return NotFound($"User with userid {userId} was not found");
                }
                return Ok(users);
            }

            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> PutUser(int userId, User user)
        {
            try
            {
                var userResult = await _userRepo.UpdateUser(userId, user);

                if (userResult == null)
                {
                    return NotFound($"User with id {userId} was not found");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(user);

        }

        [HttpPost]
        public async Task<ActionResult> PostUser(User user)
        {
            try
            {
                var createUser = await _userRepo.CreateUser(user);
                if (createUser == null)
                {
                    return StatusCode(500, "User was not created. Something failed...");
                }
                return CreatedAtAction("PostUser", new { userId = createUser.UserID }, createUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the User {ex.Message}");
            }
        }

        [HttpDelete("{userId}")]

        public async Task<ActionResult> DeleteUser(int userId) 
        {
            try
            {
                var user = await _userRepo.DeleteUser(userId);

                if (user == null)
                {
                    return NotFound($"User with id {userId} was not found");
                }
                return Ok(user);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message );    
            }

        }
        

    }   
}