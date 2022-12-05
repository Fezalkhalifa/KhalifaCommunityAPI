using System;
using System.Collections.Generic;

namespace KhalifaCommunityAPI.Models
{
    public partial class Registration
    {
        public Registration()
        {
            User = new User();
        }

        public int RegistrationId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Zip { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual User User { get; set; }
    }
}
