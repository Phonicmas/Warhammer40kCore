# Warhammer 40k Series for Rimworld

## Future Things?
1. 

## To do (Not ordered)
1. Make greater daemon sprites for all gods. (Psycast Module)
2. Give Khorne marked pawns special abilities that grows stronger the more they've killed. (Genes Module)
3. Special effect for daemon ressurection and summoning. (Genes and Psycast Module)

## Ideas

## Fixes to do


## New Modules
1. Servitor
    1. Make Servitors, these can be crafted by putting living pawns into machines where you then choose a specialization for them.

2. Drugs
    1. Look at Black Crusade book for references


# Core Framework

## DefModExtensions and what they do

1. DefModExtension_Description: Is placed on a GeneDef and will replace all written effects with whatever you put in its "newCustomEffectText" field, which is a string

2. DefModExtension_GenderDistribution: Is placed on a GeneDef and will make it so any child born with this gene will be more of less likely to be a certain gender based on given distribution. Field are "female" and "male" and both are ints

3. DefModExtension_GivesVFEAbility: Is placed on a GeneDef and will give the pawn all the listed abilities (and remove them if gene is). Field is "abilityDefs" and is a list of VFECore.Abilities.AbilityDef.

4. DefModExtension_LostHeartSurvival and DefModExtension_LostLungSurvival: Is placed on a GeneDef and will allow said pawn to survive the loss of both lung or heart.

5. DefModExtension_RequiresGene and DefModExtension_RequiresHediff: When used with their respective Recipe_InstallImplant will make said recipe require pawn to have given gene or hediff.

6. DefModExtension_SlaveabilityRecruitability: Is placed on a GeneDef and will make it so any pawn with this gene from another faction will be uncruitable and/or unslaveable. The fields are canBeEnslaved and canBeRecruited and both are booleans.

7. DefModExtension_UntradeablePawn: Is placed on a GeneDef and will make it so any pawnn with said gene will not show up as slaves in traders. There are no fields.