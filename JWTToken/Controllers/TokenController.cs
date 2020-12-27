using Microsoft.AspNetCore.Mvc;

namespace JWTToken.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService tokenService;
        
        public TokenController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        
        [HttpGet]
        public ActionResult<string> GetToken()
        {
            return tokenService.CreateToken();
        }
    }
}
