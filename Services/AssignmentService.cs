using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using HOFORTaskPlanner.Pages.Assignment;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HOFORTaskPlanner.Services
{
    public class AssignmentService
    {
        private List<Assignment> _assignments;
        private UserService _userService;
        private TimeService _timeService;
        private ContactService _contactService;
        private DbGenericService<Assignment> DbService { get; set; }

        public AssignmentService(DbGenericService<Assignment> dbService, UserService userService, TimeService timeService, ContactService contactService)
        {
            DbService = dbService;

            //_assignments = MockData.MockAssignments.GetMockAssignments();
            //InitializeDB();
            _userService = userService;
            _timeService = timeService;
            _contactService = contactService;
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
        public int AmountOfAssignmentsForYearAndMonthAndUserDepartment(int year, int month, Models.User.UserDepartments department)
        {
            var dateTime = new DateTime(year, month, 1);
            List<User> userList = _userService.GetUsersByDepartment(department);
            List<Assignment> assignmentList = new List<Assignment>();
            foreach (var user in userList)
            {
                foreach (var assignment in GetAssignmentsByUserId(user.UserId))
                {
                    assignmentList.Add(assignment);
                }
            }
            return assignmentList
                .Count(assign => assign.StartDate <= dateTime && assign.EndDate >= dateTime);
        }
        public List<Assignment> AssignmentsForYearAndMonthAndUserDepartment(int year, int month, Models.User.UserDepartments department)
        {
            var dateTime = new DateTime(year, month, 1);
            List<User> userList = _userService.GetUsersByDepartment(department);
            List<Assignment> assignmentList = new List<Assignment>();
            foreach (var user in userList)
            {
                foreach (var assignment in GetAssignmentsByUserId(user.UserId))
                {
                    assignmentList.Add(assignment);
                }
            }
            return assignmentList
                .FindAll(assign => assign.StartDate <= dateTime && assign.EndDate >= dateTime);
        }


        public List<Assignment> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = _assignments;
            var test = data.OrderBy(Ass => Ass.AssignmentId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return test;
        }
        public List<Assignment> GetPaginatedResultTest(IEnumerable<Assignment> assignmentList, int currentPage, int pageSize)
        {
            var data = assignmentList;
            var test = data.OrderBy(Ass => Ass.AssignmentId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return test;
        }

        public async Task<int> GetCount()
        {
            return _assignments.Count;
        }

        public int GetCounts()
        {
            return _assignments.Count;
        }

        public List<Assignment> AssigmentsForDateAndUserId(int year, int month, int userId)
        {
            var dateTime = new DateTime(year, month, 1);
            var tempList = _assignments.FindAll(assignment =>
                assignment.StartDate <= dateTime && assignment.EndDate >= dateTime &&
                assignment.AktionUserId == userId);
            return tempList;
        }

        public IEnumerable<Assignment> FilterAssignmentType(Assignment.AssignmentType assignmentType)
        {
            var result = _assignments.Where(Ass => Ass.Type == assignmentType);
            if (result.Count() !=0)
            {
                return result;
            }
            return _assignments;
        }

        //public List<Assignment> SortPaginationHelper(string isFiltered, int filterValue, string isSorted, string sortValue, int currentPage, int pageSize)
        //{
        //    List<Assignment> newList = new List<Assignment>();

        //    switch (isFiltered)
        //    {
        //        case "true":
        //            var type = (Models.Assignment.AssignmentType) filterValue;
        //            newList = GetPaginatedResultTest(FilterAssignmentType(type), currentPage, pageSize);
        //            break;
        //        default:
        //            newList = GetPaginatedResult(currentPage, pageSize);
        //            break;
        //    }
        //    switch (isSorted)
        //    {
        //        case "true":
        //            switch (sortValue)
        //            {
        //                case "contact":
        //                    newList = SortByContact(newList).ToList();
        //                    break;
        //            }
        //            break;
        //    }
        //    return newList;
        //}
        public IEnumerable<Assignment> SortByContact(IEnumerable<Assignment> assignments)
        {
            return assignments.OrderBy(assign => assign.ContactId);
        }

        public IEnumerable<Assignment> SortByEstimate()
        {
            return from assignment in _assignments
                orderby assignment.Estimate
                select assignment;
        }

        public IEnumerable<Assignment> SortByEstimateDescending()
        {
            return from assignment in _assignments
                orderby assignment.Estimate descending 
                select assignment;
        }

        public IEnumerable<Assignment> SortByStartDate()
        {
            return from assignment in _assignments 
                orderby assignment.StartDate 
                select assignment;
        }

        public IEnumerable<Assignment> SortByStartDateDescending()
        {
            return from assignment in _assignments
                orderby assignment.StartDate descending 
                select assignment;
        }

        public IEnumerable<Assignment> SortByEndDate()
        {
            return _assignments.OrderBy(a => a.EndDate);
        }
        public IEnumerable<Assignment> SortByEndDateDescending()
        {
            return _assignments.OrderByDescending(a => a.EndDate);
        }

        public int GetHoursByAssignmentId(int id)
        {
            return _timeService.GetTimeByAssignmentId(id).Sum(time => time.Hours);
        }

        public IEnumerable<Assignment> SortByRemaining()
        {
            return _assignments.OrderBy(a => (a.Estimate - GetHoursByAssignmentId(a.AssignmentId)));

        }
        public IEnumerable<Assignment> SortbyRemainingDescending()
        {
            return _assignments.OrderByDescending(a => (a.Estimate - GetHoursByAssignmentId(a.AssignmentId)));

        }
    }
}
