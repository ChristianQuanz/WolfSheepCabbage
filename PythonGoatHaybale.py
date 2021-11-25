def Move(pGameboard, pDirection, pDigit):
    _area = pGameboard.split("|")
    if _area[pDirection].find(pDigit.strip()) < 0: 
        return "x" # Fehler: zu bewegendes Digit ist nicht im Quellbereich vorhanden
    _area[pDirection] = "".join(sorted(_area[pDirection].replace(pDigit.strip(), "")))
    if _area[pDirection].find("2") >= 0 and len(_area[pDirection]) > 1: 
        return "x" # Fehler: 2 aufeinanderfolgende Digits: Fress-Fall
    _area[1-pDirection] = "".join(sorted(_area[1 - pDirection] + pDigit.strip()))
    return _area[0] + "|" + _area[1]

def GetCandidates(pGameboard, pDirection, pOldPassenger):
    if pDirection == 0: # Richtung Ziel
        return (pGameboard[0:pGameboard.find("|")] + " ").replace(pOldPassenger, "")
    else: # Richtung Ursprung
        return (" " + pGameboard[pGameboard.find("|")+1:]).replace(pOldPassenger, "")
        
_gameBoard = "123|"
_direction = 1
_oldPassenger = ""

print("Board: '" + _gameBoard + "'")
while not _gameBoard.find("|123") >= 0:
    _direction = 1 - _direction
    for _digit in (GetCandidates(_gameBoard, _direction, _oldPassenger)):
        _result = Move(_gameBoard, _direction, _digit)
        if _result != "x":           
            _gameBoard = _result
            _oldPassenger = _digit
            print("Move direction: '" + str(_direction) + "', digit: '" + _digit + "', board: '" + _gameBoard + "'")
            break
print("Game ends!")
input(">>")