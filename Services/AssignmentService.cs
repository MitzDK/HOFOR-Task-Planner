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
        //Initialiserer database med objekter fra liste. Denne ville bruges til at gemme objekter, som eksempelvist tidligere var gemt i en JSON-file eller kun eksisterede som MockData i en klasse for sig. 
        public async Task InitializeDB()
        {
            foreach (var assignment in _assignments)
            {
                await DbService.AddObjectAsync(assignment);
            }
        }
        //Returnere listen med Assignment-objekter som instanseres i konstruktøren med data enten fra MockData.MockAssignments eller database. Bruges i alle razorpages, som skal bruge en liste af Assignment-objekter
        public List<Assignment> GetAssignments()
        {
            return _assignments;
        }
        //Tilføjer et Assignment-objekt både til en lokal liste af assignments og til databasen.
        public async Task AddAssignmentAsync(Assignment newAssigment)
        {
            _assignments.Add(newAssigment);
            await DbService.AddObjectAsync(newAssigment);
        }
        //Tilføjer en Assignment til en lokal liste af assignments til brug med mockdata
        public void AddAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);
        }
        //Returnerer den Assignment, der har det givne Id. Bruges til at finde et specifikt Assignment-objekt. 
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
        //Returnerer en liste af Assignments, der har det givne AktionId. Bruges til at finde alle assignments som en enkelt bruger er aktion på.
        public List<Assignment> GetAssignmentsByUserId(int id)
        {
            return _assignments.FindAll(assignment => assignment.AktionUserId.Equals(id));
        }
        //Returnerer den Assignment med det givne Id fra databasen. 
        public async Task<Assignment> GetAssignmentByIdAsync(int id)
        {
            return await DbService.GetObjectByIdAsync(id);
        }
        //Bruges til at slette en Assignment fra database. 
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
        //Opdaterer et Assignment-objekt med ny data i databasen. 
        public async Task UpdateAssignmentAsync(Assignment assignment)
        {

            if (assignment != null)
            {
                await DbService.UpdateObjectAsync(assignment);
                _assignments = DbService.GetObjectsAsync().Result.ToList();
            }
        }
        //Bruges til at finde antallet af Assignments som en enkelt bruger er Aktion på. 
        public int AmountOfAssignmentsForUserId(int userId)
        {
            return _assignments.Count(assign => assign.AktionUserId.Equals(userId));
        }
        //Bruges til at finde antallet af Assignments som en enkelt bruger er Aktion på en specifik måned og år. Bruges på Dashboard-razorpage for at vise hvor mange opgaver og timer en medarbejder er knyttet til.
        public int AmountOfAssignmentsForYearAndMonthAndUserId(int year, int month, int userId)
        {
            var dateTime = new DateTime(year, month, 1);
            return _assignments
                .Count(assign =>
                    assign.AktionUserId.Equals(userId) &&
                    assign.StartDate <= dateTime && assign.EndDate >= dateTime);

        }
        //Bruges til at finde antallet af Assignments som en enhed(department) er tilegnet på en specifik måned og år. Bruges på Dashboard-razorpage for at vise hvor mange opgaver og timer der er knyttet til en enhed ( Department).
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
        //Finder alle assignments knyttet til en specifik department i et givet måned og år. Til brug i Dashboard-razorpage til at beregne antallet af timer på alle Assignments for et Assignment.
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

        //Bruger currentPage og pageSize til at vise hvor mange Assignments der skal fremgå på hver side, og for at dele listen af assignment op i flere dele. 
        public List<Assignment> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = _assignments;
            var test = data.OrderBy(Ass => Ass.AssignmentId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return test;
        }
        //Bruges til pagination i GetAssignments-razorpage. Metoden tager en liste af Assignment-objekter og imod både en int, der repræsenterer den nuværende side af pagination og hvor mange objekter hver side skal bestå af som argumenter
        public List<Assignment> GetPaginatedResultTest(IEnumerable<Assignment> assignmentList, int currentPage, int pageSize)
        {
            var data = assignmentList;
            var test = data.OrderBy(Ass => Ass.AssignmentId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return test;
        }
        //Bruges til at finde antallet af objekter i den lokale liste af assignments. 
        public async Task<int> GetCount()
        {
            return _assignments.Count;
        }
        //Bruges til at finde antallet af objekter i den lokale liste af assignments. 
        public int GetCounts()
        {
            return _assignments.Count;
        }
        //Returnerer en list af Assignments der opfylder krav for år, måned og brugerId. Listen bruges i Dashboard til at tælle antallet af opgaver den måned og år.
        public List<Assignment> AssigmentsForDateAndUserId(int year, int month, int userId)
        {
            var dateTime = new DateTime(year, month, 1);
            var tempList = _assignments.FindAll(assignment =>
                assignment.StartDate <= dateTime && assignment.EndDate >= dateTime &&
                assignment.AktionUserId == userId);
            return tempList;
        }
        //Bruges til at filtere en collection af Assignment efter AssignmentType inde på GetAssignments. Bruges blandt andet som argument i GetPaginatedResultTest.
        public IEnumerable<Assignment> FilterAssignmentType(Assignment.AssignmentType assignmentType)
        {
            var result = _assignments.Where(ass => ass.Type == assignmentType);
            if (result.Count() !=0)
            {
                return result;
            }
            return _assignments;
        }
        //Bruges til at filtere en collection af Assignment efter Description (beskrivelse) inde på GetAssignments
        public IEnumerable<Assignment> FilterAssignmentDescription(string description)
        {
            var results = _assignments.Where(ass => ass.Description.ToLower().Contains(description.ToLower()));
            if (results.Count() != 0)
            {
                return results;
            }

            return _assignments;
        }

        //Ikke i brug. Ville blive brugt til pagination i GetAssignments-razorpage. Metoden tager en liste af Assignment-objekter og imod både en int, der repræsenterer den nuværende side af pagination og hvor mange objekter hver side skal bestå af som argumenter
        public IEnumerable<Assignment> GetPaginatedResultList(IEnumerable<Assignment> assignmentList, int currentPage, int pageSize)
        {
            var data = assignmentList;
            var test = data.OrderBy(ass => ass.AssignmentId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return test;
        }
        //Bruges til at danne en liste af allerede eksistrende Descriptions (beskrivelser) fra alle Assignment-objekter for at bruge dem i en dynamisk Java-script input felt i CreateAssignment
        public List<string> GetDescriptions()
        {
            var tempList = new List<string>();
            foreach (var description in GetAssignments().Select(de => de.Description))
            {
                if (!tempList.Contains(description))
                {
                    tempList.Add(description);
                }
            }
            return tempList;
        }
        //Bruges til at danne en liste af allerede eksistrende Descriptions (beskrivelser) fra alle Assignment-objekter filtreret efter en given AssignmentType for at bruge dem i en dynamisk Java-script søgefelt i GetAssignments

        public List<string> GetDescriptionsByType(Assignment.AssignmentType descriptionType)
        {
            if (descriptionType != 0)
            {
                var tempList = new List<string>();
                foreach (var description in GetAssignments().Where(de => de.Type == descriptionType).Select(de => de.Description))
                {
                    if (!tempList.Contains(description))
                    {
                        tempList.Add(description);
                    }
                }
                return tempList;
            }
            return GetDescriptions();
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
        //Sortering efter ContactId i GetAssignments. Ikke implmenteret i GetAssignments.html, da det i nuværende form ikke fungerer med Pagination.
        public IEnumerable<Assignment> SortByContact(IEnumerable<Assignment> assignments)
        {
            return assignments.OrderBy(assign => assign.ContactId);
        }

        //Sortering efter Estimate i GetAssignments. Ikke implmenteret i GetAssignments.html, da det i nuværende form ikke fungerer med Pagination.
        public IEnumerable<Assignment> SortByEstimate()
        {
            return from assignment in _assignments
                orderby assignment.Estimate
                select assignment;
        }

        //Sortering efter Estimate omvendt rækkefølge i GetAssignments. Ikke implmenteret i GetAssignments.html, da det i nuværende form ikke fungerer med Pagination.
        public IEnumerable<Assignment> SortByEstimateDescending()
        {
            return from assignment in _assignments
                orderby assignment.Estimate descending 
                select assignment;
        }

        //Sortering efter StartDate i GetAssignments. Ikke implmenteret i GetAssignments.html, da det i nuværende form ikke fungerer med Pagination.
        public IEnumerable<Assignment> SortByStartDate()
        {
            return from assignment in _assignments 
                orderby assignment.StartDate 
                select assignment;
        }

        //Sortering efter StartDate omvendt rækkefølge i GetAssignments. Ikke implmenteret i GetAssignments.html, da det i nuværende form ikke fungerer med Pagination.
        public IEnumerable<Assignment> SortByStartDateDescending()
        {
            return from assignment in _assignments
                orderby assignment.StartDate descending 
                select assignment;
        }

        //Sortering efter EndDate i GetAssignments. Ikke implmenteret i GetAssignments.html, da det i nuværende form ikke fungerer med Pagination.
        public IEnumerable<Assignment> SortByEndDate()
        {
            return _assignments.OrderBy(a => a.EndDate);
        }
        //Sortering efter EndDate omvendt rækkefølge i GetAssignments. Ikke implmenteret i GetAssignments.html, da det i nuværende form ikke fungerer med Pagination.
        public IEnumerable<Assignment> SortByEndDateDescending()
        {
            return _assignments.OrderByDescending(a => a.EndDate);
        }
        //Bruges til at beregne samlede antal timer på alle TimeRegs for en Assignment. Dette er altså den totale skemalagte tid for en bestemt opgave. 
        public int GetHoursByAssignmentId(int id)
        {
            return _timeService.GetTimeByAssignmentId(id).Sum(time => time.Hours);
        }

        //Sortering efter resterende tid i GetAssignments. Ikke implmenteret i GetAssignments.html, da det i nuværende form ikke fungerer med Pagination.
        public IEnumerable<Assignment> SortByRemaining()
        {
            return _assignments.OrderBy(a => (a.Estimate - GetHoursByAssignmentId(a.AssignmentId)));

        }
        //Sortering efter restrende tid omvendt rækkefølge i GetAssignments. Ikke implmenteret i GetAssignments.html, da det i nuværende form ikke fungerer med Pagination.
        public IEnumerable<Assignment> SortbyRemainingDescending()
        {
            return _assignments.OrderByDescending(a => (a.Estimate - GetHoursByAssignmentId(a.AssignmentId)));

        }
    }
}
