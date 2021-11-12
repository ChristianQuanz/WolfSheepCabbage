using WolfSchafKohlkopf.Model.Lifeforms.Abstract;

namespace WolfSchafKohlkopf.Model.Lifeforms
{
    class Plant : Lifeform
    {
        #region ctor
        public Plant(string pName, int pFoodChainRank) 
        {
            base.Name = pName;
            base.FoodChainRank = pFoodChainRank; 
        }
        #endregion
    }
}
