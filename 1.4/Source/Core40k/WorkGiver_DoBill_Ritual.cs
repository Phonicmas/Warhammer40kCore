using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Core40k
{
    public class WorkGiver_DoBill_Ritual : WorkGiver_DoBill
    {
        public override Job JobOnThing(Pawn pawn, Thing thing, bool forced = false)
        {
            if (thing is IBillGiver billGiver)
            {
                //Check is there is any recipe at all
                BillStack billStack = billGiver.BillStack;
                if (billStack.FirstShouldDoNow == null)
                {
                    return null;
                }
                if (!billStack.FirstShouldDoNow.recipe.HasModExtension<DefModExtension_Ritual>())
                {
                    return null;
                }
                if (pawn.genes == null || pawn.story == null)
                {
                    return null;
                }
                //Get info from modExtension
                List<GeneDef> forbiddenGenes = billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>().forbiddenGenes;
                List<GeneDef> requiredGenes = billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>().requiredGenes;

                //Checks if pawn has any genes its not allowed to have
                if (!forbiddenGenes.NullOrEmpty())
                {
                    foreach (GeneDef gene in forbiddenGenes)
                    {
                        if (gene != null && pawn.genes.HasGene(gene))
                        {
                            return null;
                        }
                    }
                }
                //Checks if pawn has any genes it needs to have
                if (!requiredGenes.NullOrEmpty())
                {
                    foreach (GeneDef gene in requiredGenes)
                    {
                        if (gene != null && !pawn.genes.HasGene(gene))
                        {
                            return null;
                        }
                    }
                }
            }
            return base.JobOnThing(pawn, thing, forced);
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (t is IBillGiver billGiver)
            {
                BillStack billStack = billGiver.BillStack;
                if (billStack.FirstShouldDoNow == null)
                {
                    return false;
                }
                if (!billStack.FirstShouldDoNow.recipe.HasModExtension<DefModExtension_Ritual>())
                {
                    return false;
                }
                if (pawn.genes == null || pawn.story == null)
                {
                    return false;
                }
                //Get info from modExtension
                List<GeneDef> forbiddenGenes = billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>().forbiddenGenes;
                List<GeneDef> requiredGenes = billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>().requiredGenes;
                //Checks if pawn has any genes its not allowed to have
                if (!forbiddenGenes.NullOrEmpty())
                {
                    foreach (GeneDef gene in forbiddenGenes)
                    {
                        if (gene != null && pawn.genes.HasGene(gene))
                        {
                            JobFailReason.Is("MayNotHaveGene".Translate(pawn.Named("PAWN"), gene.label));
                            return false;
                        }
                    }
                }
                //Checks if pawn has any genes it needs to have
                if (!requiredGenes.NullOrEmpty())
                {
                    foreach (GeneDef gene in requiredGenes)
                    {
                        if (gene != null && !pawn.genes.HasGene(gene))
                        {
                            JobFailReason.Is("DoesNotHaveGene".Translate(pawn.Named("PAWN"), gene.label));
                            return false;
                        }
                    }
                }
            }
            return base.HasJobOnThing(pawn, t, forced);
        }
    }

}