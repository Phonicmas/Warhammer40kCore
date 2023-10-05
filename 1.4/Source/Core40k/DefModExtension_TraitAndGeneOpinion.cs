using System.Collections.Generic;
using Verse;
using static Core40k.Core40kUtils;

namespace Core40k
{
    public class DefModExtension_TraitAndGeneOpinion : DefModExtension
    {
        //Has the gods enum and their opinion of the gene or trait its attached too.
        public List<GodOpinion> godOpinion = new List<GodOpinion>();

        //This list is a list of the gods who will not give pawns with said gene or trait any gifts ever.
        public List<ChaosGods> godWontGift = new List<ChaosGods>();

        //This is a list of the gods that will always give beneficial gifts when pawn has said gene/trait and included god is giving gifts.
        public List<ChaosGods> makesGodGiveBeneficial = new List<ChaosGods>();

        //If a gene or trait has this, said pawn can NEVER recieve gifts from chaos through rituals in GENES module or mental breaks in CHAOS module.
        public bool purity = false;
    }

}   