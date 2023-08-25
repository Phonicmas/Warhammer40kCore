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

            //Check if pawn would have a job by vanilla standards
            if (base.JobOnThing(pawn, thing, forced) != null && thing is IBillGiver billGiver)
            {
                //Check is there is any recipe at all
                BillStack billStack = billGiver.BillStack;
                if (billStack.FirstShouldDoNow == null)
                {
                    return null;
                }
                if (billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>() == null)
                {
                    return null;
                }
                string recipeDefName = billStack.FirstShouldDoNow.recipe.defName;
                //Get info from modExtension
                List<GeneDef> forbiddenGenes = billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>().forbiddenGenes;
                List<GeneDef> requiredGenes = billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>().requiredGenes;

                if (recipeDefName != null && pawn.genes != null)
                {
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
                else if (recipeDefName == null)
                {
                    return null;
                }
                return base.JobOnThing(pawn, thing, forced);
            }
            return null;
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            bool hasJob = base.HasJobOnThing(pawn, t, forced);
            if (t is IBillGiver billGiver)
            {
                BillStack billStack = billGiver.BillStack;
                if (billStack.FirstShouldDoNow == null)
                {
                    return false;
                }
                if (billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>() == null)
                {
                    return false;
                }
                string recipeDefName = billStack.FirstShouldDoNow.recipe.defName;
                //Get info from modExtension
                List<GeneDef> forbiddenGenes = billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>().forbiddenGenes;
                List<GeneDef> requiredGenes = billStack.FirstShouldDoNow.recipe.GetModExtension<DefModExtension_Ritual>().requiredGenes;

                if (recipeDefName != null && !hasJob && pawn.genes != null)
                {
                    //Checks if pawn has any genes its not allowed to have
                    if (!forbiddenGenes.NullOrEmpty())
                    {
                        for (int i = 0; i < forbiddenGenes.Count; i++)
                        {
                            if (forbiddenGenes[i] != null && pawn.genes.HasGene(forbiddenGenes[i]))
                            {
                                JobFailReason.Is("May not have gene: " + forbiddenGenes[i].label.Translate());
                                return false;
                            }
                        }
                    }
                    //Checks if pawn has any genes it needs to have
                    if (!requiredGenes.NullOrEmpty())
                    {
                        for (int i = 0; i < requiredGenes.Count; i++)
                        {
                            if (requiredGenes[i] != null && !pawn.genes.HasGene(requiredGenes[i]))
                            {
                                JobFailReason.Is("Does not have gene: " + requiredGenes[i].label.Translate());
                                return false;
                            }
                        }
                    }
                }
                else if (recipeDefName == null)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }

}