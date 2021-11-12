using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfSchafKohlkopf.Model.Lifeforms;

namespace WolfSchafKohlkopf.Controller
{
    static class AnimalController
    {
        #region public extension methods
        public static void SetAsGuardian(this Animal pAnimal)
        {
            pAnimal.FoodChainRank = 0;
        }
        #endregion
    }
}
