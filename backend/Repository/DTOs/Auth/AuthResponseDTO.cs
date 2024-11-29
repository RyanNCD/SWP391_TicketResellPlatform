using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs.Auth
{
    public class AuthResponseDTO
    {
        public string? Token { get; set; }
        public DateTime? Expired { get; set; }
        public string? UserId { get; set; }
        public string? Username { get; set; }

    }
}
