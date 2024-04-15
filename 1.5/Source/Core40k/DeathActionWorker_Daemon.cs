using RimWorld;
using Verse;
using Verse.AI.Group;

namespace Core40k
{
    public class DeathActionWorker_Daemon : DeathActionWorker
    {

        public DeathActionProperties_Daemon Props => (DeathActionProperties_Daemon)props;

        public override void PawnDied(Corpse corpse, Lord prevLord)
        {
            FilthMaker.TryMakeFilth(corpse.Position, corpse.Map, ThingDefOf.Filth_CorpseBile, 3);
            corpse.Destroy();
        }
    }
}