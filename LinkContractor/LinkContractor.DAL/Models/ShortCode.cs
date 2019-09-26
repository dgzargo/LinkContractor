using System;

namespace LinkContractor.DAL.Models
{
    public class ShortCode
    {
        public int Code { get; set; }
        public Guid RelatedGuid { get; set; }

        public virtual SavedData CorrespondingSavedData { get; set; }
    }
}