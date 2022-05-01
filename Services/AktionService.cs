using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.Services
{
    public class AktionService
    {
        private List<Aktion> _aktionList;
        private DbGenericService<Aktion> DbService { get; set; }

        public AktionService(DbGenericService<Aktion> dbService)
        {
            DbService = dbService;
            //_aktionList = MockData.MockAktions.GetAktions();
            _aktionList = DbService.GetObjectsAsync().Result.ToList();
        }

        public List<Aktion> GetAktions()
        {
            return _aktionList;
        }

        public async Task AddAktionAsync(Aktion newAktion)
        {
            _aktionList.Add(newAktion);
            foreach (var VARIABLE in _aktionList)
            {
                await DbService.AddObjectAsync(VARIABLE);
            }
            
        }

        public async Task<Aktion> GetAktionAsync(int id)
        {
            return await DbService.GetObjectByIdAsync(id);
        }

        public async Task<Aktion> DeleteAktionAsync(int id)
        {
            Aktion aktionToBeDeleted = null;
            foreach (var akt in _aktionList)
            {
                if (akt.UserId == id)
                {
                    aktionToBeDeleted = akt;
                    break;
                }
            }
            if (aktionToBeDeleted != null)
            {
                _aktionList.Remove(aktionToBeDeleted);
                await DbService.DeleteObjectAsync(aktionToBeDeleted);
            }
            return aktionToBeDeleted;
        }

        public async Task UpdateItemAsync(Aktion akt)
        {
            if (akt != null)
            {
                await DbService.UpdateObjectAsync(akt);
            }
        }
    }
}
