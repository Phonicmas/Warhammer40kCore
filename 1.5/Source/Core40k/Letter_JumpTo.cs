using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using Verse;


namespace Core40k
{
    public class Letter_JumpTo : ChoiceLetter
    {
        protected DiaOption Option_ReadMore
        {
            get
            {
                GlobalTargetInfo target = lookTargets.TryGetPrimaryTarget();
                DiaOption diaOption = new DiaOption("JumpTo".Translate());
                diaOption.action = delegate
                {
                    CameraJumper.TryJumpAndSelect(target);
                    Find.LetterStack.RemoveLetter(this);
                };
                diaOption.resolveTree = true;
                if (!target.IsValid)
                {
                    diaOption.Disable(null);
                }
                return diaOption;
            }
        }

        public override IEnumerable<DiaOption> Choices
        {
            get
            {
                yield return base.Option_Close;
                if (lookTargets.IsValid())
                {
                    yield return Option_ReadMore;
                }
            }
        }
    }
}