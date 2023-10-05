using HarmonyLib;
using System.Runtime;
using UnityEngine;
using Verse;


namespace Core40k
{
    public class Core40kMod : Mod
    {
        public static Harmony harmony;

        readonly Core40kSettings settings;

        public Core40kMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<Core40kSettings>();
            harmony = new Harmony("Core40k.Mod");
            harmony.PatchAll();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label("randomChanceRitual".Translate(settings.randomChanceRitualPositive, settings.randomChanceRitualNegative));
            settings.randomChanceRitualPositive = (int)listingStandard.Slider(settings.randomChanceRitualPositive, 0, 100);
            settings.randomChanceRitualNegative = (int)listingStandard.Slider(settings.randomChanceRitualNegative, 0, 100);


            listingStandard.Label("OffsetExplanation".Translate());
            listingStandard.Label("offsetPerHatedOrLovedTrait".Translate(settings.offsetPerHatedOrLovedTrait));
            settings.offsetPerHatedOrLovedTrait = (int)listingStandard.Slider(settings.offsetPerHatedOrLovedTrait, 0, 20);

            listingStandard.Label("offsetPerHatedOrLovedGene".Translate(settings.offsetPerHatedOrLovedGene));
            settings.offsetPerHatedOrLovedGene = (int)listingStandard.Slider(settings.offsetPerHatedOrLovedGene, 0, 20);

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "ModSettingsNameCore".Translate();
        }
    }
}