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

        public AktionService()
        {
            _aktionList = MockData.MockAktions.GetAktions();
        }

        public List<Aktion> GetAktions()
        {
            return _aktionList;
        }

        public void AddAktion(Aktion newAktion)
        {
            _aktionList.Add(newAktion);
        }
    }
}
