using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using Application.Utility;
using Domain.DTOs.UserDTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator mediator;

        public UserController(IMediator _mediator)
        {
            mediator = _mediator;
        }


        [HttpPost("AddUser")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> AddUser([FromBody] UserRequest userRequest)
        {
            var user = await mediator.Send(new AddUserCommand(userRequest));

            return Ok(user);
        }


        [HttpPut("UpdateUser")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> UpdateUser([FromBody] UserRequest userRequest)
        {
            var user = await mediator.Send(new UpdateUserCommand(userRequest));

            return Ok(user);
        }



        [HttpGet("GetUserById/{userId}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> GetUserById(int userId)
        {
            var user = await mediator.Send(new GetUserByUserIdQuery(userId));

            return Ok(user);
        }



        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> GetAllUsers()
        {
            var user = await mediator.Send(new GetAllUsersQuery());

            return Ok(user);
        }




        [HttpDelete("DeleteUserById/{userId}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> DeleteUserById(int userId)
        {
            var user = await mediator.Send(new DeleteUserByIdCommand(userId));

            return Ok(user);
        }

    }
}
