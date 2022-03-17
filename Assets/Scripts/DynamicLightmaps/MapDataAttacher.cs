using UnityEngine;

namespace DynamicLightmaps
{
    public class MapDataAttacher : MonoBehaviour
    {
        [SerializeField] private string guid = string.Empty;
        public string GUID => guid;

        public MapData GetObjectMapData()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            return new MapData()
            {
                guid = guid,
                lightmapIndex = meshRenderer.lightmapIndex,
                lightmapScaleOffset = meshRenderer.lightmapScaleOffset
            };
        }

        public void AttachLightMap(LightState state)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            var lightData = state.GetObjectMapData(guid);
            if (lightData == null)
            {
                throw new System.Exception($"The object with the name {name} doesn't exist in the state {state.name}");
            }
            meshRenderer.lightmapIndex = lightData.lightmapIndex;
            meshRenderer.lightmapScaleOffset = lightData.lightmapScaleOffset;
        }
    }

}