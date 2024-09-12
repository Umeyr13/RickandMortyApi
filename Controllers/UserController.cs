using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RickandMorty.Models;
using RickMortyApplication.DTOs;
using RickMortyDomain.Entities;
using RickMortyPersistence;

namespace RickandMorty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenHandler _tokenHandler;

        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, TokenHandler tokenHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser userModel) 
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                UserName = userModel.UserName,
                Email = userModel.Email,   
            },userModel.Password);
         
            if (result.Succeeded)
                return Ok("Kullanıcı Oluşturuldu.");
            else
            {
                return BadRequest("Hata oluştu"); ;
 
            }

            
        }

        [HttpPost]
        public async Task<Token> LoginUser(LoginViewModel userlogin)
        {
            AppUser user = await _userManager.FindByNameAsync(userlogin.Username);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, userlogin.Password, false);
            if (result.Succeeded)// Authentication başarılı!
            {
                Token token = _tokenHandler.CreateAccessToken(10,user);
        
                return token;
            }
            return new Token();
        }
    }
}
