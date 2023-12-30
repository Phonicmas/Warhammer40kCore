using HarmonyLib;
using RimWorld;
using Verse;

namespace Core40k
{
    [HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), "Interacted")]
    public class UnrecruitablePatch
    {
        public static bool Prefix(Pawn initiator, Pawn recipient)
        {
            if (recipient.genes == null)
            {
                return true;
            }
            if (recipient.Faction == Faction.OfPlayer)
            {
                return true;
            }
            foreach (Gene gene in recipient.genes.GenesListForReading)
            {
                if (gene.def.HasModExtension<DefModExtension_SlaveabilityRecruitability>())
                {
                    if (!gene.def.GetModExtension<DefModExtension_SlaveabilityRecruitability>().canBeRecruited)
                    {
                        Find.LetterStack.ReceiveLetter("CannotRecruitLetter".Translate(), "CannotRecruitMessage".Translate(recipient.Named("PAWN"), initiator.Named("PAWN")), LetterDefOf.NeutralEvent);
                        return false;
                    }
                }
            }
            return true;
        }
    }

}   