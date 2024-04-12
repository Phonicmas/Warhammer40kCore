using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using VFECore;


namespace Core40k
{
    public class Recipe_InstallImplantRequiringGene : Recipe_InstallImplant
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {

            if (!(thing is Pawn pawn && (pawn.genes != null) && recipe.HasModExtension<DefModExtension_RequiresGene>() && pawn.genes.HasGene(recipe.GetModExtension<DefModExtension_RequiresGene>().geneDef)))
            {
                return false;
            }

            return base.AvailableOnNow(thing, part);
        }
    }
}