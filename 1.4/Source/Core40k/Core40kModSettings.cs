using Verse;


namespace Core40k
{
    public class Core40kSettings : ModSettings
    {
        public int randomChanceRitualPositive = 15;
        public int randomChanceRitualNegative = -15;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref randomChanceRitualPositive, "randomChanceRitual", 15);
            Scribe_Values.Look(ref randomChanceRitualNegative, "randomChanceRitual", -15);
            base.ExposeData();
        }
    }
}