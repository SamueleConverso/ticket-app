using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PROGETTO_U5_S3_L5.DTOs.Account;
using System.Text;
using PROGETTO_U5_S3_L5.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PROGETTO_U5_S3_L5.Models;
using PROGETTO_U5_S3_L5.Services;

namespace PROGETTO_U5_S3_L5.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly Jwt _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly LoggerService _loggerService;

        public AccountController(IOptions<Jwt> jwtOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, LoggerService loggerService) {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _loggerService = loggerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto) {
            var user = new ApplicationUser {
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
            if (!result.Succeeded) {
                var logErrorMessage = "Errore nella registrazione dell'utente";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            var userForRole = await _userManager.FindByEmailAsync(user.Email);

            await _userManager.AddToRoleAsync(userForRole, "Utente");

            var logInfoMessage = "Registrazione avvenuta con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new {
                message = logInfoMessage
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto) {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user == null) {
                var logErrorMessage = "Qualcosa è andato storto.";
                _loggerService.LogError(logErrorMessage);
                return BadRequest(new {
                    message = logErrorMessage
                });
            }

            await _signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, false);

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes);

            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, expires: expiry, signingCredentials: creds);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var logInfoMessage = "Login avvenuto con successo";
            _loggerService.LogInformation(logInfoMessage);
            return Ok(new TokenResponse() {
                Token = tokenString,
                Expires = expiry
            });
        }
    }
}