using System.Collections.Generic;
using Verse;

namespace Core40k
{
    public class DefModExtension_TraitAndGeneOpinion : DefModExtension
    {
        //First field is for the god who has an opinion on said trait or gene, E.G. "Khorne".
        //Second field is for how big their opinions is goes from -1 to 1, with -1 being dislike, 0 neutral and 1 likes.
        public List<ChaosGods> godOpinion = new List<ChaosGods>();
        public List<int> opinionDegree = new List<int>();

        //This list is a list of the gods who will not give pawns with said gene or trait any gifts ever.
        public List<ChaosGods> godWontGift = new List<ChaosGods>();

        //This is a list of the degree for traits with different degrees, works similar to "opinionDegree". First field is degree of trait, second is degree of opinion
        public List<Dictionary<int, int>> opinionDegreeForTraitDegrees = new List<Dictionary<int, int>>();

        //This is a list of the gods that will always give beneficial gifts when pawn has said gene/trait and included god is giving gifts.
        public List<ChaosGods> makesGodGiveBeneficial = new List<ChaosGods>();

        //If a gene or trait has this, said pawn can NEVER recieve gifts from chaos through rituals in GENES module or mental breaks in CHAOS module.
        public bool purity = false;
    }

}   