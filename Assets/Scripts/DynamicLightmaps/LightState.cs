using UnityEngine;
using UnityEngine.Rendering;

namespace DynamicLightmaps
{
    [CreateAssetMenu(fileName = "LightState")]
    public class LightState : ScriptableObject
    {
        public MapData[] mapData;

        public Texture2D[] directionMaps;
        public Texture2D[] colorMaps;

        public SphericalHarmonicsL2[] lightProbesData;

        public int Size => directionMaps.Length;

        private void Awake()
        {
            if (directionMaps == null || colorMaps == null) return;
            if (directionMaps.Length != colorMaps.Length)
                throw new System.Exception("Lightmaps arrays do not have the same size");
        }

        public void UpdateLightMapsSettings()
        {
            var newLightmapData = new LightmapData[Size];

            for (int i = 0; i < Size; i++)
            {
                newLightmapData[i] = new LightmapData();
                newLightmapData[i].lightmapColor = colorMaps[i];
                newLightmapData[i].lightmapDir = directionMaps[i];
            }

            LightmapSettings.lightmaps = newLightmapData;
            LightmapSettings.lightProbes.bakedProbes = lightProbesData;

            var attachers = FindObjectsOfType<MapDataAttacher>();
            foreach (var attacher in attachers)
            {
                attacher.AttachLightMap(this);
            }
        }

        public MapData GetObjectMapData(string guid)
        {
            foreach (var temp in mapData)
                if (temp.guid == guid) return temp;

            return null;
        }
    }
}