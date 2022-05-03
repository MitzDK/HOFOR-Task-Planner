using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HOFORTaskPlanner.Models
{
    public class Assignment
    {
        public enum AssignmentStatus
        {
            Waiting = 1,
            Archived = 2,
            Hold = 3,
            Active = 4,
            New = 5,
            Portfolio = 6,
            Finished = 7
        }

        public enum AssignmentType
        {
            Analyse = 1,
            Drift = 2,
            Implementering = 3,
            Indsats = 4,
            Kontinuær = 5,
            Udvikling = 6
        }
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }
        [Required]
        public AssignmentStatus Status { get; set; }
        [Required]
        public AssignmentType Type { get; set; }
        [Required]

        public string Area { get; set; }
        [Required]

        public string Contact { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public int Estimate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]

        public string Comment { get; set; }
        [Required]
        public int AktionUserId { get; set; }
        [Required]
        public int ControlUserId { get; set; }

        public User Aktion { get; set; }

        public User Control { get; set; }

        public Assignment()
        {
            
        }

        public Assignment(AssignmentStatus status, AssignmentType type, string area, string contact, string description, int estimate, DateTime startDate, DateTime endDate, string comment, User aktion, User control)
        {
            Status = status;
            Type = type;
            Area = area;
            Contact = contact;
            Description = description;
            Estimate = estimate;
            StartDate = startDate;
            EndDate = endDate;
            Comment = comment;
            Aktion = aktion;
            Control = control;
        }
    }
}
