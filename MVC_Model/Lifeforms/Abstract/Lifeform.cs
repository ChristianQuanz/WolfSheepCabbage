namespace WolfSchafKohlkopf.Model.Lifeforms.Abstract
{
    abstract class Lifeform
    {
        #region public properties
        public int FoodChainRank { get; set; } = 0; // 0 = außerhalb der Nahrungskette, 1 = Alpha-Predator, n = Opfer

        public string Name { get; set; } = string.Empty; // Name der Lebensform (für das Logging)
        #endregion
    }
}
