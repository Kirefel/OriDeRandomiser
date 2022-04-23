﻿using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Randomiser
{
    [HarmonyPatch(typeof(MapStone), nameof(MapStone.FixedUpdate))]
    internal class MapstonePatch
    {
        private static void Grant()
        {
            var progressiveLocation = Randomiser.Locations.GetProgressiveMapstoneLocation(Randomiser.MapstonesRepaired);
            Randomiser.Grant(progressiveLocation.guid);
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = instructions.ToList();

            var field = AccessTools.Field(typeof(MapStone), nameof(MapStone.CurrentState));

            for (int i = 0; i < codes.Count; i++)
            {
                yield return codes[i];

                if (codes[i].StoresField(field) && codes[i - 1].LoadsConstant((int)MapStone.State.Activated)) // this.CurrentState = State.Activated
                {
                    yield return CodeInstruction.Call(typeof(MapstonePatch), nameof(MapstonePatch.Grant)); // MapstonePatch.Grant()
                }
            }
        }
    }
}
