using System.Reflection;
using System.Collections.Generic;
using Verse;

namespace RimWorld
{

    public static class Backstory_Extensions
    {
        #region easier on the eyes if I do it this way. Google says I should be carefull with extentions... oh well

        //public static void SetComps(this Backstory story, List<ThingComp> comps)
        //{
        //    // Hate this bit of code. thank fuck for extentions
        //    typeof(ThingWithComps).GetField("comps", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(thingWithComps, comps);
        //}


        public static void setBody(this Backstory story,BodyType b)
        {
            // useless bit... but now I have a nice looking Get and Set ...
            story.bodyTypeFemale = b;
            story.bodyTypeGlobal = b;
            story.bodyTypeMale = b;
           
        }

        #endregion

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