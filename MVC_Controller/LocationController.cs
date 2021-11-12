using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfSchafKohlkopf.Model.Locations;
using WolfSchafKohlkopf.Model.Lifeforms.Abstract;

namespace WolfSchafKohlkopf.Controller
{
    static class LocationController
    {
        #region public extension methods
        public static bool IsSafe(this Location pLocation)
        {
            bool _isSafe = true;
            if (pLocation.Attendees.Count > 0 && !pLocation.Attendees.ContainsKey(0)) // Überprüfung nur, wenn Attendees vorhanden sind, aber kein Guardian
            {
                for (int _i=1; _i < pLocation.Attendees.Keys.Max(); _i++)
                {
                    if (pLocation.Attendees.ContainsKey(_i) && pLocation.Attendees.ContainsKey(_i+1)) // wenn 2 Anwesende in der Nahrungskette direkt nebeneinander stehen
                    {
                        _isSafe = false;
                        break;
                    }
                }
            }
            return _isSafe;
        }
        public static string Add(this Location pLocation, Lifeform pAttendee)
        {
            return Manipulate(pLocation, pAttendee, ManipulationFunction.Add);
        }
        public static string Del(this Location pLocation, Lifeform pAttendee)
        {
            return Manipulate(pLocation, pAttendee, ManipulationFunction.Del);
        }
        public static Lifeform[] GetAttendees(this Location pLocation)
        {
            Lifeform[] _attendees = new Lifeform[pLocation.Attendees.Count];
            int _i = 0;
            foreach (KeyValuePair<int,Lifeform> _attendee in pLocation.Attendees)
            {
                _attendees[_i] = _attendee.Value;
                _i++;
            }
            return _attendees;
        }
        public static bool Exists(this Location pLocation, Lifeform pAttendee)
        {
            return pLocation.Attendees.ContainsKey(pAttendee.FoodChainRank);
        }
        #endregion

        #region public methods
        public static void Switch(Location pFrom, Location pTo, out Location pFromOut, out Location pToOut)
        {
            pFromOut = pTo;
            pToOut = pFrom;
        }
        #endregion


        #region private methods
        private enum ManipulationFunction
        {
            Add = 0,
            Del = 1
        }
        private static string Manipulate(this Location pLocation, Lifeform pAttendee, ManipulationFunction pFn)
        {
            string _result = "";            
            try
            {
                if (pFn == ManipulationFunction.Add)
                    pLocation.Attendees.Add(pAttendee.FoodChainRank, pAttendee);
                else if (pFn == ManipulationFunction.Del)
                {
                    if (!pLocation.Attendees.Remove(pAttendee.FoodChainRank)) throw new Exception("Deletion failed");
                }
            }
            catch (Exception pEx)
            {
                _result = pEx.Message;
            }
            return _result;
        }
        #endregion
    }
}
