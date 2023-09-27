using RimWorld;
using Verse;

namespace Core40k
{
    [DefOf]
    public static class Core40kDefOf
    {
        public static DamageDef BEWH_WarpFlame;

        public static PawnKindDef BEWH_SummonedBlueHorror;

        public static LetterDef BEWH_GiftGiven;
        public static LetterDef BEWH_NoGiftGiven;

        public static HediffDef BEWH_MutationRottingFlesh;

        static Core40kDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(Core40kDefOf));
        }
    }
}