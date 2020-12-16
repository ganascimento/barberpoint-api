using System;

namespace GADev.BarberPoint.Identity.Models
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser<int>
    {
        public string Name { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}