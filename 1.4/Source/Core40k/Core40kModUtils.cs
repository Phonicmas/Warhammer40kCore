using RimWorld;
using System.Collections.Generic;
using Verse;


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
            public Pawn ownerPawn;
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

        public static GeneAndTraitInfo GetGeneAndTraitInfo(Pawn pawn, ChaosGods god)
        {
            GeneAndTraitInfo geneAndTraitInfo = new GeneAndTraitInfo
            {
                ownerPawn = pawn
            };

            List<Trait> pawnTraits = pawn.story.traits.allTraits;
            //Checks the traits of the pawns for thei influence on the outcome of the ritual.
            foreach (Trait trait in pawnTraits)
            {
                if (trait.def.HasModExtension<DefModExtension_TraitAndGeneOpinion>())
                {
                    DefModExtension_TraitAndGeneOpinion temp = trait.def.GetModExtension<DefModExtension_TraitAndGeneOpinion>();
                    //Find the gods for the trait that wont gift the pawn if they have said trait.
                    if (temp.godWontGift.Contains(god))
                    {
                        geneAndTraitInfo.wontGiveGift = true;
                    }
                    GodOpinion opinion = temp.godOpinion.Find(x => x.god == god);
                    if (opinion.god == god)
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
                        geneAndTraitInfo.opinionTrait = t;
                    }
                    if (temp.makesGodGiveBeneficial.Contains(god))
                    {
                        geneAndTraitInfo.willGiveBeneficial = true;
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
                    //Find the gods for the gene that wont gift the pawn if they have said gene.
                    if (temp.godWontGift.Contains(god))
                    {
                        geneAndTraitInfo.wontGiveGift = true;
                    }
                    GodOpinion opinion = temp.godOpinion.Find(x => x.god == god);
                    if (opinion.god == god)
                    {
                        geneAndTraitInfo.opinionTrait = opinion.opinion;
                    }
                    if (temp.makesGodGiveBeneficial.Contains(god))
                    {
                        geneAndTraitInfo.willGiveBeneficial = true;
                    }
                }
            }

            return geneAndTraitInfo;
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
    
    }
}