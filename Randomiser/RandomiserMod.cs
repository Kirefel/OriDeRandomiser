﻿using BaseModLib;
using HarmonyLib;
using OriDeModLoader;

namespace Randomiser
{
    public class RandomiserMod : IMod
    {
        public string Name => "Randomiser";

        Harmony harmony;

        public void Init()
        {
            harmony = new Harmony("com.ori.randomiser");
            harmony.PatchAll();

            Controllers.Add<RandomiserInventory>("b9d5727e-43ff-4a6c-a9d1-d51489b3733d", "Randomiser", mb => Randomiser.Inventory = mb as RandomiserInventory);
            Controllers.Add<RandomiserSeed>("df0ebc08-9469-4f58-9e10-f836115b797b", "Randomiser", mb => Randomiser.Seed = mb as RandomiserSeed);
            Controllers.Add<RandomiserLocations>("53d559f6-f989-48ae-9a6a-1c1b72b7955c", "Randomiser", mb => Randomiser.Locations = mb as RandomiserLocations);

            Hooks.OnStartNewGame += () =>
            {
                Randomiser.Inventory.Reset();
                Randomiser.Seed.LoadSeed("randomizer.dat");
            };

            SceneBootstrap.RegisterHandler(RandomiserBootstrap.SetupBootstrap, "Randomiser");
            RandomiserIcons.Initialise();
        }

        public void Unload()
        {

        }
    }
}
