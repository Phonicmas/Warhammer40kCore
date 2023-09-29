using Verse;

namespace Core40k
{
    public enum ChaosGods
    {
        Khorne,
        Tzeentch,
        Slaanesh,
        Nurgle,
        Undivided
    }
    public class ChaosEnumToString
    {
        public static string Convert(ChaosGods cg)
        {
            switch (cg)
            {
                case ChaosGods.Khorne:
                    return "Khorne";
                case ChaosGods.Tzeentch:
                    return "Tzeentch";
                case ChaosGods.Slaanesh:
                    return "Slaanesh";
                case ChaosGods.Nurgle:
                    return "Nurgle";
                case ChaosGods.Undivided:
                    return "the great Undivided";
                default:
                    Log.Error("Method ChaosEnumToString() tried to convert illegal enum to string, contact Phonicmas if you see this during normal gameplay");
                    return "";
            }
        }
    }
}