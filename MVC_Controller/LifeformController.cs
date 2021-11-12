using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfSchafKohlkopf.Model.Lifeforms.Abstract;
using WolfSchafKohlkopf.Model.Locations;


namespace WolfSchafKohlkopf.Controller
{
    static class LifeformController
    {
        #region public extension methods
        public static Location GetLocation(this Lifeform pAttendee, Location[] pLocations)
        {
            foreach(Location _loc in pLocations) 
                if (_loc.Exists(pAttendee)) 
                    return _loc;
            
            return null;
        }
        #endregion
    }
}
