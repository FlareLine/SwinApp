using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
