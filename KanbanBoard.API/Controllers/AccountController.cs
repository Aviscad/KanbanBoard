using KanbanBoard.API.Models;
using KanbanBoard.API.Models.Account;
using KanbanBoard.API.Services;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace KanbanBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly SignInManager<User> _singInManager;

        public AccountController(
            UserManager<User> userManager, 
            ITokenService tokenService, 
            SignInManager<User> singInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _singInManager = singInManager;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingUser  = await _userManager.FindByEmailAsync(registerDto.Email);
                if (existingUser != null) return BadRequest("Email is already in use.");

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
                        var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { emailToken, email = user.Email }, Request.Scheme);


                        var relativeTemplatePath = Path.Combine(Directory.GetCurrentDirectory(),"Models", "Email", "ConfirmEmailTemplate.html");

                        var emailTemplate = System.IO.File.ReadAllText(relativeTemplatePath);

                        var emailBody = emailTemplate
                            .Replace("@Model.ConfirmationLink", confirmationLink);

                        var emailModel = new EmailModel
                        {
                            ToEmail = registerDto.Email,
                            Subject = "Confirm your email",
                            Body = emailBody
                        };

                        _emailService.SendEmail(emailModel);

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

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string emailToken, [FromQuery]string email)
        {
            if (string.IsNullOrEmpty(emailToken) || string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid email confirmation request.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Invalid email confirmation request.");
            }
            if (user.EmailConfirmed) return BadRequest("Email is already confirmed.");

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully.");
            }

            return BadRequest("Email confirmation failed.");
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName.ToLower());

            if (
                user == null ||
                !(await _singInManager
                    .CheckPasswordSignInAsync(
                        user,
                        loginDto.Password,
                        false
                    )).Succeeded
                )
            {
                return Unauthorized();
            }

            var token = _tokenService.CreateToken(user);

            //Response.Cookies.Append("jwt", token, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.None,
            //    Expires = DateTime.UtcNow.AddDays(7)
            //});

            return Ok(
                 new NewUserDto
                 {
                     UserName = user.UserName,
                     Email = user.Email,
                     Token = token
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
                return NotFound();
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

            return Ok();
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            try
            {
                //Response.Cookies.Append("jwt", "", new CookieOptions
                //{
                //    HttpOnly = true,
                //    Secure = true,
                //    SameSite = SameSiteMode.None,
                //    Expires = DateTime.UtcNow.AddDays(-1)
                //});

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Validate")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            return Ok(new { message = "Token is valid" });
        }
    }
}
