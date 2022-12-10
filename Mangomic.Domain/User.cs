using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mangomic.Domain {
    [Index(nameof(Email), IsUnique = true)]
    public class User {
        public int Id { get; set; }
        [Required]
        public int Username { get; set; }
        [Required]
        public int Email { get; set; }
        public int PasswordHash { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}