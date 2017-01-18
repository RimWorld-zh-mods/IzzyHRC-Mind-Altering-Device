using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace MAD
{
    public class CompMAD : ThingComp //17/07/16-R
    {
        bool ChangeBackstory;
        
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.LookValue<bool>(ref this.ChangeBackstory, "ChangeBackstory");
        }

        

        public override void PostSpawnSetup()
        {
            base.PostSpawnSetup();
            ChangeBackstory = false;
        }

        public override IEnumerable<Command> CompGetGizmosExtra()
        {
            if (DefDatabase<ResearchProjectDef>.GetNamed("BackstoryChanger").IsFinished)
            {
                yield return this.Btn_Previous();
            }

        }
        public override string CompInspectStringExtra()
        {
            var stringBuilder = new StringBuilder();
            string extra = "";
            if (ChangeBackstory)
            {
                extra = "backstories";
            }
            else
            {
                extra = "traits";
            }
            stringBuilder.Append("Current function set to change ");
            return stringBuilder.ToString();
        }
        
        public Command_Action Btn_Previous()
        {
            Command_Action command_Action = new Command_Action();
            
            command_Action.defaultLabel = "Change function";
            command_Action.defaultDesc = "Change to traitchanger";
            if (ChangeBackstory)
            {
                command_Action.defaultDesc = "Change to backstorychanger";
            }
            
            //command_Action.icon = ContentFinder<Texture2D>.Get("UI/RotLeft", true);
            command_Action.icon = ContentFinder<Texture2D>.Get("UI/Commands/Halt", true);
            command_Action.action = delegate
            {
                if (ChangeBackstory)
                {
                    this.ChangeBackstory = false;
                }else
                {
                    this.ChangeBackstory = true;
                }
                                
            };
            command_Action.activateSound = SoundDef.Named("Click");
            return command_Action;
        }


    }

}

