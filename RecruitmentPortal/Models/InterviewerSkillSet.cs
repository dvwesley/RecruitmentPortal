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
    
    public partial class InterviewerSkillSet
    {
        public int InterviewerSkillSetId { get; set; }
        public int InterviewerId { get; set; }
        public int SkillSetId { get; set; }
        public System.DateTime CreatedTimestamp { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual Interviewer Interviewer { get; set; }
        public virtual SkillSet SkillSet { get; set; }
    }
}
