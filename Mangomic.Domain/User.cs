using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Mangomic.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mangomic.Domain {
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Username), IsUnique = true)]
    public class User {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
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