﻿using RimWorld;
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
                return;
            }
            List<GeneDef> genesToGive = recipe.GetModExtension<DefModExtension_Ritual>().givesGenes;
            List<GeneDef> genesToRemove = recipe.GetModExtension<DefModExtension_Ritual>().removesGenes;

            List<SkillDef> skillsScale = recipe.GetModExtension<DefModExtension_Ritual>().skillsScale;
            float skillsScaleAmount = recipe.GetModExtension<DefModExtension_Ritual>().skillsScaleAmount;

            List<TraitDef> traitsIncreaseChance = recipe.GetModExtension<DefModExtension_Ritual>().traitsIncreaseChance;
            List<TraitDef> traitsReduceChance = recipe.GetModExtension<DefModExtension_Ritual>().traitsReduceChance;
            List<GeneDef> genesIncreaseChance = recipe.GetModExtension<DefModExtension_Ritual>().genesIncreaseChance;
            List<GeneDef> genesDecreaseChance = recipe.GetModExtension<DefModExtension_Ritual>().genesDecreaseChance;

            string giftGiver = recipe.GetModExtension<DefModExtension_Ritual>().giftGiver;

            if (billDoer.genes != null)
            {
                bool addAndRemoveGenes = false;
                float chance = recipe.GetModExtension<DefModExtension_Ritual>().baseChance;

                if (skillsScale != null)
                {
                    SkillRecord skillRecord;
                    foreach (SkillDef skill in skillsScale)
                    {
                        skillRecord = billDoer.skills.GetSkill(skill);
                        if (skillRecord != null)
                        {
                            chance += skillRecord.GetLevel()*skillsScaleAmount;
                        }
                    }
                }

                /*if (traitsIncreaseChance != null)
                {

                }

                if (traitsReduceChance != null)
                {

                }

                if (genesIncreaseChance != null)
                {

                }

                if (genesDecreaseChance != null)
                {

                }*/

                chance = (float)Math.Round(chance);

                Random rand = new Random();
                int randNum = rand.Next(100);

                if (randNum <= chance)
                {
                    addAndRemoveGenes = true;
                }

                string messageText;
                string letterText;
                if (addAndRemoveGenes)
                {
                    messageText = "RitualCallsAnsweredMessage".Translate(billDoer.Named("PAWN"), giftGiver);
                    letterText = "RitualCallsAnsweredLetter".Translate(billDoer.Named("PAWN"), giftGiver);
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
                    string grantedGiftsText = "\n";
                    //Adds genes to give
                    if (!genesToGive.NullOrEmpty())
                    {
                        foreach (GeneDef gene in genesToGive)
                        {
                            if (gene != null && !billDoer.genes.HasGene(gene))
                            {
                                billDoer.genes.AddGene(gene, true);
                                grantedGiftsText = grantedGiftsText + "\n- " + gene.label;
                            }
                        }
                    }
                    Find.LetterStack.ReceiveLetter(letterText, messageText, Core40kDefOf.BEWH_GiftGiven);
                }
                else
                {
                    messageText = "RitualCallsUnansweredMessage".Translate(billDoer.Named("PAWN"), giftGiver);
                    letterText = "RitualCallsUnansweredLetter".Translate(billDoer.Named("PAWN"), giftGiver);
                    Find.LetterStack.ReceiveLetter(letterText, messageText, Core40kDefOf.BEWH_NoGiftGiven);
                }

            }
        }

    }

}