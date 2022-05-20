using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Dtos;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AccountController:BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork uow,IConfiguration configuration)
        {
            _uow = uow;
            this.configuration = configuration;
        }

        [HttpPost("login")]  //...api/account/login
        public async Task<IActionResult> Login(LoginDto logDto)
        {
            var user=await _uow.UserRepository.Authenticate(logDto.Username,logDto.Password);
            if(user==null)
            {
                return Unauthorized();
            } 
            
            var loginRes=new LoginResDto();
                loginRes.Name=user.UserName;
                loginRes.Token=CreateJwt(user);

            
            return Ok(loginRes);        
        }

        [HttpPost("register")] ///...api/account/register
        public async Task<IActionResult> RegisterUser(LoginDto dto)
        {
            if(await _uow.UserRepository.UserAlreadyExists(dto.Username))
                return BadRequest("User Already");
            _uow.UserRepository.Register(dto.Username,dto.Password);
            await _uow.SaveAsync();
            return StatusCode(201);    
        }

        private string CreateJwt(User user)
        {
            var secretKey=configuration.GetSection("AppSettings:Key").Value;

            var Key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims=new Claim[]{
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

           var signinCreditails=new SigningCredentials(
               Key,SecurityAlgorithms.HmacSha256
           );
           var tokenDescriptor=new SecurityTokenDescriptor{
               Subject=new ClaimsIdentity(claims),
               Expires=DateTime.UtcNow.AddDays(10),
               SigningCredentials=signinCreditails

           };
           var tokenHandler=new JwtSecurityTokenHandler();
           var token=tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        }

    }
}