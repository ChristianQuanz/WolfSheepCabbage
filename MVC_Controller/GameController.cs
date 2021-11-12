using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfSchafKohlkopf.Model.Locations;
using WolfSchafKohlkopf.Model.Lifeforms.Abstract;


namespace WolfSchafKohlkopf.Controller
{
    static class GameController
    {
        public static string TryMove(Location pOrigin, Location pDestination, Lifeform pGuardian, Lifeform pPassenger)
        {
            return Move(pOrigin, pDestination, out pOrigin, out pDestination, pGuardian, pPassenger, true);
        }
        public static string Move(Location pOrigin, Location pDestination, out Location pOriginAfterMove, out Location pDestinationAfterMove, Lifeform pGuardian, Lifeform pPassenger, bool pTryOnly = false)
        {
            // Es wir ein Move von einem zum anderen Ufer abgebildet.
            // Der Farmer (der Guardian) ist immer mit dabei, der andere Insasse kann einer der Spielfiguren sein - oder auch NULL
            
            string _result = string.Empty;

            //Entkopplung von den Parametern: Abbilden der beiden Locations auf lokale Instanzen
            Location _origin = new Location(pOrigin.Name, pOrigin.PointBase, pOrigin.GetAttendees());
            Location _destination = new Location(pDestination.Name, pDestination.PointBase, pDestination.GetAttendees());

            if (pGuardian is null) _result = "No guardian involved in this move"; // Ein Zug ohne den Bauern als Guardian ist nicht möglich
            else
            {
                // Führe den zug mit den lokalen Instanzen durch
                _result += _origin.Del(pGuardian);
                _result += _destination.Add(pGuardian);
                if (!(pPassenger is null))
                {
                    _result += _origin.Del(pPassenger);
                    _result += _destination.Add(pPassenger);
                }
            }

            if (_result == string.Empty) 
            {
                // Wenn der Zug rein formal funktioniert hat - Check auf Fressanfälle
                if (!_origin.IsSafe()) _result += "Hunger pangs at origin";
                if (!_destination.IsSafe()) _result += "Hunger pangs at destination";
            }

            if (!pTryOnly && _result == string.Empty)
            {
                // Wenn der Zug ok war und auch tatsächlich ausgeführt werden soll, werden die Out-Parameter mit den lokalen Objekte besetzt
                pOriginAfterMove = _origin;
                pDestinationAfterMove = _destination;
            }
            else
            {
                // Wenn der zug nicht asgeführt werden soll, werden die Out-Parameter mit den In-Parametern (den ursprungswerten) besetzt
                pOriginAfterMove = pOrigin;
                pDestinationAfterMove = pDestination;
            }

            return _result;
        }
    }
}
