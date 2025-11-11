using Domain.DTOs.UserDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class UserMapper
    {
        public static UserResponse MapToUserResponse(User user)
        {
              UserResponse userResponse = new UserResponse();   

              userResponse.UserId = user.UserId;
              userResponse.UserName = user.UserName;
              userResponse.Email = user.Email;
              userResponse.Address = user.Address;
              userResponse.PhoneNo = user.PhoneNo;

            return userResponse;    
        }



        public static List<UserResponse> MapToUserResponseList(List<User> users)
        {
            List<UserResponse> userResponses = new List<UserResponse>();

            foreach (var user in users)
            {
                UserResponse userResponse = new UserResponse();

                userResponse.UserId = user.UserId;
                userResponse.UserName = user.UserName;
                userResponse.Email = user.Email;
                userResponse.Address = user.Address;
                userResponse.PhoneNo = user.PhoneNo;

                userResponses.Add(userResponse);    
            }  
            return userResponses;
        }

    }
}
