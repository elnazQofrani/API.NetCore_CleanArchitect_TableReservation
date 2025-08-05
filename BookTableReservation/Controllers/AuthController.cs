using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace BookTableReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly ITokenHandler tokenHandler;

        public AuthController(ICustomerService customerService , ITokenHandler tokenHandler)
        {

            this.customerService = customerService;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string passeord)
        {

          var user =  await  customerService.GetCustomerByNamePassword(username ,passeord);

            if (user != null)
            {
                //generate jwt
            var token =    tokenHandler.CreateToken(user);
                return Ok(token);

            }
            return BadRequest("username and password is incorect");

        }
    }
}
