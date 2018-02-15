﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kauffman.Api.SubscriptionAssessment.Models
{
    public class UserSubscription
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime SubscriptionStartDate { get; set; }

        public DateTime SubscriptionEndDate { get; set; }

        public bool IsSubscriptionActive { get; set; }

        public DateTime SubscriptionUpdateDate { get; set; }

        public ApplicationUser User { get; set; }

        public Subscription UserCurrentSubscription { get; set; }


    }

    public class Subscription
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string SubscriptionName { get; set; }

        public string Description { get; set; }

        public string DiscountDescription { get; set; }

        public int DiscountDuration { get; set; }

        public double Amount { get; set; }

        public SubscriptionType SubscriptionType { get; set; }
    }

    public class SubscriptionType
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Type { get; set; }

        public int DurationMonths { get; set; }
    }
}