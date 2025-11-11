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
    public class GetUserByUserIdQueryHandler : IRequestHandler<GetUserByUserIdQuery, ApiResponse<UserResponse>>
    {
        private readonly AppDbContext context;
        public GetUserByUserIdQueryHandler(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResponse<UserResponse>> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserResponse> lstUserResponses = new List<UserResponse>();
                var user = await context.Users.Where(u => u.UserId == request.UserId).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new ApiResponse<UserResponse>(null, System.Net.HttpStatusCode.NotFound, "User Not Found", 1);
                }


                var userResponse = Mappers.UserMapper.MapToUserResponse(user);
                lstUserResponses.Add(userResponse);

                return new ApiResponse<UserResponse>(lstUserResponses, System.Net.HttpStatusCode.OK, "User Found Successfully", 0);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
