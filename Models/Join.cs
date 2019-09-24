using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class Join
    {
        [Key]
        public int JoinId { get; set; }

        public int UserId { get; set; }

        public int PlanId { get; set; }

        public User Joiner { get; set; }

        public Plan JoinedPlan { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}