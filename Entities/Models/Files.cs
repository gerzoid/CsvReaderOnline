using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Files {
        [Required]
        public Guid FilesId { get; set; }
        public string? OriginalName { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? Size { get; set; }
        public bool HasHeader { get; set; }
        public char Separator { get; set; }
        public int CountColumns { get; set; }
        public int CountRows { get; set; }
        public Guid UserId { get; set; }
        public Users? User { get; set; }
    }
}
