using Application.Commands.UserCommands;
using Application.Utility;
using Domain.DTOs.UserDTOs;
using Domain.Models;
using Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserHandler
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ApiResponse<UserResponse>>
    {
        private readonly AppDbContext context;
        public AddUserCommandHandler(AppDbContext _context)
        {
             context = _context; 
        }
        public async Task<ApiResponse<UserResponse>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserResponse> lstUserResponses = new List<UserResponse>();
               
                User user  = new User();    

                user.UserName = request.UserRequest.UserName;   
                user.Email = request.UserRequest.Email;
                user.Address = request.UserRequest.Address; 
                user.PhoneNo = request.UserRequest.PhoneNo;

                context.Users.Add(user);        
                await context.SaveChangesAsync();

                var userResponse  = Mappers.UserMapper.MapToUserResponse(user);
                lstUserResponses.Add(userResponse);

                return new ApiResponse<UserResponse>(lstUserResponses, HttpStatusCode.Created, "User Add Successfully", 0);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
