using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Mangomic.Domain.Enums;

namespace Mangomic.Domain {
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Username), IsUnique = true)]
    public class User {
        public int Id { get; set; }
        
        [Required]
        public string? Username { get; set; }
        
        [Required]
        public string? Email { get; set; }
        
        [Required]
        public string? PasswordHash { get; set; }

        public int isAdmin { get; set; } = (int) UserRoles.DEFAULT;

        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}