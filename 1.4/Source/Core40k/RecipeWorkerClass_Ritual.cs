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
                return;
            }
            List<GeneDef> genesToGive = recipe.GetModExtension<DefModExtension_Ritual>().givesGenes;
            List<GeneDef> genesToRemove = recipe.GetModExtension<DefModExtension_Ritual>().removesGenes;

            List<SkillDef> skillsScale = recipe.GetModExtension<DefModExtension_Ritual>().skillsScale;
            float skillsScaleAmount = recipe.GetModExtension<DefModExtension_Ritual>().skillsScaleAmount;

            ChaosGods giftGiver = recipe.GetModExtension<DefModExtension_Ritual>().giftGiver;

            if (billDoer.genes != null)
            {
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

                int opinionDegreeTraits = 0;
                int opinionDegreeGenes = 0;

                List<Trait> pawnTraits = billDoer.story.traits.allTraits;
                //Checks the traits of the pawns for thei influence on the outcome of the ritual.
                foreach (Trait trait in pawnTraits)
                {
                    if (trait.def.HasModExtension<DefModExtension_TraitAndGeneOpinion>())
                    {
                        DefModExtension_TraitAndGeneOpinion temp = trait.def.GetModExtension<DefModExtension_TraitAndGeneOpinion>();
                        if (temp.purity)
                        {
                            UnansweredCall(billDoer, giftGiver);
                            return;
                        }
                        for (int i = 0; i < temp.godWontGift.Count; i++)
                        {
                            if (temp.godWontGift[i] == giftGiver)
                            {
                                UnansweredCall(billDoer, giftGiver);
                                return;
                            }
                        }
                        for (int i = 0; i < temp.godOpinion.Count; i++)
                        {
                            if (temp.godOpinion[i] == giftGiver)
                            {
                                int a = 0;
                                if (trait.Degree != 0)
                                {
                                    a = temp.opinionDegreeForTraitDegrees[i].TryGetValue(trait.Degree);
                                }
                                else
                                {
                                    a = temp.opinionDegree[i];
                                }
                                opinionDegreeTraits += a;
                            }
                        }
                    }
                }


                List<Gene> pawnGenes = billDoer.genes.GenesListForReading;
                //Checks the genes of the pawns for thei influence on the outcome of the ritual.
                foreach (Gene gene in pawnGenes)
                {
                    if (gene.def.HasModExtension<DefModExtension_TraitAndGeneOpinion>())
                    {
                        DefModExtension_TraitAndGeneOpinion temp = gene.def.GetModExtension<DefModExtension_TraitAndGeneOpinion>();
                        if (temp.purity)
                        {
                            UnansweredCall(billDoer, giftGiver);
                            return;
                        }
                        for (int i = 0; i < temp.godWontGift.Count; i++)
                        {
                            if (temp.godWontGift[i] == giftGiver)
                            {
                                UnansweredCall(billDoer, giftGiver);
                                return;
                            }
                        }
                        for (int i = 0; i < temp.godOpinion.Count; i++)
                        {
                            if (temp.godOpinion[i] == giftGiver)
                            {
                                opinionDegreeGenes += temp.opinionDegree[i];
                            }
                        }
                    }
                }

                Random rand = new Random();
                Core40kSettings modSettings = LoadedModManager.GetMod<Core40kMod>().GetSettings<Core40kSettings>();

                chance += opinionDegreeTraits * modSettings.offsetPerHatedOrLovedTrait;
                chance += opinionDegreeGenes * modSettings.offsetPerHatedOrLovedGene;
                chance = (float)Math.Round(chance);
                chance += rand.Next(-1*modSettings.randomChanceRitualNegative, modSettings.randomChanceRitualPositive);

                int randNum = rand.Next(100);

                if (!(randNum <= chance))
                {
                    return;
                }

                string messageText = "RitualCallsAnsweredMessage".Translate(billDoer.Named("PAWN"), ChaosEnumToString.Convert(giftGiver));
                string letterText = "RitualCallsAnsweredLetter".Translate(billDoer.Named("PAWN"), ChaosEnumToString.Convert(giftGiver));
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
        }

        private void UnansweredCall(Pawn billDoer, ChaosGods giftGiver)
        {
            string messageText = "RitualCallsUnansweredMessage".Translate(billDoer.Named("PAWN"), ChaosEnumToString.Convert(giftGiver));
            string letterText = "RitualCallsUnansweredLetter".Translate(billDoer.Named("PAWN"), ChaosEnumToString.Convert(giftGiver));
            Find.LetterStack.ReceiveLetter(letterText, messageText, Core40kDefOf.BEWH_NoGiftGiven);
        }

    }

}