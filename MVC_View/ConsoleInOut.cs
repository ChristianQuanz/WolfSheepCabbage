using System;
using LightCodes;

namespace WolfSchafKohlkopf.View
{
    class ConsoleInOut
    {
        #region ctor
        public ConsoleInOut(string pModuleName)
        {
            Logger = new Logger("WolfSchafKohlkopf", pModuleName, Logger.LogLevel.Verbose, true);
        }
        #endregion

        #region public methods
        public void Write(string pData)
        {
            Logger.Verbose(pData);
        }
        public ConsoleKeyInfo WaitFoKey(string pMessage = "")
        {
            if (pMessage == string.Empty)
                Write("Press <ANYKEY> to continue...\r\n");
            else
                Write(pMessage + "\r\n" + string.Empty.Pad(pMessage.Length,"-") + "\r\n\r\nPress <ANYKEY> to continue...\r\n");

            return Console.ReadKey(false);
        }
        #endregion

        #region private properties
        Logger Logger;
        #endregion

    }
}
