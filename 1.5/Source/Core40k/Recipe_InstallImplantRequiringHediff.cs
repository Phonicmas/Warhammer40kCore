using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using VFECore;


namespace Core40k
{
    public class Recipe_InstallImplantRequiringHediff : Recipe_InstallImplant
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (!(thing is Pawn pawn && pawn.health.hediffSet.HasHediff(recipe.GetModExtension<DefModExtension_RequiresHediff>().hediffDef)) || !recipe.HasModExtension<DefModExtension_RequiresHediff>())
            {
                return false;
            }

            return base.AvailableOnNow(thing, part);
        }
    }
}