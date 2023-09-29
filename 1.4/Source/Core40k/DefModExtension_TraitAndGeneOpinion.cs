using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Core40k
{
    public class DefModExtension_TraitAndGeneOpinion : DefModExtension
    {
        //First field is for the god who has an opinion on said triat, E.G. "Khorne".
        //Second field is for how big their opinions is goes from -1 to 1, with -1 being dislike, 0 neutral and 1 likes
        public List<ChaosGods> godOpinion;
        public List<int> opinionDegree;
    }

}   