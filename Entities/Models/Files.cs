using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models {
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
        public int ColumnsCount { get; set; }
        public int RowsCount { get; set; }
        public Guid UserId { get; set; }
        public Users? User { get; set; }
    }
}
