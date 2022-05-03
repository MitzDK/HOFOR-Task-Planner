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
                "Placeholder", "Nyt projekt", 30, new DateTime(2022, 5, 1), new DateTime(2022, 5, 21), "blank",
                new User(), new User()),
            new Assignment(Assignment.AssignmentStatus.Active, Assignment.AssignmentType.Implementering, "IT",
                "Placeholder", "Stort projekt", 30, new DateTime(2022, 6, 1), new DateTime(2022, 6, 30), "Gentages årligt",
                new User(), new User()),
            new Assignment(Assignment.AssignmentStatus.Finished, Assignment.AssignmentType.Udvikling, "IT",
                "Janika", "Udvikling af bæredygtig IT-design", 100, new DateTime(2022, 1, 1), new DateTime(2022, 4, 31), "",
                new User(), new User()),


        };

        public static List<Assignment> GetMockAssignments()
        {
            return _assignments;
        }
    }
}
