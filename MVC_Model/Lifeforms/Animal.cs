using WolfSchafKohlkopf.Model.Lifeforms.Abstract;

namespace WolfSchafKohlkopf.Model.Lifeforms
{
    class Animal: Lifeform
    {
        #region ctor
        public Animal(string pName) 
        { 
            base.Name = pName; 
        }
        public Animal(string pName, int pFoodChainRank) 
        { 
            base.Name = pName; 
            base.FoodChainRank = pFoodChainRank; 
        }
        #endregion
    }

}
