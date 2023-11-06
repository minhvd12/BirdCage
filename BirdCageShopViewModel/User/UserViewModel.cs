using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopViewModel.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime DoB { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Gender { get; set; }
    }
}
