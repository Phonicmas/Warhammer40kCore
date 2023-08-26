using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Core40k
{
    public class RecipeWorkerClass_Geneseed : RecipeWorker
    {
        public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
        {
            if (recipe.GetModExtension<DefModExtension_Geneseed>() == null)
            {
                return;
            }

            List<GeneDef> genesToGive = recipe.GetModExtension<DefModExtension_Geneseed>().givesGenes;
            if (genesToGive.NullOrEmpty())
            {
                return;
            }

            string xenogermName = recipe.GetModExtension<DefModExtension_Geneseed>().xenogermName;
            XenotypeIconDef xenotypeIcon = recipe.GetModExtension<DefModExtension_Geneseed>().xenotypeIcon;

            Genepack genepack = (Genepack)ThingMaker.MakeThing(ThingDefOf.Genepack);
            genepack.Initialize(genesToGive);

            List<Genepack> genepacks = new List<Genepack>
            {
                genepack
            };

            Xenogerm xenogerm = (Xenogerm)ThingMaker.MakeThing(ThingDefOf.Xenogerm);
            xenogerm.Initialize(genepacks, xenogermName, xenotypeIcon);

            if (GenPlace.TryPlaceThing(((Thing)xenogerm), billDoer.PositionHeld, billDoer.MapHeld, ThingPlaceMode.Near))
            {
                return;
            }
            Log.Error("Could not drop item near " + (object)billDoer.PositionHeld);
        }

    }

}