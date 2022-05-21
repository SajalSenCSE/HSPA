using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Dtos;
using WebApi.Errors;
using WebApi.Extensions;
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
            ApiError apiError=new ApiError();

            if(user==null)
            {
                apiError.ErrorCode=Unauthorized().StatusCode;
                apiError.Messahe="User Id and Password invalid";
                apiError.ErrorDetails="hi sajal  this is error details";
                return Unauthorized(apiError);
            } 
            
            var loginRes=new LoginResDto();
                loginRes.Name=user.UserName;
                loginRes.Token=CreateJwt(user);

            
            return Ok(loginRes);        
        }

        [HttpPost("register")] ///...api/account/register
        public async Task<IActionResult> RegisterUser(LoginDto dto)
        {
            ApiError apiError=new ApiError();
            if(dto.Username.IsEmpty() || dto.Password.IsEmpty())
            {
                apiError.ErrorCode=BadRequest().StatusCode;
                apiError.Messahe="User name and Password can not be Empty";
                return BadRequest(apiError.Messahe);
            }

            if(await _uow.UserRepository.UserAlreadyExists(dto.Username))
            {
                apiError.ErrorCode=BadRequest().StatusCode;
                apiError.Messahe="User Already";
                return BadRequest(apiError.Messahe);
            }
               
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