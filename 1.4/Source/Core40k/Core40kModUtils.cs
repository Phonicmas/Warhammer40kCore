using RimWorld;
using System.Collections.Generic;
using Verse;


namespace Core40k
{
    public class Core40kUtils
    {
        public static void UnansweredCall(Pawn billDoer, ChaosGods giftGiver, bool isPure)
        {
            string letterText = "RitualCallsUnansweredLetter".Translate(billDoer.Named("PAWN"), ChaosEnumToString.Convert(giftGiver));
            string messageText = "RitualCallsUnansweredMessage".Translate(billDoer.Named("PAWN"), ChaosEnumToString.Convert(giftGiver));
            if (isPure)
            {
                messageText = "PawnIsPureReason".Translate(billDoer.Named("PAWN"), ChaosEnumToString.Convert(giftGiver));
            }
            
            Find.LetterStack.ReceiveLetter(letterText, messageText, Core40kDefOf.BEWH_NoGiftGiven);
        }

        public static void AnsweredCall(Pawn billDoer, ChaosGods giftGiver, string grantedGiftsText)
        {
            string letterText = "RitualCallsAnsweredLetter".Translate(billDoer.Named("PAWN"), ChaosEnumToString.Convert(giftGiver));
            string messageText = "RitualCallsAnsweredMessage".Translate(billDoer.Named("PAWN"), ChaosEnumToString.Convert(giftGiver)) + grantedGiftsText;
            Find.LetterStack.ReceiveLetter(letterText, messageText, Core40kDefOf.BEWH_GiftGiven);
        }

        public static bool PawnIsPure(Pawn pawn)
        {
            foreach (Trait trait in pawn.story.traits.allTraits)
            {
                if (trait.def.HasModExtension<DefModExtension_TraitAndGeneOpinion>() && trait.def.GetModExtension<DefModExtension_TraitAndGeneOpinion>().purity)
                {
                    return true;
                }
            }

            foreach (Gene gene in pawn.genes.GenesListForReading)
            {
                if (gene.def.HasModExtension<DefModExtension_TraitAndGeneOpinion>() && gene.def.GetModExtension<DefModExtension_TraitAndGeneOpinion>().purity)
                {
                    return true;
                }
            }

            return false;
        }
    
        public static ((int, int), (bool, bool)) GetTraitAndGeneOpinion(Pawn pawn, ChaosGods giftGiver)
        {
            int opinionDegreeTraits = 0;
            int opinionDegreeGenes = 0;
            bool isBeneficial = false;

            List<Trait> pawnTraits = pawn.story.traits.allTraits;
            //Checks the traits of the pawns for thei influence on the outcome of the ritual.
            foreach (Trait trait in pawnTraits)
            {
                if (trait.def.HasModExtension<DefModExtension_TraitAndGeneOpinion>())
                {
                    DefModExtension_TraitAndGeneOpinion temp = trait.def.GetModExtension<DefModExtension_TraitAndGeneOpinion>();
                    for (int i = 0; i < temp.godWontGift.Count; i++)
                    {
                        if (temp.godWontGift[i] == giftGiver)
                        {
                            return ((0, 0), (true, false));
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
                    for (int i = 0; i < temp.makesGodGiveBeneficial.Count; i++)
                    {
                        if (temp.makesGodGiveBeneficial[i] == giftGiver)
                        {
                            isBeneficial = true;
                            break;
                        }
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
                    for (int i = 0; i < temp.godWontGift.Count; i++)
                    {
                        if (temp.godWontGift[i] == giftGiver)
                        {
                            return ((0, 0), (true, false));
                        }
                    }
                    for (int i = 0; i < temp.godOpinion.Count; i++)
                    {
                        if (temp.godOpinion[i] == giftGiver)
                        {
                            opinionDegreeGenes += temp.opinionDegree[i];
                        }
                    }
                    for (int i = 0; i < temp.makesGodGiveBeneficial.Count; i++)
                    {
                        if (temp.makesGodGiveBeneficial[i] == giftGiver)
                        {
                            isBeneficial = true;
                            break;
                        }
                    }
                }
            }

            return ((opinionDegreeTraits, opinionDegreeGenes), (false, isBeneficial));
        }
    
    }
}