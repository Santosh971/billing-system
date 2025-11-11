using Application.Commands.UserCommands;
using Application.Utility;
using Domain.DTOs.UserDTOs;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserHandler
{
    public class UserUpdateCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UserResponse>>
    {
        private readonly AppDbContext context;
        public UserUpdateCommandHandler(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResponse<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {

                List<UserResponse> lstUserResponses = new List<UserResponse>();
                var user = await context.Users.Where(u => u.UserId == request.UserRequest.UserId).FirstOrDefaultAsync();

                if(user == null)
                {
                    return new ApiResponse<UserResponse>(null, System.Net.HttpStatusCode.NotFound, "User Not Found", 1);
                }


                user.UserName = request?.UserRequest?.UserName ?? user.UserName;
                user.Email = request?.UserRequest?.Email ?? user.Email;
                user.Address = request?.UserRequest?.Address ?? user.Address;   
                user.PhoneNo = request?.UserRequest?.PhoneNo ?? user.PhoneNo;   

                await context.SaveChangesAsync();   

                var userResponse = Mappers.UserMapper.MapToUserResponse(user);
                lstUserResponses.Add(userResponse);

                return new ApiResponse<UserResponse>(lstUserResponses, System.Net.HttpStatusCode.OK, "User Update Successfully", 0);

            }  
            catch(Exception ex)
            {
                throw;
            } 
        }
    }
}
