using System;

namespace LinkContractor.DAL.Entities
{
    public class SavedData
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Message { get; set; }
        public bool IsLink { get; set; }
        public Guid? User { get; set; }
        public DateTime Created { get; set; }
        public int? TimeLimit { get; set; }
        public DateTime? EndTime { get; set; }
        public int? ClickLimit { get; set; }
        public Guid? Group { get; set; }

        public ShortCode ShortCode { get; set; }
    }
}