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
            if (recipe.GetModExtension<DefModExtension_Ritual>() == null)
            {
                UnansweredCall(billDoer, ChaosGods.None, false);
                return;
            }

            DefModExtension_Ritual defMod = recipe.GetModExtension<DefModExtension_Ritual>();
            ChaosGods giftGiver = defMod.giftGiver;

            (Dictionary<ChaosGods, GeneAndTraitInfo>, bool) geneAndTraitInfo = GetGeneAndTraitInfo(billDoer);

            //Pawn is pure
            if (geneAndTraitInfo.Item2)
            {
                UnansweredCall(billDoer, giftGiver, true);
                return;
            }

            //Gift giver wont give pawn gift
            if (geneAndTraitInfo.Item1.TryGetValue(giftGiver).wontGiveGift)
            {
                UnansweredCall(billDoer, giftGiver, false);
                return;
            }

            List<GeneDef> genesToGive = defMod.givesGenes;
            List<GeneDef> genesToRemove = defMod.removesGenes;

            Random rand = new Random();
            Core40kSettings modSettings = LoadedModManager.GetMod<Core40kMod>().GetSettings<Core40kSettings>();

            float chance = defMod.baseChance;
            chance += rand.Next(-1 * modSettings.randomChanceRitualNegative, modSettings.randomChanceRitualPositive);
            chance = GetOpinionBasedOnTraitsAndGenes(chance, giftGiver, geneAndTraitInfo.Item1);
            chance = GetOpinionBasedOnSkills(chance, billDoer, ChaosEnumUtils.GetGodAssociatedSkills(giftGiver), defMod.skillsScaleAmount);
            chance = (float)Math.Round(chance);

            int randNum = rand.Next(100);
            if (!(randNum <= chance))
            {
                UnansweredCall(billDoer, giftGiver, false);
                return;
            }

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

            AnsweredCall(billDoer, giftGiver, grantedGiftsText);
        }
    }

}