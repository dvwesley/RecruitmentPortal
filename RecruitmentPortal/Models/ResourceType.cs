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
    
    public partial class ResourceType
    {
        public ResourceType()
        {
            this.Resources = new HashSet<Resource>();
        }
    
        public int ResourceTypeId { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
