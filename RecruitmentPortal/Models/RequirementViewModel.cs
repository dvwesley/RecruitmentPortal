using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentPortal.Models
{
    public class RequirementDisplayModel
    {
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Number")]
        public string JobNumber { get; set; }

        public string Description { get; set; }

        public string Client { get; set; }

        public int RequirementId { get; set; }
    }

    public class RequirementDetailViewModel
    {
        public int RequirementDetailId { get; set; }

        public int RequirementId { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [Display(Name = "Referred By")]
        public int RecruiterId { get; set; }

        public IEnumerable<SuggestedResourceModel> Resources { get; set; }

        public static RequirementDetailViewModel Initialize(int requirementId)
        {
            var model = new RequirementDetailViewModel();
            model.RequirementId = requirementId;
            model.Resources = Resource.GetResources(requirementId);
            return model;
        }
    }
}

