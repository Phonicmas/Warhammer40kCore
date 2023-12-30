using System.Collections.Generic;
using Verse;
using VFECore.Abilities;

namespace Core40k
{
    public class Gene_GiveVFECoreAbility : Gene
    {
        public override void PostAdd()
        {
            CompAbilities comp = pawn.GetComp<CompAbilities>();
            if (comp != null)
            {
                if (def.HasModExtension<DefModExtension_GivesVFEAbility>())
                {
                    DefModExtension_GivesVFEAbility defModExtension = def.GetModExtension<DefModExtension_GivesVFEAbility>();
                    foreach (AbilityDef abilityDef in defModExtension.abilityDefs)
                    {
                        comp.GiveAbility(abilityDef);
                    }
                }
            }

            base.PostAdd();
        }

        public override void PostRemove()
        {
            CompAbilities comp = pawn.GetComp<CompAbilities>();
            if (comp != null)
            {
                if (def.HasModExtension<DefModExtension_GivesVFEAbility>())
                {
                    DefModExtension_GivesVFEAbility defModExtension = def.GetModExtension<DefModExtension_GivesVFEAbility>();
                    for (int i = 0; i < comp.LearnedAbilities.Count; i++)
                    {
                        foreach (AbilityDef abilityDef in defModExtension.abilityDefs)
                        {
                            if (comp.LearnedAbilities[i].def == abilityDef)
                            {
                                comp.LearnedAbilities.RemoveAt(i);
                            }
                        }
                    }
                }
            }

            base.PostRemove();
        }
    }
}   