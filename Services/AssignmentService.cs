using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.Services
{
    public class AssignmentService
    {
        private List<Assignment> _assignments;
        private DbGenericService<Assignment> DbService { get; set; }

        public AssignmentService(DbGenericService<Assignment> dbService)
        {
            DbService = dbService;

            //_assignments = MockData.MockAssignments.GetMockAssignments();
            //InitializeDB();

            _assignments = DbService.GetObjectsAsync().Result.ToList();
        }

        public async Task InitializeDB()
        {
            foreach (var assignment in _assignments)
            {
                await DbService.AddObjectAsync(assignment);
            }
        }
        public List<Assignment> GetAssignments()
        {
            return _assignments;
        }

        public async Task AddAssignmentAsync(Assignment newAssigment)
        {
            _assignments.Add(newAssigment);
            await DbService.AddObjectAsync(newAssigment);
        }

        public void AddAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);
        }

        public Assignment GetAssignmentById(int id)
        {
            foreach (var assignment in _assignments)
            {
                if (assignment.AssignmentId == id)
                {
                    return assignment;
                }
            }
            return null;
        }

        public List<Assignment> GetAssignmentsByUserId(int id)
        {
            return _assignments.FindAll(assignment => assignment.AktionUserId.Equals(id));
        }
        public async Task<Assignment> GetAssignmentByIdAsync(int id)
        {
            return await DbService.GetObjectByIdAsync(id);
        }

        public async Task<Assignment> DeleteAssignmentAsync(int id)
        {
            Assignment assigmentToBeDeleted = null;
            foreach (var asgn in _assignments)
            {
                if (asgn.AssignmentId == id)
                {
                    assigmentToBeDeleted = asgn;
                    break;
                }
            }
            if (assigmentToBeDeleted != null)
            {
                _assignments.Remove(assigmentToBeDeleted);
                await DbService.DeleteObjectAsync(assigmentToBeDeleted);
            }
            return assigmentToBeDeleted;
        }

        public async Task UpdateAssignmentAsync(Assignment assignment)
        {

            if (assignment != null)
            {
                await DbService.UpdateObjectAsync(assignment);
                _assignments = DbService.GetObjectsAsync().Result.ToList();
            }
        }

        public int AmountOfAssignmentsForUserId(int userId)
        {
            return _assignments.Count(assign => assign.AktionUserId.Equals(userId));
        }
        public int AmountOfAssignmentsForYearAndMonthAndUserId(int year, int month, int userId)
        {
            var dateTime = new DateTime(year, month, 1);
            return _assignments
                .Count(assign =>
                    assign.AktionUserId.Equals(userId) &&
                    assign.StartDate <= dateTime && assign.EndDate >= dateTime);

        }
    }
}
