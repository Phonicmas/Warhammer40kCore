using RimWorld;
using System;
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
                Core40kUtils.UnansweredCall(billDoer, ChaosGods.None, false);
                return;
            }

            ChaosGods giftGiver = recipe.GetModExtension<DefModExtension_Ritual>().giftGiver;

            if (Core40kUtils.PawnIsPure(billDoer))
            {
                Core40kUtils.UnansweredCall(billDoer, giftGiver, true);
                return;
            }

            List<GeneDef> genesToGive = recipe.GetModExtension<DefModExtension_Ritual>().givesGenes;
            List<GeneDef> genesToRemove = recipe.GetModExtension<DefModExtension_Ritual>().removesGenes;

            List<SkillDef> skillsScale = recipe.GetModExtension<DefModExtension_Ritual>().skillsScale;
            float skillsScaleAmount = recipe.GetModExtension<DefModExtension_Ritual>().skillsScaleAmount;

            float chance = recipe.GetModExtension<DefModExtension_Ritual>().baseChance;

            if (!skillsScale.NullOrEmpty())
            {
                SkillRecord skillRecord;
                foreach (SkillDef skill in skillsScale)
                {
                    skillRecord = billDoer.skills.GetSkill(skill);
                    if (skillRecord != null)
                    {
                        chance += skillRecord.GetLevel() * skillsScaleAmount;
                    }
                }
            }

            ((int, int), (bool, bool)) opinions = Core40kUtils.GetTraitAndGeneOpinion(billDoer, giftGiver);

            if (opinions.Item2.Item1)
            {
                Core40kUtils.UnansweredCall(billDoer, giftGiver, false);
                return;
            }

            int opinionDegreeTraits = opinions.Item1.Item1;
            int opinionDegreeGenes = opinions.Item1.Item2;

            Random rand = new Random();
            Core40kSettings modSettings = LoadedModManager.GetMod<Core40kMod>().GetSettings<Core40kSettings>();

            chance += opinionDegreeTraits * modSettings.offsetPerHatedOrLovedTrait;
            chance += opinionDegreeGenes * modSettings.offsetPerHatedOrLovedGene;
            chance = (float)Math.Round(chance);
            chance += rand.Next(-1 * modSettings.randomChanceRitualNegative, modSettings.randomChanceRitualPositive);

            int randNum = rand.Next(100);

            if (!(randNum <= chance))
            {
                Core40kUtils.UnansweredCall(billDoer, giftGiver, false);
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

            Core40kUtils.AnsweredCall(billDoer, giftGiver, grantedGiftsText);
        }
    }

}