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
            return "Current process set to change traits.";

            return "Current process set to change: backstory.";
        }

        public static Texture2D Icon(string state)
        {
            if (state == "trait")
                return ContentFinder<Texture2D>.Get("UI/Commands/MAD_trait", true);
            return ContentFinder<Texture2D>.Get("UI/Commands/MAD_story", true);
        }

        public static string defaultLabel()
        {
                return "Switch Process";
            
        }

        public static string defaultDesc(string state)
        {
            if (state == "trait")
                return "Switch process to change: backstory.";

            return "Switch process to change traits.";
        }

    }

    public struct MADState
    {
        public static StateStructure MAD_trait = new StateStructure(
            "Switch Process",
            "Switch process to change: backstory.",
            ContentFinder<Texture2D>.Get("UI/Commands/MAD_trait", true),
            "Current process set to change traits.",
            "trait");
        public static StateStructure MAD_story = new StateStructure(
            "Switch Process",
            "Switch process to change traits.",
            ContentFinder<Texture2D>.Get("UI/Commands/MAD_story", true),
            "Current process set to change: backstory.",
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