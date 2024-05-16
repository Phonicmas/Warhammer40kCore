using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Core40k
{
    [StaticConstructorOnStartup]
    public static class SpecificInheritableArchiteGenes
    {
        static SpecificInheritableArchiteGenes()
        {
            Core40kMod.harmony.Patch(AccessTools.Method(typeof(PregnancyUtility), "ApplyBirthOutcome"), null, new HarmonyMethod(AccessTools.Method(typeof(SpecificInheritableArchiteGenes), "Postfix")));
        }

        public static void Postfix(ref Thing __result, Pawn geneticMother, Pawn father)
        {
            Pawn pawn = (Pawn)__result;

            if (geneticMother == null || geneticMother.genes == null || father == null || father.genes == null)
            {
                return;
            }

            List<GeneDef> inheritedArchiteGenes = GetInheriteArchiteGenes(geneticMother.genes, father.genes);

            if (inheritedArchiteGenes.NullOrEmpty())
            {
                return;
            }

            foreach (GeneDef geneDef in inheritedArchiteGenes)
            {
                if (!pawn.genes.HasGene(geneDef))
                {
                    pawn.genes.AddGene(geneDef, false);
                }
            }
        }


        private static List<GeneDef> GetInheriteArchiteGenes(Pawn_GeneTracker mother, Pawn_GeneTracker father)
        {
            List<GeneDef> geneSetMother = mother.Endogenes.Select((Gene x) => x.def).ToList();
            List<GeneDef> geneSetFather = father.Endogenes.Select((Gene x) => x.def).ToList();

            List<GeneDef> finalGenes = new List<GeneDef>();


            foreach (GeneDef geneDef in geneSetMother)
            {
                if (finalGenes.Contains(geneDef))
                {
                    continue;
                }
                if (geneDef.biostatArc > 0 && geneDef.HasModExtension<DefModExtension_InheritableArchite>())
                {
                    if (!geneDef.GetModExtension<DefModExtension_InheritableArchite>().presentOnBothParentsRequired || geneSetFather.Contains(geneDef))
                    {
                        finalGenes.Add(geneDef);
                    }
                }
            }

            foreach (GeneDef geneDef in geneSetFather)
            {
                if (finalGenes.Contains(geneDef))
                {
                    continue;
                }
                if (geneDef.biostatArc > 0 && geneDef.HasModExtension<DefModExtension_InheritableArchite>())
                {
                    if (!geneDef.GetModExtension<DefModExtension_InheritableArchite>().presentOnBothParentsRequired)
                    {
                        finalGenes.Add(geneDef);
                    }
                }
            }

            return finalGenes;
        }
    }
}   