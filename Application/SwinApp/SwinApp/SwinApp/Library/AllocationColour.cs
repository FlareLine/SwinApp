using SQLite;

namespace SwinApp.Library
{
    public class AllocationColour
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string SubjectCode { get; set; }

        public string HexCode { get; set; }
    }
}
