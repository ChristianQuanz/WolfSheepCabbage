using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WolfSchafKohlkopf.Model.Lifeforms.Abstract;
using WolfSchafKohlkopf.Model.Lifeforms;

namespace WolfSchafKohlkopf.Model.Locations
{
    class Location
    {
        #region public properties
        public Dictionary<int, Lifeform> Attendees = new Dictionary<int, Lifeform>();
        public string Name { get; }
        public int PointBase { get; }
        #endregion

        #region ctors
        public Location(string pName, int pPointBase)
        {
            Name = pName;
            PointBase = pPointBase;
        }    
        public Location(string pName, int pPointBase, Lifeform[] pAttendees)
        {
            Name = pName;
            PointBase = pPointBase;
            foreach (Lifeform _attendee in pAttendees)
                Attendees.Add(_attendee.FoodChainRank, _attendee);
        }
        #endregion
    }
}
