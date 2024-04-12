using RimWorld;
using System.Collections.Generic;
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

    public enum DaemonParts
    {
        None,
        Wings,
        Tail,
        Horns,
        Hide,
        Color
    }
    public class ChaosEnumUtils
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
                    Log.Error("Method Convert() tried to convert illegal enum to string, contact Phonicmas if you see this during normal gameplay");
                    return "";
            }
        }

        public static string GetLetterTitle(ChaosGods cg)
        {
            switch (cg)
            {
                case ChaosGods.None:
                    return "NoneLetterTitle".Translate();
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
                    Log.Error("Method GetLetterTitle() tried to get illegal string, contact Phonicmas if you see this during normal gameplay");
                    return "";
            }
        }

        public static List<SkillDef> GetGodAssociatedSkills(ChaosGods cg)
        {
            switch (cg)
            {
                case ChaosGods.Khorne:
                    return new List<SkillDef>() { SkillDefOf.Melee, SkillDefOf.Shooting };
                case ChaosGods.Tzeentch:
                    return new List<SkillDef>() { SkillDefOf.Intellectual, SkillDefOf.Crafting };
                case ChaosGods.Slaanesh:
                    return new List<SkillDef>() { SkillDefOf.Social, SkillDefOf.Artistic };
                case ChaosGods.Nurgle:
                    return new List<SkillDef>() { SkillDefOf.Plants, SkillDefOf.Cooking };
                case ChaosGods.Undivided:
                    return new List<SkillDef>() { SkillDefOf.Plants, SkillDefOf.Cooking, SkillDefOf.Social, SkillDefOf.Artistic, SkillDefOf.Intellectual, SkillDefOf.Crafting, SkillDefOf.Melee, SkillDefOf.Shooting };
                default:
                    Log.Error("Method GetGodAssociatedSkills() tried to illegal skilldefs, contact Phonicmas if you see this during normal gameplay");
                    return null;
            }
        }
    }
}