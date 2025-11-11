using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UserDTOs
{
    public  class UserResponse
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? PhoneNo { get; set; }
    }
}
