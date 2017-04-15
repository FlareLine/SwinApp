using System;
using System.Collections.Generic;
using System.Text;

namespace SwinApp.Library
{
    /// <summary>
    /// A Unit from Blackboard
    /// </summary>
    /// <remarks>
    /// Will require better documentation
    /// </remarks>
    public class BlackboardUnit
    {
        public string Id { get; set; }
        public string UUID { get; set; }
        public string ExternalId { get; set; }
        public string DataSourceId { get; set; }
        public string CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool Organization { get; set; }
        public string UltraStatus { get; set; }
        public bool AllowGuests { get; set; }
        public bool ReadOnly { get; set; }
        public string TermId { get; set; }
    }
}
