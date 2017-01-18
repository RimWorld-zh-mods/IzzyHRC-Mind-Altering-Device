
using System.Collections.Generic;
using Verse;
using Verse.AI;
using Verse.Sound;
using UnityEngine;
using RimWorld;
using System.Text;


namespace MAD
{
    public class Building_MAD : Building_Casket
    {
        protected int IcookingTicking = 0;
        protected int IcookingTime = 1000;

        //StateStructure stateMAD;
        string oldStateName = "trait";
        string curStateName = "trait";

        //bool EJECT;

        public CompPowerTrader powerComp;
        public CompGlower Glower;

        private ColorInt red = new ColorInt(255, 100, 100, 0);
        private ColorInt green = new ColorInt(100, 255, 100, 0);


        public ColorInt CurrentColour;


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.LookValue<int>(ref this.IcookingTicking, "IcookingTicking");
            Scribe_Values.LookValue<string>(ref this.oldStateName, "oldStateName");
            Scribe_Values.LookValue<string>(ref this.curStateName, "curStateName");
            
        }


        public bool PowerOn
        {
            get { return this.powerComp.PowerOn; }
        }
        

        public override void SpawnSetup(Map map)
        {
            base.SpawnSetup(map);
            this.Glower = base.GetComp<CompGlower>();
            this.powerComp = base.GetComp<CompPowerTrader>();
            ChangeColour(this.green);
            CurrentColour = this.green;
            //stateMAD = MADState.MAD_trait;
            //curStateName = "trait";
            //oldStateName = "trait";
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (DefDatabase<ResearchProjectDef>.GetNamed("MADresearchStory").IsFinished)
            {
                yield return this.Btn_ChangeState();
            }
            base.GetGizmos();
        }

        public Command_Action Btn_ChangeState()
        {
            Command_Action command_Action = new Command_Action();
            command_Action.defaultLabel = MADstateHelp.defaultLabel();
            command_Action.defaultDesc = MADstateHelp.defaultDesc(curStateName);
            command_Action.icon = MADstateHelp.Icon(curStateName);
            //if (stateMAD.name == "trait")
            //{
            //    command_Action.icon = ContentFinder<Texture2D>.Get("UI/Commands/MAD_trait", true);
            //}
            //else
            //{
            //    command_Action.icon = ContentFinder<Texture2D>.Get("UI/Commands/MAD_story", true);
            //}
            
            command_Action.action = delegate
            {
                if (curStateName == "trait")
                {
                    curStateName = "story";
                }
                else
                {
                    curStateName = "trait";
                }
                if (HasAnyContents)
                {
                    EjectContents();
                }
                oldStateName = curStateName;

            };
            command_Action.activateSound = SoundDef.Named("Click");
            return command_Action;
        }
        
        public override bool TryAcceptThing(Thing thing, bool allowSpecialEffects = true)
        {
            if (base.TryAcceptThing(thing, allowSpecialEffects))
            {
                if (allowSpecialEffects)
                {
                    SoundDef.Named("CryptosleepCasketAccept").PlayOneShot(new TargetInfo(base.Position, base.Map, false));
                }
                return true;
            }
            return false;
        }

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn myPawn)
        {
            if (!this.powerComp.PowerOn)
            {

                FloatMenuOption item5 = new FloatMenuOption("CannotUseNoPower".Translate(), null);
                return new List<FloatMenuOption>
				{
					item5
				};

            }

            if (!myPawn.CanReserve(this, 1))
            {
                FloatMenuOption item = new FloatMenuOption("CannotUseReserved".Translate(), null);
                return new List<FloatMenuOption>
				{
					item
				};
            }

            if (!myPawn.CanReach(this, PathEndMode.InteractionCell, Danger.Some))
            {
                FloatMenuOption item2 = new FloatMenuOption("CannotUseNoPath".Translate(), null);
                return new List<FloatMenuOption>
				{
					item2
				};
            }

            if (!HasAnyContents)
            {//Enter the Mind Altering Device
                FloatMenuOption item3 = new FloatMenuOption("EnterMad".Translate(), () =>
                {

                    myPawn.Reserve(this, 0);

                    Job job = new Job(DefDatabase<JobDef>.GetNamed("EnterMAD"), this);
                    
                    //myPawn.drafter.TakeOrderedJob(job);
                    myPawn.jobs.TryTakeOrderedJob(job);
                });
                return new List<FloatMenuOption>
				{
					item3
				};
            }
            if (HasAnyContents)
            {//Open the Mind Altering Device
                FloatMenuOption item4 = new FloatMenuOption("OpenMad".Translate(), () =>
                {


                    myPawn.Reserve(this, 0);

                    Job job = new Job(DefDatabase<JobDef>.GetNamed("OpenMAD"), this);



                    //myPawn.drafter.TakeOrderedJob(job);
                    myPawn.jobs.TryTakeOrderedJob(job);
                });
                return new List<FloatMenuOption>
				{
					item4
				};
            }

            return null;
        }

        public void CookIt()
        {
            foreach (Thing current in this.innerContainer)
            {
                
                Pawn pawn = current as Pawn;

                if (pawn != null)
                {
                    //INSERT NEW FUNCTION HERE!
                    if (curStateName == "trait")
                    {
                        TraitChanger(pawn);
                    }
                    if (curStateName == "story")
                    {
                        BackstoryChanger(pawn);
                    }
                    ConclusionAdder(pawn);
                }
            }
        }
        public void ConclusionAdder(Pawn pawn)
        {
            pawn.health.AddHediff(HediffDef.Named("Rewire"), null, null);
            if (PawnChanger.HasMood(pawn, ThoughtDef.Named("Wrecked")) || PawnChanger.HasMood(pawn, ThoughtDef.Named("Scrambled")))
            {
                PawnChanger.ExecuteBadThings(pawn);
            }
            PawnChanger.SetMood(pawn);
        }
        public void BackstoryChanger(Pawn pawn)
        {
            pawn.story.childhood = BackstoryDatabase.RandomBackstory(BackstorySlot.Childhood);
            pawn.story.adulthood = BackstoryDatabase.RandomBackstory(BackstorySlot.Adulthood);
            pawn.story.Reset();
        }

        public void TraitChanger (Pawn pawn)
        {
            PawnChanger.SetPawnTraits(pawn, Rand.RangeInclusive(2, 3));
            
        }

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(MADstateHelp.Inspect(curStateName));
            return stringBuilder.ToString();
        }


        //if(find.tickmanager.ticksgame % 60 == 0) <= skullywag
        public override void Tick()
        {
            //if (curStateName != stateMAD.name)
            //{
            //    if (curStateName == "trait")
            //    {
            //        stateMAD = MADState.MAD_trait;
            //    }
            //    else
            //    {
            //        stateMAD = MADState.MAD_story;
            //    }
            //}

            if (this.powerComp.PowerOn)
            {
                
                if (HasAnyContents)
                {
                    //if (oldStateName != curStateName && this.IcookingTicking == 0)
                    //{
                    //    oldStateName = curStateName;
                    //    EjectContents();
                    //}
                    
                    this.IcookingTicking++;
                    if (CurrentColour == this.green)
                    {
                        ChangeColour(this.red);

                    }
                    if (this.IcookingTicking >= IcookingTime)
                    {
                        this.CookIt();
                        this.EjectContents();
                        this.IcookingTicking = 0;
                    }
                }
                else
                {
                    if (CurrentColour == this.red)
                    {
                        ChangeColour(this.green);
                    }
                }
            }
            else
            {
                if (CurrentColour != this.red)
                {
                    ChangeColour(this.red);
                }

                this.IcookingTicking = 0;

                if (HasAnyContents)
                {
                    this.EjectContents();

                }
            }

        }

        public override void EjectContents()

        {
            foreach (Thing current in this.innerContainer)
            {
                Pawn pawn = current as Pawn;
                if (pawn != null)
                {
                    if (this.IcookingTicking != IcookingTime)
                    {
                        PawnChanger.ExecuteBadThings(pawn);
                    }


                }
            }
            if (!base.Destroyed)
            {
                SoundDef.Named("CryptosleepCasketEject").PlayOneShot(SoundInfo.InMap(new TargetInfo(base.Position, base.Map, false), MaintenanceType.None));
            }
            this.IcookingTicking = 0;
            ChangeColour(this.red);
            base.EjectContents();
        }


        public override void Draw()
        {
            base.Draw();
            DrawCookingBar();
            DrawLedLight();
        }

        public void DrawCookingBar()
        {
            MAD.DrawHelper.FillableBarRequest r = default(MAD.DrawHelper.FillableBarRequest);

            r.size = new Vector2(0.55f, 0.16f);
            r.fillPercent = (float)IcookingTicking / (float)IcookingTime;
            r.mat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.5f, 0.1f));
            r.matback = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.6f, 0.6f, 0.6f));

            Rot4 rotation = base.Rotation;
            rotation.Rotate(RotationDirection.Clockwise);
            r.rotation = rotation;

            if (r.rotation == Rot4.West) //North
            {
                r.rotation = Rot4.West;
                r.center = this.DrawPos + Vector3.up * 0.1f + Vector3.back * 0.45f;
            }
            if (r.rotation == Rot4.North) //East
            {
                r.rotation = Rot4.North;
                r.center = this.DrawPos + Vector3.up * 0.1f + Vector3.left * 0.45f;
            }
            if (r.rotation == Rot4.East) //South
            {
                r.rotation = Rot4.East;
                r.center = this.DrawPos + Vector3.up * 0.1f + Vector3.forward * 0.45f;
            }
            if (r.rotation == Rot4.South) //West
            {
                r.rotation = Rot4.South;
                r.center = this.DrawPos + Vector3.up * 0.1f + Vector3.right * 0.45f;
            }

            MAD.DrawHelper.DrawFillableBar(r);
        }

        public void DrawLedLight()
        {
            MAD.DrawHelper.RectangleRequest r = default(MAD.DrawHelper.RectangleRequest);


            r.size = new Vector2(0.16f, 0.16f);

            if (this.powerComp.PowerOn)
            {
                if (this.IcookingTicking == 0)
                {
                    r.mat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0f, 0.8f, 0f));
                }
                else
                {
                    r.mat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.8f, 0f, 0f));
                }
            }
            else
            {
                r.mat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.6f, 0.6f, 0.6f));
            }


            Rot4 rotation2 = base.Rotation;
            rotation2.Rotate(RotationDirection.Clockwise);
            r.rotation = rotation2;

            if (r.rotation == Rot4.West) //North
            {
                r.rotation = Rot4.West;
                r.center = this.DrawPos + Vector3.up * 0.1f + Vector3.forward * 0.08f;
            }
            if (r.rotation == Rot4.North) //East
            {
                r.rotation = Rot4.North;
                r.center = this.DrawPos + Vector3.up * 0.1f + Vector3.right * 0.08f;
            }
            if (r.rotation == Rot4.East) //South
            {
                r.rotation = Rot4.East;
                r.center = this.DrawPos + Vector3.up * 0.1f + Vector3.back * 0.08f;
            }
            if (r.rotation == Rot4.South) //West
            {
                r.rotation = Rot4.South;
                r.center = this.DrawPos + Vector3.up * 0.1f + Vector3.left * 0.08f;
            }

            MAD.DrawHelper.DrawRectangle(r);
        }



        public void ChangeColour(ColorInt colour)
        {
            CurrentColour = colour;
            Util.DestroyNCreateGlower(this, colour, this.Glower.Props.glowRadius,this.Map);
        }


    }
}
