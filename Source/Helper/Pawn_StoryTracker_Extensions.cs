using System.Reflection;
using System.Collections.Generic;
using Verse;

namespace RimWorld
{

    public static class Pawn_StoryTracker_Extensions
    {
        
        public static void Reset(this Pawn_StoryTracker storyTracker)
        {
            List<WorkTypeDef> worktypeDef = new List<WorkTypeDef>();
            foreach (Backstory current in storyTracker.AllBackstories)
            {
                if (current == null)
                {
                    worktypeDef = null;
                }
                foreach (WorkTypeDef current2 in current.DisabledWorkTypes)
                {
                    if (!worktypeDef.Contains(current2))
                    {
                        worktypeDef.Add(current2);
                    }
                }
            }
            for (int i = 0; i < storyTracker.traits.allTraits.Count; i++)
            {
                foreach (WorkTypeDef current3 in storyTracker.traits.allTraits[i].GetDisabledWorkTypes())
                {
                    if (!worktypeDef.Contains(current3))
                    {
                        worktypeDef.Add(current3);
                    }
                }
            }
            typeof(Pawn_StoryTracker).GetField("cachedDisabledWorkTypes", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(storyTracker, worktypeDef);
        }
        

    }

    //public static class ThoughtHandler_Extensions
    //{
    //    #region easier on the eyes if I do it this way. Google says I should be carefull with extentions... oh well

    //    //private static void InjectThought(this ThoughtHandler thought, Thought T)
    //    //{

    //    //    // Hate this bit of code. thank fuck for extentions
    //    //    typeof(ThingWithComps).GetField("comps", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(thingWithComps, comps);
    //    //}


    //    //private static List<ThingComp> GetComps(this ThingWithComps thingWithComps)
    //    //{
    //    //    // useless bit... but now I have a nice looking Get and Set ...
    //    //    return thingWithComps.AllComps;
    //    //}

    //    #endregion

    //}

}