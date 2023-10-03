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

        public static string GetLetterTitle(ChaosGods cg)
        {
            switch (cg)
            {
                case ChaosGods.Khorne:
                    return "KhorneLetterTitle".Translate();
                case ChaosGods.Tzeentch:
                    return "TzeentchLetterTitle".Translate();
                case ChaosGods.Slaanesh:
                    return "SlaaneshLetterTitle".Translate();
                case ChaosGods.Nurgle:
                    return "NurgleLetterTitle".Translate();
                case ChaosGods.Undivided:
                    return "UndividedLetterTitle".Translate();
                default:
                    Log.Error("Method ChaosEnumToString() tried to convert illegal enum to string, contact Phonicmas if you see this during normal gameplay");
                    return "";
            }
        }
    }
}