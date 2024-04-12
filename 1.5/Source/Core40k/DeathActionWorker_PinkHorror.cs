﻿using RimWorld;
using Verse;
using Verse.AI.Group;

namespace Core40k
{
    public class DeathActionWorker_PinkHorror : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse, Lord prevLord)
        {
            FilthMaker.TryMakeFilth(corpse.Position, corpse.Map, ThingDefOf.Filth_CorpseBile, 3);

            PawnKindDef summon = Core40kDefOf.BEWH_SummonedBlueHorror;

            GenSpawn.Spawn(PawnGenerator.GeneratePawn(summon, corpse.InnerPawn.Faction), corpse.Position, corpse.Map);
            GenSpawn.Spawn(PawnGenerator.GeneratePawn(summon, corpse.InnerPawn.Faction), corpse.Position, corpse.Map);

            corpse.Destroy();
        }
    }
}