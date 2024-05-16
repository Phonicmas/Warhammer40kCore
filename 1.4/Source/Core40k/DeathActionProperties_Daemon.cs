using RimWorld;
using Verse;
using Verse.AI.Group;

namespace Core40k
{
    public class DeathActionProperties_Daemon : DeathActionProperties
    {
        public DeathActionProperties_Daemon()
        {
            workerClass = typeof(DeathActionWorker_Daemon);
        }
    } 
}