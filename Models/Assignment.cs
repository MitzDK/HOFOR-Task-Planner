using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace HOFORTaskPlanner.Models
{
    public class Assignment
    {
        public enum AssignmentStatus
        {
            [Display(Name = "I gang")]
            Active = 1,
            [Display(Name = "Ny")]
            New = 2,
            [Display(Name = "Afventer")]
            Waiting = 3,
            [Display(Name = "Hold")]
            Hold = 4,
            [Display(Name = "Slut")]
            Finished = 5,
            [Display(Name = "Portefølje")]
            Portfolio = 6,
            [Display(Name = "Arkiveret")]
            Archived = 7
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }
        [Required(ErrorMessage = "Du skal vælge Status")]
        [Display(Name = "Status")]
        public AssignmentStatus Status { get; set; }
        [Required(ErrorMessage = "Du skal vælge Type")]
        [Display(Name = "Type")]
        public AssignmentType Type { get; set; }
        [Required(ErrorMessage = "Område skal udfyldes")]
        [Display(Name = "Område")]
        public string Area { get; set; }
        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Indtast et estimat")]
        [Display(Name = "Estimat")]
        public int Estimate { get; set; }
        [Required(ErrorMessage = "Indtast en startdato for opgaven")]
        [Display(Name = "Startdato")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Indtast en slutdato for opgaven")]
        [Display(Name = "Slutdato")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Kommentar")]

        public string Comment { get; set; }

        public int AktionUserId { get; set; }
        public int ControlUserId { get; set; }
        public int ContactId { get; set; }

        public Assignment()
        {

        }

        public Assignment(AssignmentStatus status, AssignmentType type, string area, string description, int estimate, DateTime startDate, DateTime endDate, string comment, int aktionUserId, int controlUserId, int contactId)
        {
            Status = status;
            Type = type;
            Area = area;
            Description = description;
            Estimate = estimate;
            StartDate = startDate;
            EndDate = endDate;
            Comment = comment;
            AktionUserId = aktionUserId;
            ControlUserId = controlUserId;
            ContactId = contactId;

        }
    }
}
