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
    
    public partial class Interviewer
    {
        public Interviewer()
        {
            this.Interviews = new HashSet<Interview>();
            this.InterviewerSkillSets = new HashSet<InterviewerSkillSet>();
        }
    
        public int InterviewerId { get; set; }
        public string Summary { get; set; }
        public bool AvailableonSundays { get; set; }
        public bool AvailableonMondays { get; set; }
        public bool AvailableonTuesdays { get; set; }
        public bool AvailableonWednesdays { get; set; }
        public bool AvailableonThursdays { get; set; }
        public bool AvailableonFridays { get; set; }
        public bool AvailabaleonSaturdays { get; set; }
        public Nullable<decimal> HourlyRate { get; set; }
        public Nullable<decimal> FixedFee { get; set; }
        public Nullable<int> ResourceId { get; set; }
        public System.DateTime CreatedTimestamp { get; set; }
        public Nullable<System.DateTime> UpdatedTimestamp { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual Resource Resource { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<InterviewerSkillSet> InterviewerSkillSets { get; set; }
    }
}
