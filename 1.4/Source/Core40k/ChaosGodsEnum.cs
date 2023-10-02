using Verse;

namespace Core40k
{
    public enum ChaosGods
    {
        None,
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
                    return "KhorneName".Translate();
                case ChaosGods.Tzeentch:
                    return "TzeentchName".Translate();
                case ChaosGods.Slaanesh:
                    return "SlaaneshName".Translate();
                case ChaosGods.Nurgle:
                    return "NurgleName".Translate();
                case ChaosGods.Undivided:
                    return "UndividedName".Translate();
                default:
                    Log.Error("Method ChaosEnumToString() tried to convert illegal enum to string, contact Phonicmas if you see this during normal gameplay");
                    return "";
            }
        }
    }
}