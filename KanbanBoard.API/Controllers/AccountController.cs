using KanbanBoard.API.Models.Account;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _singInManager;

        public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> singInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _singInManager = singInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = new User
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user)
                            }
                            );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var id = _userManager.GetUserId(User);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("No se pudo ingresar!");

            var result = await _singInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("No se pudo ingresar!");

            return Ok(
                 new NewUserDto
                 {
                     UserName = user.UserName,
                     Email = user.Email,
                     Token = _tokenService.CreateToken(user)
                 }
                );
        }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok(new { Message = "Password changed successfully" });
        }

    }
}
