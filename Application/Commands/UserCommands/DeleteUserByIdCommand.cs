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
    public class DeleteUserByIdCommand : IRequest<ApiResponse<UserResponse>>
    {
        public int UserId {  get; set; }

        public DeleteUserByIdCommand(int userId)
        {
            UserId = userId;    
        }
    }
}
