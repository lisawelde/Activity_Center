using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        
        [DataType(DataType.Date, ErrorMessage = "Date cannot be in the past.")]
        [FutureDate]
        [DisplayFormat(DataFormatString = "{0:MMM dd}")]
        public DateTime Date { get; set; }

        public int DurationAmount { get; set; }

        public string DurationType { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public int UserId { get; set; }

        public int PlannerId { get; set; }

        public User Planner { get; set; }

        public List<Join> AttendingUsers { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}