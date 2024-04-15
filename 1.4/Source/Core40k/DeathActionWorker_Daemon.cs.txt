using RimWorld;
using Verse;

namespace Core40k
{
    public class DeathActionWorker_Daemon : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse)
        {
            FilthMaker.TryMakeFilth(corpse.Position, corpse.Map, ThingDefOf.Filth_CorpseBile, 3);
            corpse.Destroy();
        }
    }
}