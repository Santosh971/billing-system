using Application.Utility;
using Domain.DTOs.UserDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<ApiResponse<UserResponse>>
    {
        public UserRequest UserRequest {  get; set; }   

        public UpdateUserCommand(UserRequest userRequest)
        {
            UserRequest = userRequest;  
            
        }
    }
}
