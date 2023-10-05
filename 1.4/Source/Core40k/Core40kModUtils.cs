using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using static Core40k.Core40kUtils;


namespace Core40k
{
    public class Core40kUtils
    {

        public struct GeneAndTraitInfo
        {
            public int opinionTrait;
            public int opinionGene;
            public bool willGiveBeneficial;
            public bool wontGiveGift;
        }

        public struct GodOpinion
        {
            public ChaosGods god;
            public int opinion;
            public Dictionary<int, int> degreeOpinion;
        }

        public static GeneAndTraitInfo AddToOpinion(int newOpinion, GeneAndTraitInfo t, bool isTrait)
        {
            if (isTrait)
            {
                t.opinionTrait += newOpinion;
            }
            else
            {
                t.opinionGene += newOpinion;
            }
            return t;
        }

        public static GeneAndTraitInfo ChangeBeneficialStatus(bool b, GeneAndTraitInfo t)
        {
            t.willGiveBeneficial = b;
            return t;
        }

        public static GeneAndTraitInfo ChangeWontGiftStatus(bool b, GeneAndTraitInfo t)
        {
            t.wontGiveGift = b;
            return t;
        }

        public static (Dictionary<ChaosGods, GeneAndTraitInfo>, bool) GetGeneAndTraitInfo(Pawn pawn)
        {
            Dictionary<ChaosGods, GeneAndTraitInfo> geneAndTraitInfos = new Dictionary<ChaosGods, GeneAndTraitInfo>
            {
                { ChaosGods.Undivided, new GeneAndTraitInfo { opinionTrait = 0, opinionGene = 0, willGiveBeneficial = false, wontGiveGift = false } },
                { ChaosGods.Khorne, new GeneAndTraitInfo { opinionTrait = 0, opinionGene = 0, willGiveBeneficial = false, wontGiveGift = false } },
                { ChaosGods.Tzeentch, new GeneAndTraitInfo { opinionTrait = 0, opinionGene = 0, willGiveBeneficial = false, wontGiveGift = false } },
                { ChaosGods.Nurgle, new GeneAndTraitInfo { opinionTrait = 0, opinionGene = 0, willGiveBeneficial = false, wontGiveGift = false } },
                { ChaosGods.Slaanesh, new GeneAndTraitInfo { opinionTrait = 0, opinionGene = 0, willGiveBeneficial = false, wontGiveGift = false } }
            };
            bool pure = false;

            List<Trait> pawnTraits = pawn.story.traits.allTraits;
            //Checks the traits of the pawns for thei influence on the outcome of the ritual.
            foreach (Trait trait in pawnTraits)
            {
                if (trait.def.HasModExtension<DefModExtension_TraitAndGeneOpinion>())
                {
                    DefModExtension_TraitAndGeneOpinion temp = trait.def.GetModExtension<DefModExtension_TraitAndGeneOpinion>();
                    if (temp.purity)
                    {
                        pure = true;
                    }
                    //Find the gods for the trait that wont gift the pawn if they have said trait.
                    foreach (ChaosGods god in temp.godWontGift)
                    {
                        geneAndTraitInfos.SetOrAdd(god, ChangeWontGiftStatus(true, geneAndTraitInfos.TryGetValue(god)));
                    }

                    //Gets the opinion of the trait
                    foreach (GodOpinion opinion in temp.godOpinion)
                    {
                        int t = 0;
                        if (!opinion.degreeOpinion.NullOrEmpty())
                        {
                            t = opinion.degreeOpinion.TryGetValue(trait.Degree);
                        }
                        else
                        {
                            t = opinion.opinion;
                        }

                        geneAndTraitInfos.SetOrAdd(opinion.god, AddToOpinion(t, geneAndTraitInfos.TryGetValue(opinion.god), true));
                    }

                    //Finds the gods that will guarentee a beneficial gift if chosen to gift the pawn.
                    foreach (ChaosGods god in temp.makesGodGiveBeneficial)
                    {
                        geneAndTraitInfos.SetOrAdd(god, ChangeBeneficialStatus(true, geneAndTraitInfos.TryGetValue(god)));
                    }
                }
            }


            List<Gene> pawnGenes = pawn.genes.GenesListForReading;
            //Checks the genes of the pawns for thei influence on the outcome of the ritual.
            foreach (Gene gene in pawnGenes)
            {
                if (gene.def.HasModExtension<DefModExtension_TraitAndGeneOpinion>())
                {
                    DefModExtension_TraitAndGeneOpinion temp = gene.def.GetModExtension<DefModExtension_TraitAndGeneOpinion>();
                    if (temp.purity)
                    {
                        pure = true;
                    }
                    //Find the gods for the gene that wont gift the pawn if they have said gene.
                    foreach (ChaosGods god in temp.godWontGift)
                    {
                        geneAndTraitInfos.SetOrAdd(god, ChangeWontGiftStatus(true, geneAndTraitInfos.TryGetValue(god)));
                    }

                    //Gets the opinion of the gene
                    foreach (GodOpinion opinion in temp.godOpinion)
                    {
                        int t = opinion.opinion;

                        geneAndTraitInfos.SetOrAdd(opinion.god, AddToOpinion(t, geneAndTraitInfos.TryGetValue(opinion.god), false));
                    }

                    //Finds the gods that will guarentee a beneficial gift if chosen to gift the pawn.
                    foreach (ChaosGods god in temp.makesGodGiveBeneficial)
                    {
                        geneAndTraitInfos.SetOrAdd(god, ChangeBeneficialStatus(true, geneAndTraitInfos.TryGetValue(god)));
                    }
                }
            }

            return (geneAndTraitInfos, pure);
        }

        public static void UnansweredCall(Pawn billDoer, ChaosGods giftGiver, bool isPure)
        {
            string letterText = "RitualCallsUnansweredLetter".Translate(billDoer.Named("PAWN"), ChaosEnumUtils.Convert(giftGiver));
            string messageText = "RitualCallsUnansweredMessage".Translate(billDoer.Named("PAWN"), ChaosEnumUtils.Convert(giftGiver));
            if (isPure)
            {
                messageText = "PawnIsPureReason".Translate(billDoer.Named("PAWN"), ChaosEnumUtils.Convert(giftGiver));
            }
            
            Find.LetterStack.ReceiveLetter(letterText, messageText, Core40kDefOf.BEWH_NoGiftGiven);
        }

        public static void AnsweredCall(Pawn billDoer, ChaosGods giftGiver, string grantedGiftsText)
        {
            string letterText = "RitualCallsAnsweredLetter".Translate(billDoer.Named("PAWN"), ChaosEnumUtils.Convert(giftGiver));
            string messageText = "RitualCallsAnsweredMessage".Translate(billDoer.Named("PAWN"), ChaosEnumUtils.Convert(giftGiver)) + grantedGiftsText;
            Find.LetterStack.ReceiveLetter(letterText, messageText, Core40kDefOf.BEWH_GiftGiven);
        }

        public static float GetOpinionBasedOnTraitsAndGenes(float chance, ChaosGods giftGiver, Dictionary<ChaosGods, GeneAndTraitInfo> geneAndTraitInfo)
        {
            Core40kSettings modSettings = LoadedModManager.GetMod<Core40kMod>().GetSettings<Core40kSettings>();

            chance += geneAndTraitInfo.TryGetValue(giftGiver).opinionTrait * modSettings.offsetPerHatedOrLovedTrait;
            chance += geneAndTraitInfo.TryGetValue(giftGiver).opinionGene * modSettings.offsetPerHatedOrLovedGene;
            
            return chance;
        }

        public static float GetOpinionBasedOnSkills(float chance, Pawn pawn, List<SkillDef> skillsScale, float skillsScaleAmount)
        {
            if (!skillsScale.NullOrEmpty())
            {
                SkillRecord skillRecord;
                foreach (SkillDef skill in skillsScale)
                {
                    skillRecord = pawn.skills.GetSkill(skill);
                    if (skillRecord != null)
                    {
                        chance += skillRecord.GetLevel() * skillsScaleAmount;
                    }
                }
            }

            return chance;
        }
    
    }
}