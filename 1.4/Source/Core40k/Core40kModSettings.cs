using Verse;


namespace Core40k
{
    public class Core40kSettings : ModSettings
    {
        public int randomChanceRitualPositive = 15;
        //This setting is always positive and shoud be multiplied by -1 for it negative counterpart
        public int randomChanceRitualNegative = 15;

        public int offsetPerHatedOrLovedTrait = 2;

        public int offsetPerHatedOrLovedGene = 2;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref randomChanceRitualPositive, "randomChanceRitual", 15);
            Scribe_Values.Look(ref randomChanceRitualNegative, "randomChanceRitual", -15);
            Scribe_Values.Look(ref offsetPerHatedOrLovedTrait, "offsetPerHatedOrLovedTrait", 3);
            Scribe_Values.Look(ref offsetPerHatedOrLovedGene, "offsetPerHatedOrLovedGene", 2);
            base.ExposeData();
        }
    }
}