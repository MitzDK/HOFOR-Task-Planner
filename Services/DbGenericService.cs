using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace HOFORTaskPlanner.Services
{
    //Klassen er generisk og virker på alle typer, der også er klasser, hvilket betyder at den bruges til alle objekter vi vil gemme i vores database.
    public class DbGenericService<T> where T : class
    {
        //Henter og returnerer objekter af den givne type fra databasen.
        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using (var context = new TaskPlannerDbContext())
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }
        //Tilføjer objekt til database. 
        public async Task AddObjectAsync(T obj)
        {
            using (var context = new TaskPlannerDbContext())
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
        }
        //Sletter objekt fra database.
        public async Task DeleteObjectAsync(T obj)
        {
            using (var context = new TaskPlannerDbContext())
            {
                context.Set<T>().Remove(obj);
                await context.SaveChangesAsync();
            }
        }
        //Opdaterer objekts properties i database
        public async Task UpdateObjectAsync(T obj)
        {
            using (var context = new TaskPlannerDbContext())
            {
                context.Set<T>().Update(obj);
                await context.SaveChangesAsync();
            }
        }
        //Henter og returnere et enkelt objekt på baggrund af dets id
        public async Task<T> GetObjectByIdAsync(int id)
        {
            using (var context = new TaskPlannerDbContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }
    }
}
