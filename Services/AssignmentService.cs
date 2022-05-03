﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.Services
{
    public class AssignmentService
    {
        private List<Assignment> _assignments;
        private DbGenericService<Assignment> DbService;

        public AssignmentService(DbGenericService<Assignment> dbService)
        {
            DbService = dbService;
            _assignments = DbService.GetObjectsAsync().Result.ToList();
        }

        public List<Assignment> GetAssigments()
        {
            return _assignments;
        }

        public async Task AddAssigmentAsync(Assignment newAssigment)
        {
            _assignments.Add(newAssigment);
            await DbService.AddObjectAsync(newAssigment);
        }

        public async Task<Assignment> GetAssigmentByIdAsync(int id)
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
            }
        }
    }
}