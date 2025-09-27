using Microsoft.AspNetCore.Authorization;

namespace TrainingCenterAPI.Controllers.Admin
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }

        // Register Admin
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            model.Role = "Admin"; // تثبيت الرول
            var result = await _authService.RegisterAsync(model);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        // Login Admin
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.LoginAsync(model, Response);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordDto model)
        {
            var response = await _authService.ChangePasswordAsync(id, model);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var result = await _authService.MeAsync(User);
            return Ok(result);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var result = _authService.Logout(Response);
            return Ok(result);
        }

        // Get All Admins
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _authService.GetAllAdminsAsync();
            return Ok(result);
        }

        // Update Admin
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAdminDto model)
        {
            var result = await _authService.UpdateAdminAsync(id, model);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        // Delete Admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _authService.DeleteUserAsync(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("test-register")]
        public IActionResult TestRegister()
        {
            return Ok("Endpoint reached");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(Guid id)
        {
            var response = await _authService.GetAdminByIdAsync(id);

            if (!response.Success)
            {
                return BadRequest(new { message = response.Message });
            }

            return Ok(response);
        }
    }
}
