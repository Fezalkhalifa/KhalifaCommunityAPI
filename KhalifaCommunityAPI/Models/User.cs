using System;
using System.Collections.Generic;

namespace KhalifaCommunityAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public int RegistrationId { get; set; }
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

       // public virtual Registration Registration { get; set; } = null!;
    }
}
