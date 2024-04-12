using HarmonyLib;
using System;
using System.Collections.Generic;
using VanillaGenesExpanded;
using Verse;

namespace Core40k
{
    [HarmonyPatch(typeof(PawnGenerator), "GenerateGenes")]
    public class GenderDistributionPatch
    {
        public static void Postfix(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return;
            }
            List<Gene> genesListForReading = pawn.genes.GenesListForReading;
            float male = -1;
            float female = -1;
            foreach (Gene gene in genesListForReading)
            {
                if (gene.Active)
                {
                    GeneExtension modExtension = gene.def.GetModExtension<GeneExtension>();
                    if (modExtension != null && (modExtension.forceFemale || modExtension.forceMale))
                    {
                        return;
                    }
                    DefModExtension_GenderDistribution defModExtension = gene.def.GetModExtension<DefModExtension_GenderDistribution>();
                    if (defModExtension != null)
                    {
                        male = defModExtension.male;
                        female = defModExtension.female;
                    }
                }
            }

            if (male >= 0 && female >= 0)
            {
                Random rand = new Random();
                int rando = rand.Next(1, 100);
                if (rando <= male)
                {
                    pawn.gender = Gender.Male;
                }
                else
                {
                    pawn.gender = Gender.Female;
                }
            }

        }
    }

}   