using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Core40k
{
    public class DamageWorker_WarpFlame : DamageWorker_AddInjury
    {
        public override DamageResult Apply(DamageInfo dinfo, Thing victim)
        {
            Pawn pawn = victim as Pawn;
            if (pawn != null && pawn.Faction == Faction.OfPlayer)
            {
                Find.TickManager.slower.SignalForceNormalSpeedShort();
            }
            Map map = victim.Map;
            Random rnd = new Random();
            int hitAmount = rnd.Next(1, 3);
            DamageResult damageResult = base.Apply(dinfo, victim);

            DamageInfo dinfo2;
            DamageResult damageResult2;
            for (int i = 0; i < hitAmount; i++)
            {
                if (victim.Destroyed)
                {
                    break;
                }
                dinfo2 = new DamageInfo(Core40kDefOf.BEWH_WarpFlame, Core40kDefOf.BEWH_WarpFlame.defaultDamage);
                damageResult2 = base.Apply(dinfo2, victim);

                if (!damageResult2.deflected && !dinfo2.InstantPermanentInjury && Rand.Chance(FireUtility.ChanceToAttachFireFromEvent(victim)))
                {
                    victim.TryAttachFire(Rand.Range(0.15f, 0.25f));
                }
            }
            if (victim.Destroyed && map != null && pawn == null)
            {
                foreach (IntVec3 item in victim.OccupiedRect())
                {
                    FilthMaker.TryMakeFilth(item, map, ThingDefOf.Filth_Ash);
                }
                if (victim is Plant plant && plant.LifeStage != 0)
                {
                    plant.TrySpawnStump(PlantDestructionMode.Flame);
                }
            }

            return damageResult;
        }

        public override void ExplosionAffectCell(Explosion explosion, IntVec3 c, List<Thing> damagedThings, List<Thing> ignoredThings, bool canThrowMotes)
        {
            base.ExplosionAffectCell(explosion, c, damagedThings, ignoredThings, canThrowMotes);
            if (def == DamageDefOf.Flame && Rand.Chance(FireUtility.ChanceToStartFireIn(c, explosion.Map)))
            {
                FireUtility.TryStartFireIn(c, explosion.Map, Rand.Range(0.2f, 0.6f));
            }
        }
    }

}