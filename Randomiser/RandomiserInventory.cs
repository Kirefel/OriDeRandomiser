﻿namespace Randomiser
{
    public class RandomiserInventory : SaveSerialize
    {
        public bool goalComplete;
        public bool finishedGinsoEscape;

        public BitCollection pickupsCollected = new BitCollection(320);

        public int attackUpgrades;
        public int spiritLightEfficiency;
        public int extraDashes;
        public int extraJumps;
        public bool chargeDashEfficiency;
        public int healthRegen;
        public int energyRegen;

        public void Reset()
        {
            // TODO make this automatic somehow
            finishedGinsoEscape = false;
            goalComplete = false;
            pickupsCollected.Clear();
            attackUpgrades = 0;
            spiritLightEfficiency = 0;
            extraDashes = 0;
            extraJumps = 0;
            chargeDashEfficiency = false;
            healthRegen = 0;
            energyRegen = 0;
        }

        public override void Serialize(Archive ar)
        {
            ar.Serialize(ref goalComplete);
            ar.Serialize(ref finishedGinsoEscape);
            pickupsCollected.Serialize(ar);

            ar.Serialize(ref attackUpgrades);
            ar.Serialize(ref spiritLightEfficiency);
            ar.Serialize(ref extraDashes);
            ar.Serialize(ref extraJumps);
            ar.Serialize(ref chargeDashEfficiency);
            ar.Serialize(ref healthRegen);
            ar.Serialize(ref energyRegen);
        }
    }
}
