﻿using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Core40k
{
    public class DefModExtension_Ritual : DefModExtension
    {
        public List<GeneDef> requiredGenes;
        public List<GeneDef> forbiddenGenes;
        public List<GeneDef> removesGenes; //Gene must also be present in required genes.

        public List<GeneDef> givesGenes;
        public List<GeneDef> givesGenesRandom;


        public string giftGiver = "the gods";


        public List<SkillDef> skillsScale;
        public float skillsScaleAmount;

        public List<TraitDef> traitsIncreaseChance;
        public List<TraitDef> traitsReduceChance;

        public List<GeneDef> genesIncreaseChance;
        public List<GeneDef> genesDecreaseChance;

        public float baseChance; //Base chance given pawn has no traits, genes and has 0 in every skill
    }

}   