namespace Entities.Dto
{
    public class FilesDto
    {
        public Guid FilesId { get; set; }
        public string? OriginalName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? Size { get; set; }
        public bool HasHeader { get; set; }
        public char Separator { get; set; }
        public int ColumnsCount { get; set; }
        public int RowsCount { get; set; }
    }
}
