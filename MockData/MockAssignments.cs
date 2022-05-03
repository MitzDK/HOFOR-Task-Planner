using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.MockData
{
    public class MockAssignments
    {
        private static List<Assignment> _assignments = new List<Assignment>()
        {
            new Assignment(Assignment.AssignmentStatus.Active, Assignment.AssignmentType.Implementering, "IT",
                "Placeholder", "Nyt projekt", 30, DateTime.Now, DateTime.Now, "blank",
                new User(), new User()),
            new Assignment(Assignment.AssignmentStatus.Active, Assignment.AssignmentType.Implementering, "IT",
                "Placeholder", "Stort projekt", 30, DateTime.Now, DateTime.Now, "Gentages årligt",
                new User(), new User()),
            new Assignment(Assignment.AssignmentStatus.Finished, Assignment.AssignmentType.Udvikling, "IT",
                "Janika", "Udvikling af bæredygtig IT-design", 100, DateTime.Now, DateTime.Now, "",
                new User(), new User()),


        };

        public static List<Assignment> GetMockAssignments()
        {
            return _assignments;
        }
    }
}
