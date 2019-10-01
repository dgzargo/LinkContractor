using System;

namespace LinkContractor.BLL.DTO
{
    public class SavedDataDTO
    {
        public Guid Guid { get; set; }
        public string Message { get; set; }
        public bool IsLink { get; set; }
        public Guid? User { get; set; }
        public DateTime Created { get; set; }
        public int? TimeLimit { get; set; }
        public DateTime? EndTime { get; set; }
        public int? ClickLimit { get; set; }
        public Guid? Group { get; set; }
        public int? Code { get; set; }
    }
}