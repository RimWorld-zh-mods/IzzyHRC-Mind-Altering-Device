using System;
using System.Collections.Generic;
using UnityEngine;
using RimWorld;
using Verse;

namespace MAD
{

    public class Util //17/07/16-R
    {

        #region color related
        public static Color IntToColour(ColorInt col)
        {
            float r = (float)col.r / 255f;
            float g = (float)col.g / 255f;
            float b = (float)col.b / 255f;
            float a = (float)1f;

            return new Color(r, g, b, a);
        }
        #endregion

        #region mapmesh related
        public static void updateMap(IntVec3 pos,Map map)
        {
            map.mapDrawer.MapMeshDirty(pos, MapMeshFlag.Things);
            map.mapDrawer.MapMeshDirty(pos, MapMeshFlag.GroundGlow);
            map.glowGrid.MarkGlowGridDirty(pos);
        }
        #endregion

        #region Glower related
        //provides a new Compglower
        public static CompGlower newCompGlower(ThingWithComps parent, ColorInt glowColour, float glowRadius)
        {
            CompGlower Comp_NewGlower = new CompGlower();
            Comp_NewGlower.parent = parent;


            //CompProperties CompProp_New = new CompProperties();


            CompProperties_Glower CompProp_New = new CompProperties_Glower();
            CompProp_New.compClass = typeof(CompGlower);
            CompProp_New.glowColor = glowColour;
            CompProp_New.glowRadius = glowRadius;

            Comp_NewGlower.Initialize(CompProp_New);

            return Comp_NewGlower;
        }


        //moved it to util for easier implementation with MAD
        public static void DestroyNCreateGlower(ThingWithComps parent, ColorInt glowColour, float glowRadius,Map map)
        {
            CompGlower oldGlower = null;
            CompPowerTrader pwrTrader = null;

            List<ThingComp> list = parent.GetComps();

            foreach (ThingComp comp in list)
            {
                if (typeof(CompGlower) == comp.GetType())
                {
                    oldGlower = (CompGlower)comp;
                }
                if (typeof(CompPowerTrader) == comp.GetType())
                {
                    pwrTrader = (CompPowerTrader)comp;
                }
            }

            if (oldGlower != null)
            {

                //Boolean isLit = oldGlower.;
                //oldGlower.Lit = false;
                //parent.BroadcastCompSignal("FlickedOff");

                //oldGlower.PostDeSpawn();
                map.glowGrid.DeRegisterGlower(oldGlower);

                CompGlower newGlower = Util.newCompGlower(parent, glowColour, glowRadius);
                list.Remove(oldGlower);
                list.Add(newGlower);
                //Find.GlowGrid.RegisterGlower(newGlower);

                //(CompGlower)oldGlower.


                //replaced with an extention to thingWithComps... doesnt feel safe but hey
                //typeof(BuildingGlowTnC).GetField("comps", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(parent, List_AllThingComps);
                parent.SetComps(list);
                //parent.BroadcastCompSignal("FlickedOn");

                newGlower.UpdateLit(map);
                //newGlower.Lit = false;
                updateMap(parent.Position,map);


                //if (pwrTrader != null)
                //{
                //    if (isLit && pwrTrader.PowerOn)
                //    {
                //        newGlower.Lit = true;
                //    }
                //}
            }
        }
        #endregion


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