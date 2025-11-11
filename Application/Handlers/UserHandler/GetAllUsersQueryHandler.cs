using Application.Queries.UserQueries;
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

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApiResponse<UserResponse>>
    {
        private readonly AppDbContext context;
        public GetAllUsersQueryHandler(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<ApiResponse<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var users = await context.Users.ToListAsync();


                if(users == null || users.Count == 0) 
                {
                    return new ApiResponse<UserResponse>(null, System.Net.HttpStatusCode.NotFound, "Users List Empty ",1);

                }
                var lstUserResponses = Mappers.UserMapper.MapToUserResponseList(users); 

                return new ApiResponse<UserResponse>(lstUserResponses,System.Net.HttpStatusCode.OK,"Users List ", 0);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
