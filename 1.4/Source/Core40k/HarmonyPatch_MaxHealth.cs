using HarmonyLib;
using System;
using Verse;


namespace Core40k
{
    [HarmonyPatch(typeof(BodyPartDef), "GetMaxHealth")]
    public class GetMaxHealthCorePatch
    {
        public static void Postfix(Pawn pawn, ref float __result)
        {
            if (pawn.genes != null)
            {
                float amount = 1;
                if (pawn.health.hediffSet.HasHediff(Core40kDefOf.BEWH_MutationRottingFlesh))
                {
                    amount -= 0.5f;
                }

                float temp = (float)Math.Round(__result * amount);
                __result = temp;
            }
        }
    }
}