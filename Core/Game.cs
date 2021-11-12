using System.Collections.Generic;
using WolfSchafKohlkopf.Model.Lifeforms;
using WolfSchafKohlkopf.Model.Lifeforms.Abstract;
using WolfSchafKohlkopf.Model.Locations;
using WolfSchafKohlkopf.View;
using WolfSchafKohlkopf.Controller;

namespace WolfSchafKohlkopf.Core
{
    static class Game
    {
        public static void Run()
        {
            // Instanzierung Output-Kanal
            var _inout = new ConsoleInOut("Run");

            // Instanzierung aller beteiligten Personen
            var _farmer = new Animal("farmer");
            _farmer.SetAsGuardian();
            var _wolf = new Animal("wolf", pFoodChainRank: 1);
            var _sheep = new Animal("sheep", pFoodChainRank: 2);
            Plant _cabbage = new Plant("cabbage", pFoodChainRank: 3);          
            _inout.Write("Farmer, wolf, sheep and cabbage have come into life");

            // Instanzierung aller beteiligten Schauplätze
            Lifeform[] _attendees = { _farmer, _wolf, _sheep, _cabbage };
            Location _fromLocation = new Location("left bank (origin)", 0, _attendees);
            Location _toLocation = new Location("right bank (destination)", 1);
            _inout.Write("Now we have a river with a left bank (origin) and a right bank (destination)");

            _inout.WaitFoKey("Initialisation finished - gameboard completed. Now let's play!");

            // Spielablauf-Schleife
            bool _gameEnds = false;
            Lifeform _passenger = null;

            while (!_gameEnds)
            {

                // Achtung: Hier kommt ein geplanter Symmetriebruch!
                // =================================================
                //
                // Ziel des Spieles ist es bekanntlich, dass alle Passagiere am Ende ans andere Ufer kommen.
                // Daher wird eine Logik eingebaut, die eine Gewichtung der Züge "in Richtung Ziel"
                // vornimmt. Damit ist keine spezielle Spieltaktik vorbestimmt, sondern lediglich die allgemeine
                // Spielstrategie näher definiert: Es herrscht ja Konsens darüber, dass am Ende des Spieles
                // sämtliche Passagiere lebend vom Ursprungsstandort an den Zielstandort gelangen sollen.
                //
                // Jede Location hat daher eine Property "PointBase". Je höher diese ist, umso näher ist
                // sie am Ziel. Bei zwei Ufern hat das Linke Ufer den Wert 0 und das rechte Ufer
                // den Wert 1.
                //
                // a) Findet eine Fahrt vom Ufer mit dem niedrigeren zum Ufer mit dem höheren Wert statt, 
                // wird vorzugsweise ein Passagier mitgenommen und keine Leerfahrt unternommen.
                //
                // b) Bei einer Fahrt von einem Ufer mit einem höheren Punktwert zu einem Ufer mit einem 
                // niedrigeren Punktwert soll vorzugsweise leer gefahren werden und nur im Notfall ein Passagier
                // mitgenommen werden.
                //
                // c) Damit eine theoretiche Bildung von Endlosschleifen (ständiges Hin- und Herfahren eines
                //    Passagiers) unterbleibt, muss sichergestellt werden, dass nach einer Fahrt der jeweilige
                //    Passagier auch wirklich das Boot verlässt. Zu diesem Zweck wird das Objekt
                //    "_passenger" mit "null" initialisiert und bei Erstellung der Kandidatenlister als
                //    Ausschlusskriterium verwendet.

                // Zunächst wird die Liste der Passagierkandidaten gebildet.
                SortedList<int, Lifeform> _candidates = new SortedList<int, Lifeform>();
                int _i = 1;
                foreach (Lifeform _candidate in _fromLocation.GetAttendees())
                    if (_candidate.FoodChainRank > 0 && _candidate != _passenger)
                    {
                        _candidates.Add(_i, _candidate);
                        _i++;
                    }

                // Diese Liste ist je nach Fall verschiedenartig ausgeprägt.
                //
                // a) Fahrt Richtung Ziel: An as Ende der Liste wird mit Index _i ein Eintrag "Leerfahrt" (null) angefügt [2].
                //    Diese Option ist eigentlich nicht nötig, weil das Spiel aufgeht, indem immer ein Passagier mitgenommen
                //    werden kann. Sie wird hier realisiert, weil das Spiel nicht taktisch vorbestimmt werden soll. Der Symmetrie-
                //    bruch soll so klein wie möglich bleiben, denn die Idee ist, mit Minimaleingriff dafür zu sorgen, dass
                //    die Programmierung das Vorgehen selber berechnet.
                //
                // b) Fahrt Richtung Ursprung: Der Eintrag erfolgt vor dem Anfang der Liste mit Index 0 [3].
                if (_fromLocation.PointBase <= _toLocation.PointBase)
                    _candidates.Add(_i,null); // [2]
                else
                    _candidates.Add(0, null); // [3]

                // Die Kandidatenliste wird mittels TryMove daraufhin getestet, mit welchem Passagier-Kandidaten (inkl.
                // Leerfahrt, entweder als erste oder als letzte Option) ein gültiger Spielzug möglich wäre [4].
                //
                // Der erste zutreffende Kandidat wird verwendet [5].
                foreach (KeyValuePair<int, Lifeform> _candidate in _candidates)
                    if (GameController.TryMove(_fromLocation, _toLocation, _farmer, _candidate.Value) == string.Empty) // [4]
                    {
                        _passenger = _candidate.Value; // [5]
                        break;
                    }

                // Zu diesem Zeitpunkt sind Fahrtstrecke und Passagier bekannt. Der Zug kann ausgeführt werden.
                // Eigentlich kann kein Fehler auftreten, dennoch wird die Ergebnisvariable _result getestet.
                if (_passenger == null)
                    _inout.Write(string.Format("'{0}' => '{1}': empty", _fromLocation.Name, _toLocation.Name));
                else
                    _inout.Write(string.Format("'{0}' => '{1}': '{2}'",_fromLocation.Name, _toLocation.Name, _passenger.Name));
                string _result = GameController.Move(_fromLocation, _toLocation, out _fromLocation, out _toLocation, _farmer, _passenger);
                if (_result != string.Empty)
                {
                    _inout.Write("Fehler: " + _result);
                    return;
                }

                // Prüfung, ob am Zielort alle Beteiligen versammelt sind (dann ist das Spiel beendet).
                _gameEnds = (_toLocation.Attendees.Count == _attendees.Length);

                // Vertauschen der Rollen (From, To) der beiden Flussufer _leftBank und _rightBank
                if (!_gameEnds)
                    LocationController.Switch(_fromLocation, _toLocation, out _fromLocation, out _toLocation);
            }

            _inout.WaitFoKey("All attendees are at destination - game finished!");
        }
    }
}
