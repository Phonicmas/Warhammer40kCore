using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Core40k
{
    public class DefModExtension_Ritual : DefModExtension
    {
        public List<GeneDef> requiredGenes;
        public List<GeneDef> forbiddenGenes;
        public List<GeneDef> removesGenes; //Gene must also be present in required genes.

        public List<GeneDef> givesGenes;

        public ChaosGods giftGiver = ChaosGods.Undivided;
    }
}   