using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kauffman.Api.SubscriptionAssessment.Models
{
    public class UserAssessmentStatus
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime AssessmentDate { get; set; }

        //public DateTime CurrentAssessmentDate { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual List<UserAssessmentAnswer> UserAssessmentAnswers { get; set; }
    }

    public class UserAssessmentAnswer
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Answer { get; set; }
        public virtual AssessmentQuestion Question { get; set; }

        public virtual UserAssessmentStatus UserAssementStatus { get; set; }
    }


    public class AssessmentQuestion
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string QuestionText { get; set; }

        public Guid? ParentQuestionId { get; set; }

        public virtual QuestionType QuestionType { get; set; }
    }

    public class QuestionType
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Type {get ; set;}

        public double MinValue { get; set; }

        public double MaxValue { get; set; }
    }
}