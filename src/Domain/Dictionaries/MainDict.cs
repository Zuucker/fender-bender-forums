namespace Backend.Data
{
    public class MainDict
    {
        public enum OfferCondition
        {
            None = 0,
            New = 1,
            VeryGood = 2,
            Good = 3,
            Fair = 4,
            Poor = 5,
            Salvage = 6,
        }

        public enum OfferFuelType
        {
            Other = 0,
            Gasoline = 1,
            Diesel = 2,
            Electric = 3,
            Hybrid = 4,
            CNG = 5,
            LPG = 6,
            Hydrogen = 7,
            Ethanol = 8,
        }

        public enum AdditionalContentType
        {
            None = 0,
            Image = 1,
            Video = 2,
        }
    }
}
