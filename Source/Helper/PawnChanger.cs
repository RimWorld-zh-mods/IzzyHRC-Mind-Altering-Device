using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MAD
{
    public class PawnChanger
    {
        public static void ExecuteBadThings(Pawn pawn)
        {
            GiveMood(pawn, ThoughtDef.Named("Scrambled"));

            int rng = Rand.RangeInclusive(1, 100);
            if (rng <= 10)
            {
                pawn.health.AddHediff(HediffDef.Named("BadMigraine"), null, null);
            }
            else
                if (rng <= 20)
                {
                    pawn.health.AddHediff(HediffDef.Named("BodyRestart"), null, null);
                }
                else
                    if (rng <= 50)
                    {
                        pawn.health.AddHediff(HediffDef.Named("Dementia"), null, null);
                    }
                    else
                    {
                        pawn.health.AddHediff(HediffDef.Named("Braindeath"), null, null);;
                    }
        }

        public static void SetMood(Pawn pawn)
        {
            if (HasMood(pawn, ThoughtDef.Named("Wrecked")))
            {
                GiveMood(pawn, ThoughtDef.Named("Wrecked"));
                GiveMood(pawn, ThoughtDef.Named("Confused"));
                GiveMood(pawn, ThoughtDef.Named("Wrong"));
            }
            else
                if (HasMood(pawn, ThoughtDef.Named("Confused")))
                {
                    GiveMood(pawn, ThoughtDef.Named("Wrecked"));
                    GiveMood(pawn, ThoughtDef.Named("Confused"));
                    GiveMood(pawn, ThoughtDef.Named("Wrong"));
                }
                else
                    if (HasMood(pawn, ThoughtDef.Named("Wrong")))
                    {

                        GiveMood(pawn, ThoughtDef.Named("Confused"));
                        GiveMood(pawn, ThoughtDef.Named("Wrong"));
                    }
                    else
                    {
                        GiveMood(pawn, ThoughtDef.Named("Wrong"));
                    }
        }

        //SituationalThoughtHandler
        //	private Thought_Situational TryCreateSituationalThought(ThoughtDef def)
        //{
        //	if (!this.pawn.needs.mood.thoughts.CanGetThought(def))
        //	{
        //		return null;
        //	}
        //	if (!def.Worker.CurrentState(this.pawn).Active)
        //	{
        //		return null;
        //	}
        //	Thought_Situational thought_Situational = (Thought_Situational)ThoughtMaker.MakeThought(def);
        //      thought_Situational.pawn = this.pawn;
        //	thought_Situational.RecalculateState();
        //	return thought_Situational;
        //}


        //cachedSituationalSocialThoughts.thoughts.Add(thought_SituationalSocial);
        //cachedSituationalThoughts.thoughts.Add(thought_Situational);
//    }
//}

//private void CheckRecalculateSituationalThoughtsAffectingMoodState()
//{
//    if (Find.TickManager.TicksGame - this.lastStateRecalculation < 100)
//    {
//        return;
//    }
//    this.lastStateRecalculation = Find.TickManager.TicksGame;
//    ProfilerThreadCheck.BeginSample("recalculating situational thoughts");
//    try
//    {
//        this.tmpCachedThoughts.Clear();
//        for (int i = 0; i < this.cachedSituationalThoughts.Count; i++)
//        {
//            this.cachedSituationalThoughts[i].RecalculateState();
//            this.tmpCachedThoughts.Add(this.cachedSituationalThoughts[i].def);
//        }
//        List<ThoughtDef> allDefsListForReading = DefDatabase<ThoughtDef>.AllDefsListForReading;
//        int j = 0;
//        int count = allDefsListForReading.Count;
//        while (j < count)
//        {
//            if (allDefsListForReading[j].IsSituational)
//            {
//                if (!allDefsListForReading[j].IsSocial)
//                {
//                    if (!this.tmpCachedThoughts.Contains(allDefsListForReading[j]))
//                    {
//                        Thought_Situational thought_Situational = this.TryCreateSituationalThought(allDefsListForReading[j]);
//                        if (thought_Situational != null)
//                        {
//                            this.cachedSituationalThoughts.Add(thought_Situational);
//                        }
//                    }
//                }
//            }
//            j++;
//        }
//        this.RecalculateSocialSituationalThoughtsAffectingMood();
//    }
//    finally
//    {
//        ProfilerThreadCheck.EndSample();
//    }
//}
public static void GiveMood(Pawn pawn, ThoughtDef Tdef)
        {
            //pawn.needs.mood.thoughts.TryGainThought(Tdef);
           // pawn.needs.mood.thoughts.cachedSituationalThoughts.thoughts.Add(thought_Situational);
            
            //pawn.needs.mood.thoughts.Thoughts.Add(ThoughtMaker.MakeThought(Tdef));
            pawn.needs.mood.thoughts.memories.TryGainMemoryThought(Tdef, null);
        }

        public static Boolean HasMood(Pawn pawn, ThoughtDef Tdef)
        {
            //pawn.needs.mood.thoughts.memories.Memories.Contains((Thought_Memory)ThoughtMaker.MakeThought(Tdef));
            //if (pawn.needs.mood.thoughts.Thoughts.Contains(ThoughtMaker.MakeThought(Tdef)))
            Thought_Memory thought_Memory = pawn.needs.mood.thoughts.memories.Memories.Find((Thought_Memory x) => x.def == Tdef);
            if (thought_Memory == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            //if (pawn.needs.mood.thoughts.memories.Memories.Contains((Thought_Memory)ThoughtMaker.MakeThought(Tdef)))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public static void SetPawnTraits(Pawn pawn, int num)
        {
            pawn.story.traits.allTraits.Clear();
            while (pawn.story.traits.allTraits.Count < num)
            {
                TraitDef newTraitDef = DefDatabase<TraitDef>.GetRandom();

                if (!pawn.story.traits.HasTrait(newTraitDef))
                {
                    if (newTraitDef.conflictingTraits == null || !Contains(pawn, newTraitDef.conflictingTraits))
                    {

                        Trait trait = new Trait(newTraitDef, PawnGenerator.RandomTraitDegree(newTraitDef));
                        //trait.Degree = PawnGenerator.RandomTraitDegree(trait.def);
                        //trait.
                        //trait.degree 
                        //mentalStateSTarter = mentalBreaker
                        //if (pawn.mindState.mentalStateStarter.StartHardMentalStateThreshold + trait.OffsetOfStat(StatDefOf.MentalBreakThreshold) <= 40f)
                        //{
                            pawn.story.traits.GainTrait(trait);
                        //}
                    }
                }
            }
        }

        //HardBreakThreshold == StartHardMentalStateThreshold
        //MentalStateStarter == MentalBreaker
        //pawn.mindState.breaker.HardBreakThreshold

        private static Boolean Contains(Pawn pPawnSel, List<TraitDef> lTraitDef)
        {

            TraitDef[] tArray = new TraitDef[lTraitDef.Count];

            lTraitDef.CopyTo(tArray, 0);

            for (var i = 0; i < tArray.Length; i++)
            {
                if (pPawnSel.story.traits.HasTrait(tArray[i]))
                {
                    return true;
                }
            }


            return false;

        }


        //public static void BreakPawn (Pawn pawn)
        //{
        //    MentalBreaker mB = pawn.mindState.breaker;
            
        //    //pawn.story.traits.allTraits

            

        //    pawn.mindState.broken.StartBrokenState(DefDatabase<BrokenStateDef>.GetNamed("Manhunter", true));



        //    //if (pawn.mindState.broken.CurState == null)
        //    //{
        //    //    if (pawn.story != null)
        //    //    {
        //    //        List<Trait> allTraits = pawn.story.traits.allTraits;
        //    //        for (int i = 0; i < allTraits.Count; i++)
        //    //        {
        //    //            TraitDegreeData currentData = allTraits[i].CurrentData;
        //    //            if (currentData.randomBreakState != null)
        //    //            {
        //    //                float mtb = currentData.randomBreakMtbDaysMoodCurve.Evaluate(CurMood(pawn));
        //    //                if (Rand.MTBEventOccurs(mtb, 30000f, 150f) && currentData.randomBreakState.Worker.StateCanOccur(pawn))
        //    //                {
        //    //                    pawn.mindState.broken.StartBrokenState(currentData.randomBreakState);
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}

        private static float CurMood(Pawn pawn)
        {
            
                if (pawn.needs.mood == null)
                {
                    return 0.5f;
                }
                return pawn.needs.mood.CurLevel;
            
        }
        
    }
}
