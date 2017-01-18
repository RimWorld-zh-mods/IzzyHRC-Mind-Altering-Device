using System;
using System.Collections.Generic;
using UnityEngine;
using RimWorld;
using Verse;

namespace MAD
{
    public struct MADstateHelp
    {
        public static string Inspect(string state)
        {
            if (state == "trait")
            return "MAD_CurrentProcessTraits".Translate();

            return "MAD_CurrentProcessBackstory".Translate();
        }

        public static Texture2D Icon(string state)
        {
            if (state == "trait")
                return ContentFinder<Texture2D>.Get("UI/Commands/MAD_trait", true);
            return ContentFinder<Texture2D>.Get("UI/Commands/MAD_story", true);
        }

        public static string defaultLabel()
        {
                return "MAD_SwitchProcess".Translate();
            
        }

        public static string defaultDesc(string state)
        {
            if (state == "trait")
                return "MAD_SwitchProcessToBackstory".Translate();

            return "MAD_SwitchProcessToTraits".Translate();
        }

    }

    public struct MADState
    {
        public static StateStructure MAD_trait = new StateStructure(
            "MAD_SwitchProcess".Translate(),
            "MAD_SwitchProcessToBackstory".Translate(),
            ContentFinder<Texture2D>.Get("UI/Commands/MAD_trait", true),
            "MAD_CurrentProcessTraits".Translate(),
            "trait");
        public static StateStructure MAD_story = new StateStructure(
            "MAD_SwitchProcess".Translate(),
            "MAD_SwitchProcessToTraits".Translate(),
            ContentFinder<Texture2D>.Get("UI/Commands/MAD_story", true),
            "MAD_CurrentProcessBackstory".Translate(),
            "story");


    }
    public struct StateStructure
    {
        public string name;
        public string defaultLabel;
        public string defaultDesc;
        public Texture2D icon;

        public string inspect;

        public StateStructure(string a, string b, Texture2D c, string d,string e)
        {
            name = e;
            defaultLabel = a;
            defaultDesc = b;
            icon = c;
            inspect = d;
            
        }
    }
    
}




////Boolean isLit = oldGlower.Lit;
//              //oldGlower.Lit = false;
//              parent.BroadcastCompSignal("FlickedOff");
//              CompGlower newGlower = Util.newCompGlower(parent, glowColour, glowRadius);
//              list.Remove(oldGlower);
//              list.Add(newGlower);

//              //replaced with an extention to thingWithComps... doesnt feel safe but hey
//              //typeof(BuildingGlowTnC).GetField("comps", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(parent, List_AllThingComps);
//              parent.SetComps(list);
//              parent.BroadcastCompSignal("FlickedOn");
//              //newGlower.Lit = false;
//              updateMap(parent.Position);


//              //if (pwrTrader != null)
//              //{
//              //    if (isLit && pwrTrader.PowerOn)
//              //    {
//              //        newGlower.Lit = true;
//              //    }
//              //}