using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Core40k
{
    public class Recipe_InstallImplantWithLevels : Recipe_Surgery
    {
        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (billDoer != null)
            {
                if (CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    return;
                }
                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, billDoer, pawn);
            }

            if (!pawn.health.hediffSet.HasHediff(recipe.addsHediff))
            {
                pawn.health.AddHediff(recipe.addsHediff, part);
            }
            else
            {
                pawn.health.hediffSet.GetFirstHediffOfDef(recipe.addsHediff).Severity += 1;
            }
        }

        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (!(thing is Pawn pawn))
            {
                return false;
            }

            if (pawn.health.hediffSet.HasHediff(recipe.addsHediff))
            {
                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(recipe.addsHediff);

                if (hediff.Severity == hediff.def.maxSeverity)
                {
                    return false;
                }
            }

            return true;
        }

        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            return MedicalRecipesUtility.GetFixedPartsToApplyOn(recipe, pawn, delegate (BodyPartRecord record)
            {
                if (!pawn.health.hediffSet.GetNotMissingParts().Contains(record))
                {
                    return false;
                }
                if (pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(record))
                {
                    return false;
                }
                return true;
            });
        }
    }
}   