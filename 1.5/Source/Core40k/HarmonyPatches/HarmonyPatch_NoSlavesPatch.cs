using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;


namespace Core40k
{
    [HarmonyPatch(typeof(StockGenerator_Slaves), "GenerateThings")]
    public class DisallowSlaveBuyingPatch
    {
        public static IEnumerable<Thing> Postfix(IEnumerable<Thing> __result)
        {
			List<Thing> newResult = new List<Thing>();

            foreach (Thing thing in __result)
            {
				bool addThing = true;
				if (thing is Pawn pawn)
				{
					if (pawn.genes == null)
					{
						continue;
					}
                    if (pawn.genes.Xenotype.HasModExtension<DefModExtension_UntradeablePawn>())
					{
						addThing = false;
					}
				}
				if (addThing)
				{
					newResult.Add(thing);
				}
			}

			if (newResult.NullOrEmpty())
			{
				return __result;
			}

			return newResult;
        }
    }
}