using RimWorld;
using Verse;
using Verse.AI.Group;

namespace Core40k
{
    public class DeathActionProperties_PinkHorror : DeathActionProperties
    {
        public DeathActionProperties_PinkHorror()
        {
            workerClass = typeof(DeathActionWorker_PinkHorror);
        }
    }
}