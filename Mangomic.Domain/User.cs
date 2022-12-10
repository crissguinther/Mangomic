using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mangomic.Domain {
    [Index(nameof(Email), IsUnique = true)]
    public class User {
        public int Id { get; set; }
        
        [Required]
        public string? Username { get; set; }
        
        [Required]
        public string? Email { get; set; }
        
        [Required]
        public string? PasswordHash { get; set; }
        
        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}