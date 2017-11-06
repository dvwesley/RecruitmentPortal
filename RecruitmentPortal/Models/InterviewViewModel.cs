using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentPortal.Models
{
    public class InterviewViewModel
    {
        public int InterviewId { get; set; }
        public DateTime InterviewDate { get; set; }
        public int InterviewType { get; set; }
        public string InterviewStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double? AverageRating { get; set; }

        public string InterviewerName { get; set; }

        public int? InterviewerId { get; set; }

        public string InterviewStatusDisplay
        {
            get
            {
                return new Utility().InterviewStatus.Where(s => s.Value == InterviewStatus).FirstOrDefault().Text;
            }
        }

        public string InterviewTypeDisplay
        {
            get
            {
                if (InterviewType == 1)
                {
                    return "SwitchLane";
                }
                else if (InterviewType == 2)
                {
                    return "Client";
                }
                return string.Empty;
            }
        }
    }
}