//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecruitmentPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResourceDocument
    {
        public int ResourceDocumentId { get; set; }
        public int ResourceId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public System.DateTime UploadedTimestamp { get; set; }
        public string FileName { get; set; }
        public string UploadedBy { get; set; }
    
        public virtual DocumentType DocumentType { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
