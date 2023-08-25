using System.Collections.Generic;
using Verse;

namespace Core40k
{
    public class RecipeWorkerClass_Ritual : RecipeWorker
    {
        public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
        {
            if (recipe.GetModExtension<DefModExtension_Ritual>() == null)
            {
                return;
            }
            List<GeneDef> genesToGive = recipe.GetModExtension<DefModExtension_Ritual>().givesGenes;
            List<GeneDef> genesToRemove = recipe.GetModExtension<DefModExtension_Ritual>().removesGenes;

            if (billDoer.genes != null)
            {
                //Removes genes to remove
                if (!genesToRemove.NullOrEmpty())
                {
                    List<Gene> removeThese = new List<Gene>();
                    foreach (Gene gene in billDoer.genes.Xenogenes)
                    {
                        if (genesToRemove.Contains(gene.def))
                        {
                            removeThese.Add(gene);
                        }
                    }
                    foreach (Gene gene in removeThese)
                    {
                        billDoer.genes.RemoveGene(gene);
                    }
                }
                //Adds genes to give
                if (!genesToGive.NullOrEmpty())
                {
                    foreach (GeneDef gene in genesToGive)
                    {
                        if (gene != null && !billDoer.genes.HasGene(gene))
                        {
                            billDoer.genes.AddGene(gene, true);
                        }
                    }
                }
            }
        }

    }

}