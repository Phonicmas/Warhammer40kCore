using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using static Core40k.Core40kUtils;

namespace Core40k
{
    public class RecipeWorkerClass_Ritual : RecipeWorker
    {
        public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
        {
            DefModExtension_Ritual defMod = recipe.GetModExtension<DefModExtension_Ritual>();

            List<GeneDef> genesToGive = defMod.givesGenes;
            List<GeneDef> genesToRemove = defMod.removesGenes;

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
            string grantedGiftsText = "\n\n";
            //Adds genes to give
            if (!genesToGive.NullOrEmpty())
            {
                foreach (GeneDef gene in genesToGive)
                {
                    if (gene != null && !billDoer.genes.HasGene(gene))
                    {
                        billDoer.genes.AddGene(gene, true);
                        grantedGiftsText = grantedGiftsText + "\n- " + gene.label.CapitalizeFirst();
                    }
                }
            }
        }
    }

}