using System;

namespace LinkContractor.DAL.Entities
{
    public class ShortCode
    {
        public int Code { get; set; }
        public Guid RelatedGuid { get; set; }

        public SavedData CorrespondingSavedData { get; set; }
    }
}