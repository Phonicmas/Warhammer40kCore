using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Core40k
{
    public class DefModExtension_Mutations : DefModExtension
    {
        public List<BodyPartDef> canApplyToPart;

        public bool applyToAllParts = false;
    }

}   