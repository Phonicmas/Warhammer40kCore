using System.Collections.Generic;
using Verse;

namespace Core40k
{
    public class RecipeWorkerClass_Geneseed : RecipeWorker
    {
        public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
        {
            if (recipe.GetModExtension<DefModExtension_Geneseed>() == null)
            {
                return;
            }
            List<GeneDef> genesToGive = recipe.GetModExtension<DefModExtension_Geneseed>().givesGenes;

            if (billDoer.genes != null)
            {
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