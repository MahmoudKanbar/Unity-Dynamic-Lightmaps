using UnityEngine;

namespace DynamicLightmaps
{
    [System.Serializable]
    public class MapData
    {
        public string guid;
        public int lightmapIndex;
        public Vector4 lightmapScaleOffset;
    }
}
