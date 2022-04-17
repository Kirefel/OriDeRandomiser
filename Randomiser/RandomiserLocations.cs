﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Randomiser
{
    public class RandomiserLocations : MonoBehaviour
    {
        private Dictionary<string, Location> nameMap = new Dictionary<string, Location>();
        private Dictionary<MoonGuid, Location> guidMap = new Dictionary<MoonGuid, Location>();

        public Location GetLocation(MoonGuid guid) => guidMap.GetOrDefault(guid);
        public Location GetLocation(string name) => nameMap.GetOrDefault(name);

        public IEnumerable<Location> GetAll() => guidMap.Values;

        void Awake()
        {
            Load(@".\Mods\assets\OriDeRandomiser\LocationData.json");
        }

        public void Load(string file)
        {
            // TODO handle missing file
            // TODO improve debug menu keyboard controls

            // Unity's JsonUtility sucks at deserialising nested objects, and Json.Net won't work
            // so we have to pick them out manually
            List<Location> allLocs = new List<Location>();

            string json = File.ReadAllText(file);
            int start = 0;
            int end = 0;
            while (end < json.Length)
            {
                if (json[end] == '{')
                {
                    start = end;
                }
                if (json[end] == '}')
                {
                    string obj = json.Substring(start, end - start + 1);
                    allLocs.Add(JsonUtility.FromJson<LocationData>(obj).ToLocation());
                }
                end++;
            }

            nameMap = allLocs.ToDictionary(l => l.name);
            guidMap = allLocs.ToDictionary(l => l.guid);
        }
    }

    [Serializable]
    public class LocationData
    {
        public string guid;
        public string name;
        public string type;
        public string area;
        public string position;

        Vector2 ParseVec2(string str)
        {
            var strings = str.Trim('(', ')').Split(',');
            return new Vector2(float.Parse(strings[0]), float.Parse(strings[1]));
        }

        public Location ToLocation() => new Location(name, ParseVec2(position), (Location.LocationType)Enum.Parse(typeof(Location.LocationType), type), (Location.WorldArea)Enum.Parse(typeof(Location.WorldArea), area), new MoonGuid(new Guid(guid)));
    }
}
