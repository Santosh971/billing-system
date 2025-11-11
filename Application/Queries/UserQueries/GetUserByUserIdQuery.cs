using Application.Utility;
using Domain.DTOs.UserDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserQueries
{
    public class GetUserByUserIdQuery: IRequest<ApiResponse<UserResponse>>
    {
        public int UserId {  get; set; }    

        public GetUserByUserIdQuery(int userId) 
        {
            UserId = userId;    
        }
    }
}
