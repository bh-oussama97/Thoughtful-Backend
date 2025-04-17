using Microsoft.AspNetCore.Mvc;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.Commands;
using Thoughtful.Api.Features.Authentication.DTO;
using Thoughtful.Dal;

namespace Thoughtful.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected IMediator _mediator;
        protected ThoughtfulDbContext _context;
        public UsersController(IMediator mediator, ThoughtfulDbContext context)
        {
            this._mediator = mediator;
            this._context = context;
        }
        [HttpPost]
        public async Task<ActionResult<Result<UserGetDTO>>> Login(LoginRequestDTO loginDTO)
        {
            var result = await this._mediator.Send(new LoginUser { LoginRequest = loginDTO });
            if (result == null)
            {
                return BadRequest("Error");
            }
            return Ok(result);
        }

        [HttpPost]

        public async Task<ActionResult<Result<UserGetDTO>>> Register(RegisterDTO dto)
        {

            var result = await _mediator.Send(new RegisterUser { RegisterRequest = dto });
            if (result == null)
            {
                return BadRequest("Error");
            }
            return Ok(result);
        }
    }
}
