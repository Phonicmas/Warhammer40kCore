using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using VFECore;


namespace Core40k
{
    [HarmonyPatch(typeof(PawnUtility), "ShouldSendNotificationAbout")]
    public class NoDaemonNotificationPatch
    {
        public static void Postfix(ref bool __result, Pawn p)
        {
            if (__result && (p.kindDef == Core40kDefOf.BEWH_Plaguebearer || p.kindDef == Core40kDefOf.BEWH_SummonedBloodletter || p.kindDef == Core40kDefOf.BEWH_SummonedBlueHorror || p.kindDef == Core40kDefOf.BEWH_SummonedDaemonette))
            {
                __result = false;
            }
        }
    }
}