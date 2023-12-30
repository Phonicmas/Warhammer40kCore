using HarmonyLib;
using UnityEngine;
using Verse;


namespace Core40k
{
    public class Core40kMod : Mod
    {
        public static Harmony harmony;
        public Core40kMod(ModContentPack content) : base(content)
        {
            harmony = new Harmony("Core40k.Mod");
            harmony.PatchAll();
        }
    }
}