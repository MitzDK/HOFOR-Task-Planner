using System;
using System.Collections.Generic;
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

        public int AssignmentId { get; set; }
        public AssignmentStatus Status { get; set; }
        public AssignmentType Type { get; set; }
        public string Area { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }
        public int Estimate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }

        //public User Aktion { get; set; }
        //public User Control { get; set; }

        public Assignment()
        {
            
        }
        //Skal lige laves igen efter merge.
        public Assignment(int assignmentId, AssignmentStatus status, AssignmentType type, string area, string contact, string description, int estimate, DateTime startDate, DateTime endDate, string comment)
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
        }
    }
}
