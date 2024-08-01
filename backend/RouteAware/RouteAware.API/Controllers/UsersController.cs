using Microsoft.AspNetCore.Mvc;
using RouteAware.API.Contracts;
using RouteAware.Application.Services;

namespace RouteAware.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsersController(IUsersService usersService, IHttpContextAccessor httpContextAccessor)
        {
            this.usersService = usersService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("register")]
        public async Task<IResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var userId = await usersService.RegisterUser(request.UserName, request.Password, request.Email);

            return Results.Ok(new UsersResponse(userId, request.UserName, request.Email));
        }

        [HttpPost("login")]
        public async Task<IResult> LoginUser([FromBody] LoginUserRequest request)
        {
            var context = httpContextAccessor.HttpContext;
            var (token, user) = await usersService.LoginUser(request.Email, request.Password);

            context!.Response.Cookies.Append("tasty-cookies", token);

            return Results.Ok(new UsersResponse(user.Id, user.UserName, user.Email));
        }
    }
}