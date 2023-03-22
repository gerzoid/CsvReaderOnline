using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Users {
        public Guid UsersId { get; set; }

        [Required]
        [MinLength(5)]
        public string? Name { get; set; }
        public ICollection<Files>? Files { get; set; }
    }
}
